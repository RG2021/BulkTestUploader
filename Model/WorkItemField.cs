using Microsoft.VisualStudio.Services.WebApi.Patch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkTestUploader.Model
{
    public class WorkItemField : Attribute
    {
        public required string Name { get; set; }
        public string? Path { get; set; }
        public string? Type { get; set; }
        public string? Relation { get; set; }
    }
}
