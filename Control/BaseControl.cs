using BulkTestUploader.Service;
using System;
using System.Windows.Forms;

namespace BulkTestUploader.Control
{
    public partial class BaseControl : Form
    {
        protected static DevopsService DevopsService;
        protected static LogsControl Logger;
        protected static InputControl InputControl;
        protected static SuitesGridControl SuitesGridControl;
        protected static ActionsControl ActionsControl;
        protected static StatusBarControl StatusBarControl;

        public BaseControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeCustomControls();
        }

        private void InitializeCustomControls()
        {
            Logger = new LogsControl(logsTextBox);
            InputControl = new InputControl(inputTableLayoutPanel);
            SuitesGridControl = new SuitesGridControl(suitesGrid);
            StatusBarControl = new StatusBarControl(statusBarStrip);
            ActionsControl = new ActionsControl(actionControls, timerControl);
        }
    }
}
