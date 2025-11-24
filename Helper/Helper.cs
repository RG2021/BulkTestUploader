using BulkTestUploader.Model;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkItem = Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.WorkItem;

namespace BulkTestUploader.Helper
{
    public static class Helper
    {
        public static bool ValidatePATToken(string organization, string patToken)
        {
            return !string.IsNullOrEmpty(organization) && !string.IsNullOrEmpty(patToken);
        }

        public static List<CustomTestCase> LoadTestCasesFromFile(string filePath)
        {
            List<CustomTestCase> results = new List<CustomTestCase>();

            using (IXLWorkbook workbook = new XLWorkbook(filePath))
            {
                IXLWorksheet worksheet = workbook.Worksheets.First();
                IXLRow header = worksheet.FirstRow();
                Dictionary<string, int> col = header.Cells().Where(c => !string.IsNullOrWhiteSpace(c.GetString())).ToDictionary(c => c.GetString().Trim(), c => c.Address.ColumnNumber);

                List<IXLRangeRow> rows = worksheet.RangeUsed().RowsUsed().Skip(1).ToList();

                int groupId = 0;

                List<List<IXLRangeRow>> groupedRows = rows.Select(r =>
                {
                    if (r.Cell(col["Work Item Type"]).GetString().Trim() == "Test case") groupId++;
                    return new { Row = r, Group = groupId };

                }).GroupBy(x => x.Group).Select(g => g.Select(x => x.Row).ToList()).ToList();

                int id = -1;
                foreach (List<IXLRangeRow> group in groupedRows)
                {
                    IXLRangeRow headerRow = group.First();
                    List<TestCaseStep> steps = new List<TestCaseStep>();

                    foreach (IXLRangeRow row in group.Skip(1))
                    {
                        string stepNumStr = row.Cell(col["Test Step"]).GetString().Trim();

                        if (!int.TryParse(stepNumStr, out int stepNum))
                        {
                            continue;
                        }

                        steps.Add(new TestCaseStep
                        {
                            StepNumber = stepNumStr,
                            Action = row.Cell(col["Step Action"]).GetString()?.Trim() ?? "",
                            Expected = row.Cell(col["Step Expected"]).GetString()?.Trim() ?? ""
                        });
                    }

                    CustomTestCase customTestCase = new CustomTestCase
                    {
                        Id = id--,
                        Title = headerRow.Cell(col["Title"]).GetString()?.Trim() ?? null,
                        Tags = headerRow.Cell(col["Tags"]).GetString()?.Trim() ?? null,
                        Steps = steps,
                        // ParentItem = headerRow.Cell(col["Parent Item"]).GetString()?.Trim() ?? null,
                    };

                    results.Add(customTestCase);
                }
            }

            return results;
        }

        public static List<List<JsonPatchOperation>> GenerateBatchForTestCases(List<CustomTestCase> customTestCases, TestPlan testPlan)
        {
            List<List<JsonPatchOperation>> results = new List<List<JsonPatchOperation>>();
            foreach (CustomTestCase customTestCase in customTestCases)
            {
                List<JsonPatchOperation> patchOperations = new List<JsonPatchOperation>();
                TestCase testCase = BuildTestCase(customTestCase, testPlan);

                patchOperations.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/id",
                    Value = customTestCase.Id
                });

                foreach (var field in testCase.workItem.WorkItemFields)
                {
                    var fieldDict = field as CustomWorkItemField;

                    if (fieldDict != null && !String.IsNullOrEmpty(fieldDict.Name) && !String.IsNullOrEmpty(fieldDict.Value))
                    {
                        patchOperations.Add(new JsonPatchOperation
                        {
                            Operation = Operation.Add,
                            Path = $"/fields/{fieldDict.Name}",
                            Value = fieldDict.Value
                        });
                    }
                }

                results.Add(patchOperations);
            }

            return results;
        }

        private static TestCase BuildTestCase(CustomTestCase customTestCase, TestPlan testPlan)
        {
            return new TestCase
            {
                workItem = new WorkItemDetails
                {
                    WorkItemFields = new List<object>
                    {
                        new CustomWorkItemField
                        {
                            Name = "System.Title",
                            Value = customTestCase.Title
                        },
                        new CustomWorkItemField
                        {
                            Name = "System.Tags",
                            Value = customTestCase.Tags
                        },
                        new CustomWorkItemField
                        {
                            Name = "Microsoft.VSTS.TCM.Steps",
                            Value = BuildStepsXml(customTestCase.Steps)
                        },
                        new CustomWorkItemField
                        {
                            Name = "System.AreaPath",
                            Value = testPlan.AreaPath
                        },
                        new CustomWorkItemField
                        {
                            Name = "System.IterationPath",
                            Value = testPlan.Iteration
                        }
                    }
                }
            };
        }

        private static string BuildStepsXml(List<TestCaseStep> steps)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<steps id=\"0\" last=\""+ steps.Count +"\">");
            foreach (TestCaseStep step in steps)
            {
                sb.AppendLine($"  <step id=\"{step.StepNumber}\" type=\"ActionStep\">");
                sb.AppendLine("    <parameterizedString isformatted=\"true\">");
                sb.AppendLine("      <text>" + System.Security.SecurityElement.Escape(step.Action) + "</text>");
                sb.AppendLine("    </parameterizedString>");
                sb.AppendLine("    <parameterizedString isformatted=\"true\">");
                sb.AppendLine("      <text>" + System.Security.SecurityElement.Escape(step.Expected) + "</text>");
                sb.AppendLine("    </parameterizedString>");
                sb.AppendLine("  </step>");
            }
            sb.AppendLine("</steps>");
            return sb.ToString();
        }

        public static List<SuiteTestCaseCreateUpdateParameters> GetSuiteTestCaseList(List<int> testCaseIds)
        {
            List<SuiteTestCaseCreateUpdateParameters> testCases = new List<SuiteTestCaseCreateUpdateParameters>();
            foreach (int testCaseId in testCaseIds)
            {
                testCases.Add(new SuiteTestCaseCreateUpdateParameters
                {
                    workItem = new WorkItem
                    {
                        Id = testCaseId
                    }
                });
            }
            return testCases;
        }
    }
}
