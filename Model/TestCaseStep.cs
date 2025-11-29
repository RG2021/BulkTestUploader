using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkTestUploader.Model
{
    public class TestCaseStep
    {
        public required string StepNumber { get; set; }
        public string? Action { get; set; }
        public string? Expected { get; set; }
    }
}
