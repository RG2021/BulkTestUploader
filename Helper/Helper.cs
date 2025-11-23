using BulkTestUploader.Model;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkTestUploader.Helper
{
    public class Helper
    {
        public static bool ValidatePATToken(string organization, string patToken)
        {
            return !string.IsNullOrEmpty(organization) && !string.IsNullOrEmpty(patToken);
        }

        //public static void LoadSuitesToTree(TreeView treeView, List<TestSuite> suites)
        //{
        //    treeView.Nodes.Clear();
        //    void AddNodes(TreeNode parent, List<TestSuite> children, string parentPath)
        //    {
        //        foreach (TestSuite child in children)
        //        {
        //            Suite suite = new Suite
        //            {
        //                SuiteId = child.Id.ToString(),
        //                SuiteName = child.Name,
        //                SuitePath = parentPath,
        //            };

        //            TreeNode node = new TreeNode(child.Name)
        //            {
        //                Tag = suite,
        //                Checked = false
        //            };

        //            parent.Nodes.Add(node);

        //            if (child.HasChildren)
        //            {
        //                string currentPath = string.IsNullOrEmpty(parentPath) ? child.Name : $"{parentPath}/{child.Name}";
        //                AddNodes(node, child.Children, currentPath);
        //            }
        //        }
        //    }

        //    AddNodes(treeView.Nodes.Add("Test Suites"), suites, "");
        //}
    }
}
