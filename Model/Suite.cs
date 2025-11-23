using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkTestUploader.Model
{
    public class Suite : INotifyPropertyChanged
    {
        public string SuiteId { get; set; } = string.Empty;
        public string SuiteName { get; set; } = string.Empty;
        public string SuitePath { get; set; } = string.Empty;
        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    OnPropertyChanged(nameof(FilePath));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
