using BulkTestUploader.Model;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BulkTestUploader.Helper.Helper;
using Timer = System.Windows.Forms.Timer;

namespace BulkTestUploader.Control
{
    public class ActionsControl : BaseControl
    {
        ToolStripButton UploadTestCases { get; set; }
        public ActionsControl(ToolStrip actionsControl)
        {
            UploadTestCases = actionsControl.Items["uploadTestCasesButton"] as ToolStripButton;
            UploadTestCases.Click += UploadTestCases_Click;
        }

        private void UploadTestCases_Click(object? sender, EventArgs e)
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

                if(createdTestCaseIds.Count == 0)
                {
                    Logger?.Log($"No test cases were created for suite '{suite.SuiteId}'. Skipping adding to suite.");
                    continue;
                }

                Logger?.Log($"Created {createdTestCaseIds.Count} test cases for suite '{suite.SuiteId}'.");

                List<TestCase> uploadedTestCases = DevopsService.AddTestCaseToSuite(projectName, testPlanId, int.Parse(suite.SuiteId), createdTestCaseIds);
        
                stopwatch.Stop();
                Logger?.Log($"Uploaded {uploadedTestCases.Count} test cases to suite '{suite.SuiteId}' in {stopwatch.ElapsedMilliseconds} ms.");
            }
        }
    }
}
