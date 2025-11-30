using System;

namespace BulkTestUploader.Model
{
    public class TestCaseTemplate
    {
        [ExcelColumn("Work Item Type")]
        public required string WorkItemType { get; set; }

        [ExcelColumn("Title")]
        public required string Title { get; set; }

        [ExcelColumn("Test Step")]
        public string? TestStep { get; set; }

        [ExcelColumn("Step Action")]
        public string? StepAction { get; set; }

        [ExcelColumn("Step Expected")]
        public string? StepExpected { get; set; }

        [ExcelColumn("Parent Item")]
        public string? ParentItem { get; set; }

        [ExcelColumn("Tags")]
        public string? Tags { get; set; }

        [ExcelColumn("Assigned To")]
        public string? AssignedTo { get; set; }
    }
}
