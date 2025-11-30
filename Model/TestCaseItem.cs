using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.WebApi.Patch;

namespace BulkTestUploader.Model
{
    public class TestCaseItem
    {
        [WorkItemField(Name = "id", Path = "/id")]
        public int Id { get; set; }

        [WorkItemField(Name = "System.Title")]
        public string? Title { get; set; }

        [WorkItemField(Name = "System.Tags")]
        public string? Tags { get; set; }

        [WorkItemField(Name = "Microsoft.VSTS.TCM.Steps")]
        public string? Steps { get; set; }

        [WorkItemField(Name = "System.Parent", Type = "WorkItem", Relation = "System.LinkTypes.Hierarchy-Reverse", Path = "/relations/-")]
        public string? ParentItem { get; set; }

        [WorkItemField(Name = "System.IterationPath")]
        public string IterationPath { get; set; } = string.Empty;

        [WorkItemField(Name = "System.AreaPath")]
        public string AreaPath { get; set; } = string.Empty;

        [WorkItemField(Name = "System.AssignedTo")]
        public string? AssignedTo { get; set; } = string.Empty;
    }
}
