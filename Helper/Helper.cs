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
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WorkItem = Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.WorkItem;
using WorkItemField = BulkTestUploader.Model.WorkItemField;

namespace BulkTestUploader.Helper
{
    public static class Helper
    {
        public static bool ValidatePATToken(string organization, string patToken)
        {
            return !string.IsNullOrEmpty(organization) && !string.IsNullOrEmpty(patToken);
        }

        public static List<TestCaseTemplate> LoadTemplatesFromFile(string filePath)
        {
            List<TestCaseTemplate> results = new List<TestCaseTemplate>();

            using (IXLWorkbook workbook = new XLWorkbook(filePath))
            {
                IXLWorksheet worksheet = workbook.Worksheets.First();
                IXLRow header = worksheet.FirstRow();
                Dictionary<string, int> col = header.Cells().Where(c => !string.IsNullOrWhiteSpace(c.GetString())).ToDictionary(c => c.GetString().Trim(), c => c.Address.ColumnNumber, StringComparer.OrdinalIgnoreCase);

                List<IXLRangeRow> rows = worksheet.RangeUsed().RowsUsed().Skip(1).ToList();
                List<PropertyInfo> props = typeof(TestCaseTemplate).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

                foreach (IXLRangeRow row in rows)
                {
                    TestCaseTemplate template = Activator.CreateInstance<TestCaseTemplate>();

                    foreach (PropertyInfo prop in props)
                    {
                        ExcelColumnAttribute? attr = prop.GetCustomAttribute<ExcelColumnAttribute>();
                        string columnName = attr?.Name ?? prop.Name;

                        if (!col.TryGetValue(columnName, out var idx)) continue;

                        string? cellValue = row.Cell(idx).GetString()?.Trim();
                        if (string.IsNullOrEmpty(cellValue)) continue;

                        if(prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(template, cellValue);
                        }   
                    }

                    results.Add(template!);
                }

                return results;
            }
            
        }

        public static List<TestCaseItem> LoadTestCasesFromFile(string filePath, TestPlan testPlan)
        {
            List<TestCaseTemplate> testCaseTemplates = LoadTemplatesFromFile(filePath);
            List<TestCaseItem> results = new List<TestCaseItem>();

            if (testCaseTemplates == null || testCaseTemplates.Count == 0)
                return results;

            List<List<TestCaseTemplate>> groups = new List<List<TestCaseTemplate>>();
            List<TestCaseTemplate>? currentGroup = null;

            foreach (TestCaseTemplate tpl in testCaseTemplates)
            {
                if (string.Equals(tpl.WorkItemType?.Trim(), "Test case", StringComparison.OrdinalIgnoreCase))
                {
                    currentGroup = new List<TestCaseTemplate>();
                    groups.Add(currentGroup);
                    currentGroup.Add(tpl);
                }
                else
                {
                    currentGroup?.Add(tpl);
                }
            }

            int testCaseId = -1;
            foreach (List<TestCaseTemplate> group in groups)
            {
                TestCaseTemplate headerTpl = group.First();
                List<TestCaseTemplate> stepTpls = group.Skip(1).ToList();

                List<TestCaseStep> steps = new List<TestCaseStep>();

                foreach (TestCaseTemplate st in stepTpls)
                {
                    string? stepNumber = st.TestStep?.ToString() ?? null;
                    if (string.IsNullOrEmpty(stepNumber)) continue;

                    steps.Add(new TestCaseStep
                    {
                        StepNumber = stepNumber,
                        Action = st.StepAction?.Trim(),
                        Expected = st.StepExpected?.Trim()
                    });
                }

                TestCaseItem testCase = new TestCaseItem
                {
                    Id = testCaseId--,
                    Title = headerTpl.Title?.Trim() ?? null,
                    Tags = headerTpl.Tags?.Trim() ?? null,
                    Steps = BuildStepsXml(steps),
                    AreaPath = testPlan.AreaPath,
                    IterationPath = testPlan.Iteration,
                    ParentItem = headerTpl.ParentItem?.ToString().Trim() ?? null,
                    AssignedTo = headerTpl.AssignedTo?.ToString().Trim() ?? null
                };

                results.Add(testCase);
            }

            return results;
        }

        public static List<List<JsonPatchOperation>> GenerateBatchForTestCases(List<TestCaseItem> testCases)
        {
            List<List<JsonPatchOperation>> results = new List<List<JsonPatchOperation>>();
            foreach (TestCaseItem testCase in testCases)
            {
                List<JsonPatchOperation> patchOperations = new List<JsonPatchOperation>();
                List<PropertyInfo> properties = testCase.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();

                foreach (PropertyInfo prop in properties)
                {
                    WorkItemField? attr = prop.GetCustomAttribute<WorkItemField>();
                    if (attr == null) continue;

                    object? value = prop.GetValue(testCase);

                    if(value == null || (value is string str && string.IsNullOrEmpty(str))) continue;

                    if(attr.Type == "WorkItem" && attr.Relation != null)
                    {
                        string parentIdStr = value.ToString() ?? string.Empty;
                        value = new
                        {
                            rel = attr.Relation,
                            url = $"vstfs:///WorkItemTracking/WorkItem/{parentIdStr}",
                        };
                    }

                    string path = string.IsNullOrEmpty(attr.Path) ? $"/fields/{attr.Name}" : attr.Path;

                    patchOperations.Add(new JsonPatchOperation
                    {
                        Operation = Operation.Add,
                        Path = path,
                        Value = value
                    });
                }

                //TestCase testCase = BuildTestCase(customTestCase, testPlan);

                //patchOperations.Add(new JsonPatchOperation()
                //{
                //    Operation = Operation.Add,
                //    Path = "/id",
                //    Value = customTestCase.Id
                //});

                //foreach (var field in testCase.workItem.WorkItemFields)
                //{
                //    var fieldDict = field as CustomWorkItemField;

                //    if (fieldDict != null && !String.IsNullOrEmpty(fieldDict.Name) && !String.IsNullOrEmpty(fieldDict.Value))
                //    {
                //        patchOperations.Add(new JsonPatchOperation
                //        {
                //            Operation = Operation.Add,
                //            Path = $"/fields/{fieldDict.Name}",
                //            Value = fieldDict.Value
                //        });
                //    }
                //}

                results.Add(patchOperations);
            }

            return results;
        }

        //private static TestCase BuildTestCase(TestCaseItem customTestCase, TestPlan testPlan)
        //{
        //    return new TestCase
        //    {
        //        workItem = new WorkItemDetails
        //        {
        //            WorkItemFields = new List<object>
        //            {
        //                new CustomWorkItemField
        //                {
        //                    Name = "System.Title",
        //                    Value = customTestCase.Title
        //                },
        //                new CustomWorkItemField
        //                {
        //                    Name = "System.Tags",
        //                    Value = customTestCase.Tags
        //                },
        //                new CustomWorkItemField
        //                {
        //                    Name = "Microsoft.VSTS.TCM.Steps",
        //                    Value = customTestCase.Steps
        //                },
        //                new CustomWorkItemField
        //                {
        //                    Name = "System.AreaPath",
        //                    Value = testPlan.AreaPath
        //                },
        //                new CustomWorkItemField
        //                {
        //                    Name = "System.IterationPath",
        //                    Value = testPlan.Iteration
        //                }
        //            }
        //        }
        //    };
        //}

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
