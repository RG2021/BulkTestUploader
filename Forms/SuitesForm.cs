using BulkTestUploader.Control;
using BulkTestUploader.Model;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BulkTestUploader.Helper.Helper;

namespace BulkTestUploader.Forms
{
    public partial class SuitesForm : Form
    {
        public SuitesForm(List<TestSuite> testSuites)
        {
            InitializeComponent();
            LoadSuitesToTree(testSuites);
            suitesTreeView.AfterCheck += AfterCheck;
        }

        private void AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode childNode in e.Node.Nodes)
                {
                    childNode.Checked = e.Node.Checked;
                }
            }
        }

        private void LoadSuitesToTree(List<TestSuite> suites)
        {
            suitesTreeView.Nodes.Clear();
            void AddNodes(TreeNode parent, List<TestSuite> children, string parentPath)
            {
                foreach (TestSuite child in children)
                {
                    GridSuite suite = new GridSuite
                    {
                        SuiteId = child.Id.ToString(),
                        SuiteName = child.Name,
                        SuitePath = parentPath,
                    };

                    TreeNode node = new TreeNode(child.Name)
                    {
                        Tag = suite,
                        Checked = false
                    };

                    parent.Nodes.Add(node);

                    if (child.HasChildren)
                    {
                        string currentPath = string.IsNullOrEmpty(parentPath) ? child.Name : $"{parentPath}/{child.Name}";
                        AddNodes(node, child.Children, currentPath);
                    }
                }
            }

            AddNodes(suitesTreeView.Nodes.Add("Test Suites"), suites, "");
            
        }

        public List<GridSuite> GetCheckedSuites()
        {
            List<GridSuite> result = [];

            void Collect(TreeNode node)
            {
                if (node.Checked && node.Tag is GridSuite suiteInfo)
                {
                    result.Add(suiteInfo);
                }

                foreach (TreeNode child in node.Nodes)
                {
                    Collect(child);
                }
            }

            foreach (TreeNode root in suitesTreeView.Nodes)
            {
                Collect(root);
            }

            return result;
        }
    }
}
