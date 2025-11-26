using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkTestUploader.Control
{
    public class StatusBarControl : BaseControl
    {
        StatusStrip StatusStrip { get; set; }
        public ToolStripProgressBar ProgressBar { get; set; }
        public ToolStripStatusLabel UploadedCount { get; set; }
        public ToolStripStatusLabel TimeTaken { get; set; }
        public StatusBarControl(StatusStrip statusBarStrip)
        {
            StatusStrip = statusBarStrip;
            ProgressBar = statusBarStrip.Items["progressStatusBar"] as ToolStripProgressBar;
            UploadedCount = statusBarStrip.Items["countStatusLabel"] as ToolStripStatusLabel;
            TimeTaken = statusBarStrip.Items["timeStatusLabel"] as ToolStripStatusLabel;
        }

        public void ResetStatusBar()
        {
            Action resetAction = () =>
            {
                ProgressBar.Value = 0;
                UploadedCount.Text = "Uploaded 0 of 0 suites";
                TimeTaken.Text = "00:00:00";
            };

            if (StatusStrip.InvokeRequired)
            {
                StatusStrip.Invoke(resetAction);
            }
            else
            {
                resetAction.Invoke();
            }
        }

        public int UpdateProgress(int value)
        {
            int finalValue = 0;

            Action updateAction = () =>
            {
                if (ProgressBar.Value + value <= ProgressBar.Maximum)
                {
                    ProgressBar.Value += value;
                }
                else
                {
                    ProgressBar.Value = ProgressBar.Maximum;
                }
                finalValue = ProgressBar.Value;
            };

            if (StatusStrip.InvokeRequired)
            {
                StatusStrip.Invoke(updateAction);
            }
            else
            {
                updateAction.Invoke();
            }

            return finalValue;
        }

        public void SetUploadedCount(int uploaded, int total)
        {
            Action setCountAction = () =>
            {
                UploadedCount.Text = $"Uploaded {uploaded} of {total} suites";
            };

            if (StatusStrip.InvokeRequired)
            {
                StatusStrip.Invoke(setCountAction);
            }
            else
            {
                setCountAction.Invoke();
            }
        }

        public void SetTimeTaken(string str)
        {
            Action setTimeAction = () =>
            {
                TimeTaken.Text = str;
            };

            if (StatusStrip.InvokeRequired)
            {
                StatusStrip.Invoke(setTimeAction);
            }
            else
            {
                setTimeAction.Invoke();
            }
        }
    }
}
