using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkTestUploader.Model
{
    public class ComboItem
    {
        public required string Name { get; set; }
        public required string Id { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
