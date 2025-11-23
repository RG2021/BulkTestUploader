using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkTestUploader.Control
{
    public class LogsControl(RichTextBox logsTextBox) : BaseControl
    {
        RichTextBox LogsTextBox { get; set; } = logsTextBox;

        public void Log(string log)
        {
            if (LogsTextBox.InvokeRequired)
            {
                LogsTextBox.Invoke(new Action(() =>
                {
                    LogsTextBox.AppendText($"{DateTime.Now}: {log}{Environment.NewLine}");
                }));
            }
            else
            {
                LogsTextBox.AppendText($"{DateTime.Now}: {log}{Environment.NewLine}");
            }
        }

        public void ClearLogs()
        {
            if (LogsTextBox.InvokeRequired)
            {
                LogsTextBox.Invoke(new Action(() =>
                {
                    LogsTextBox.Clear();
                }));
            }
            else
            {
                LogsTextBox.Clear();
            }
        }
    }
}
