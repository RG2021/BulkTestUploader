using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkTestUploader.Model
{
    public class CustomTestCase
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Tags { get; set; }
        public List<TestCaseStep>? Steps { get; set; }
        public string? ParentItem { get; set; }
}
}
