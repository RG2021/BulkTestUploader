using BulkTestUploader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkTestUploader.Control
{
    public class ActionsControl : BaseControl
    {
        ToolStripButton UploadTestCases { get; set; }
        public ActionsControl(ToolStrip actionsControl)
        {
            UploadTestCases = actionsControl.Items["uploadTestCasesButton"] as ToolStripButton;
            UploadTestCases.Click += UploadTestCases_Click;
        }

        private void UploadTestCases_Click(object? sender, EventArgs e)
        {
            List<Suite> selectedSuites = SuitesGridControl?.GetSuiteList().ToList() ?? new List<Suite>();
            if (selectedSuites.Count == 0)
            {
                Logger?.Log("No test suites selected for upload.");
                return;
            }

            List<Suite> validSuites = selectedSuites.Where(suite => !string.IsNullOrEmpty(suite.FilePath)).ToList();
            if (validSuites.Count == 0)
            {
                Logger?.Log("No valid test suites with file paths found for upload.");
                return;
            }

            Logger?.Log($"Found {validSuites.Count} valid test suites for upload.");
        }
    }
}
