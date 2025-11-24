using BulkTestUploader.Model;
using ClosedXML.Excel;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static BulkTestUploader.Helper.Helper;
using Timer = System.Windows.Forms.Timer;

namespace BulkTestUploader.Control
{
    public class ActionsControl : BaseControl
    {
        ToolStripButton UploadTestCases { get; set; }
        ToolStripButton ImportTemplate { get; set; }
        ToolStripButton ExportTemplate { get; set; }
        public ActionsControl(ToolStrip actionsControl)
        {
            UploadTestCases = actionsControl.Items["uploadTestCasesButton"] as ToolStripButton;
            ImportTemplate = actionsControl.Items["importTemplateButton"] as ToolStripButton;
            ExportTemplate = actionsControl.Items["exportTemplateButton"] as ToolStripButton;

            UploadTestCases.Click += UploadTestCases_Click;
            ImportTemplate.Click += ImportTemplate_Click;
            ExportTemplate.Click += ExportTemplate_Click;
        }

        private void UploadTestCases_Click(object? sender, EventArgs e)
        {
            try
            {
                string projectName = InputControl?.GetProjectName() ?? string.Empty;
                Guid projectId = InputControl?.GetProjectId() ?? Guid.Empty;
                int testPlanId = InputControl?.GetTestPlanId() ?? 0;

                List<Suite> selectedValidSuites = SuitesGridControl?.GetSuiteList().Where(suite => !string.IsNullOrEmpty(suite.FilePath)).ToList() ?? new List<Suite>();
                if (selectedValidSuites.Count == 0)
                {
                    Logger?.Log("No valid test suites with file paths found for upload.");
                    return;
                }

                Logger?.Log($"Found {selectedValidSuites.Count} valid test suites for upload.");

                foreach (Suite suite in selectedValidSuites)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    List<CustomTestCase> testCases = LoadTestCasesFromFile(suite.FilePath);
                    List<List<JsonPatchOperation>> batch = GenerateBatchForTestCases(testCases);

                    List<WitBatchResponse> responses = DevopsService.CreateTestCases(projectId, batch);

                    List<int> createdTestCaseIds = [];
                    foreach (WitBatchResponse response in responses.Where(r => r.Code == 200))
                    {
                        dynamic output = JsonConvert.DeserializeObject(response.Body);
                        createdTestCaseIds.Add((int)output.id);
                    }

                    if (createdTestCaseIds.Count == 0)
                    {
                        Logger?.Log($"No test cases were created for suite '{suite.SuiteId}'. Skipping adding to suite.");
                        continue;
                    }

                    Logger?.Log($"Created {createdTestCaseIds.Count} test cases for suite id '{suite.SuiteId}'.");

                    List<TestCase> uploadedTestCases = DevopsService.AddTestCaseToSuite(projectName, testPlanId, int.Parse(suite.SuiteId), createdTestCaseIds);

                    stopwatch.Stop();
                    Logger?.Log($"Uploaded {uploadedTestCases.Count} test cases to suite id '{suite.SuiteId}' in {stopwatch.ElapsedMilliseconds} ms.");
                }
            }
            catch (Exception ex)
            {
                Logger?.Log($"Error during upload: {ex.Message}");
            }
        }

        private void ImportTemplate_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                openFileDialog.Title = "Select Template File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    using (IXLWorkbook workbook = new XLWorkbook(filePath))
                    {
                        IXLWorksheet worksheet = workbook.Worksheets.First();
                        IXLRow header = worksheet.FirstRow();
                        Dictionary<string, int> col = header.Cells().Where(c => !string.IsNullOrWhiteSpace(c.GetString())).ToDictionary(c => c.GetString().Trim(), c => c.Address.ColumnNumber);

                        List<string> requiredColumns = new List<string> { "Suite ID", "Suite Name", "Suite Path", "Test File Path" };
                        foreach (string requiredColumn in requiredColumns)
                        {
                            if (!col.ContainsKey(requiredColumn))
                            {
                                Logger?.Log($"Template is missing required column: {requiredColumn}");
                                return;
                            }
                        }
                        Logger?.Log("Template file validated successfully.");

                        List<Suite> importedSuites = worksheet.RowsUsed().Skip(1).Select(row => new Suite
                        {
                            SuiteId = row.Cell(col["Suite ID"]).GetString().Trim(),
                            SuiteName = row.Cell(col["Suite Name"]).GetString().Trim(),
                            SuitePath = row.Cell(col["Suite Path"]).GetString().Trim(),
                            FilePath = row.Cell(col["Test File Path"]).GetString().Trim()
                        }).ToList();

                        SuitesGridControl?.LoadSuites(importedSuites);
                    }
                }
            }
        }

        private void ExportTemplate_Click(object? sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                saveFileDialog.Title = "Save Template File";
                saveFileDialog.FileName = $"TestSuiteTemplate_{DateTime.Now:yyyyMMddHHmm}.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    using (IXLWorkbook workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet = workbook.Worksheets.Add("Test Suites");
                        worksheet.Cell(1, 1).Value = "Suite ID";
                        worksheet.Cell(1, 2).Value = "Suite Name";
                        worksheet.Cell(1, 3).Value = "Suite Path";
                        worksheet.Cell(1, 4).Value = "Test File Path";

                        List<Suite> suites = SuitesGridControl?.GetSuiteList().ToList() ?? new List<Suite>();

                        foreach (Suite suite in suites)
                        {
                            int newRow = worksheet.LastRowUsed().RowNumber() + 1;
                            worksheet.Cell(newRow, 1).Value = suite.SuiteId;
                            worksheet.Cell(newRow, 2).Value = suite.SuiteName;
                            worksheet.Cell(newRow, 3).Value = suite.SuitePath;
                            worksheet.Cell(newRow, 4).Value = suite.FilePath;
                        }

                        workbook.SaveAs(filePath);
                        Logger?.Log($"Template file exported successfully to {filePath}");
                    }
                }
            }
        }
    }
}
