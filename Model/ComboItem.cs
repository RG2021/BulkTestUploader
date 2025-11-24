using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkTestUploader.Model
{
    public class ComboItem<T> where T : class
    {
        public required string Name { get; set; }
        public required string Id { get; set; }
        public T? Value { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
