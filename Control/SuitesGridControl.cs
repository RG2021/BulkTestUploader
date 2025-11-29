using BulkTestUploader.Model;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkTestUploader.Control
{
    public class SuitesGridControl : BaseControl
    {
        private readonly DataGridView suitesGridView;
        private BindingList<GridSuite> suiteBindingList;
        private BindingSource suiteBindingSource;
        public SuitesGridControl(DataGridView dataGridView)
        {
            suitesGridView = dataGridView;

            suiteBindingList = new BindingList<GridSuite>();
            suiteBindingSource = new BindingSource();
            suiteBindingSource.DataSource = suiteBindingList;

            suitesGridView.DataSource = suiteBindingSource;

            suitesGridView.CellContentClick += SuitesGridView_CellContentClick;
        }

        public void LoadSuites(List<GridSuite> testSuites)
        {
            suiteBindingList.Clear();
            foreach (GridSuite suite in testSuites)
            {
                suiteBindingList.Add(suite);
            }
            InputControl?.SetSuiteButtonText($"{testSuites.Count} Suites Selected");
        }

        public BindingList<GridSuite> GetSuiteList()
        {
            return suiteBindingList;
        }

        private void SuitesGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (suitesGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Select Excel File";
                    ofd.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        if (suitesGridView.Rows[e.RowIndex].DataBoundItem is GridSuite suite)
                        {
                            suite.FilePath = ofd.FileName;
                        }
                    }
                }
            }
        }
    }
}
