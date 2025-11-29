using System;

namespace BulkTestUploader.Model
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ExcelColumnAttribute : Attribute
    {
        public string Name { get; }
        public ExcelColumnAttribute(string name) => Name = name;
    }
}