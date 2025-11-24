using BulkTestUploader.Forms;
using BulkTestUploader.Model;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BulkTestUploader.Helper.Helper;

namespace BulkTestUploader.Control
{
    public class InputControl : BaseControl
    {
        TableLayoutPanel TableLayoutPanel { get; set; }
        MaskedTextBox? PatTokenTextBox { get; set; }
        TextBox? OrganizationTextBox { get; set; }
        Button? ValidateTokenButton { get; set; }
        ComboBox? ProjectComboBox { get; set; }
        ComboBox? TestPlanComboBox { get; set; }
        Button? SelectSuiteButton { get; set; }

        private string patToken = string.Empty;
        private string orgName = string.Empty;

        public InputControl(TableLayoutPanel tableLayoutPanel)
        {
            TableLayoutPanel = tableLayoutPanel;
            PatTokenTextBox = tableLayoutPanel.Controls.Find("patTokenMaskedTextBox", true).FirstOrDefault() as MaskedTextBox;
            OrganizationTextBox = tableLayoutPanel.Controls.Find("orgNameTextBox", true).FirstOrDefault() as TextBox;
            ValidateTokenButton = tableLayoutPanel.Controls.Find("validateButton", true).FirstOrDefault() as Button;
            ProjectComboBox = tableLayoutPanel.Controls.Find("projectComboBox", true).FirstOrDefault() as ComboBox;
            TestPlanComboBox = tableLayoutPanel.Controls.Find("planComboBox", true).FirstOrDefault() as ComboBox;
            SelectSuiteButton = tableLayoutPanel.Controls.Find("suiteButton", true).FirstOrDefault() as Button;

            ValidateTokenButton!.Click += ValidateTokenButton_Click;
            ProjectComboBox!.SelectedIndexChanged += ProjectSelected;
            SelectSuiteButton!.Click += SelectSuiteButton_Click;

        }
        private void ValidateTokenButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (ValidatePATToken(OrganizationTextBox?.Text ?? "", PatTokenTextBox?.Text ?? ""))
                {
                    patToken = PatTokenTextBox?.Text ?? "";
                    orgName = OrganizationTextBox?.Text ?? "";

                    Service.DevopsService newService = new Service.DevopsService(patToken, orgName);
                    LoadProjects(newService);
                }
                else
                {
                    Logger?.Log("PAT token and Organization Name are required.");
                }
            }
            catch (Exception ex)
            {
                Logger?.Log($"Error validating PAT token: {ex.Message}");
            }
        }

        private void LoadProjects(Service.DevopsService service)
        {
            List<TeamProjectReference>? projects = service?.GetProjects();
            if (projects != null && ProjectComboBox != null)
            {
                ProjectComboBox.Items.Clear();
                foreach (TeamProjectReference project in projects)
                {
                    ProjectComboBox.Items.Add(new ComboItem
                    {
                        Name = project.Name,
                        Id = project.Id.ToString()
                    });
                }

                Logger?.Log("PAT token validated successfully.");
                Logger?.Log("Projects loaded successfully.");

                DevopsService = service;

                if (projects.Count == 1)
                {
                    ProjectComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                Logger?.Log("Failed to load projects.");
            }
        }

        private void ProjectSelected(object? sender, EventArgs e)
        {
            ComboItem selectedProject = (ComboItem)ProjectComboBox!.SelectedItem!;
            List<TestPlan>? testPlans = DevopsService?.GetTestPlans(selectedProject.Id);

            LoadTestPlans(testPlans);
        }

        private void LoadTestPlans(List<TestPlan>? testPlans)
        {
            if (testPlans != null && TestPlanComboBox != null)
            {
                TestPlanComboBox.Items.Clear();
                foreach (TestPlan plan in testPlans)
                {
                    TestPlanComboBox.Items.Add(new ComboItem
                    {
                        Name = plan.Name,
                        Id = plan.Id.ToString()
                    });
                }

                Logger?.Log("Test Plans loaded successfully.");
                if (testPlans.Count == 1)
                {
                    TestPlanComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                Logger?.Log("Failed to load Test Plans.");
            }
        }

        private void TestPlanSelected(object? sender, EventArgs e)
        {
            ComboItem selectedProject = (ComboItem)ProjectComboBox!.SelectedItem!;
            ComboItem selectedPlan = (ComboItem)TestPlanComboBox!.SelectedItem!;

            List<TestSuite>? testSuites = DevopsService?.GetTestSuites(selectedProject.Id, int.Parse(selectedPlan.Id));

            if (testSuites != null)
            {
                Logger?.Log($"Loaded test suites for Plan: {selectedPlan.Name}");
            }
            else
            {
                Logger?.Log("Failed to load test suites.");
            }
        }

        private void SelectSuiteButton_Click(object? sender, EventArgs e)
        {
            ComboItem selectedProject = (ComboItem)ProjectComboBox!.SelectedItem!;
            ComboItem selectedPlan = (ComboItem)TestPlanComboBox!.SelectedItem!;

            if(selectedProject == null || selectedPlan == null)
            {
                Logger?.Log("Please select a Project and Test Plan first.");
                return;
            }

            List<TestSuite>? testSuites = DevopsService?.GetTestSuites(selectedProject.Id, int.Parse(selectedPlan.Id));

            if (testSuites != null)
            {
                using (SuitesForm suitesForm = new SuitesForm(testSuites))
                {
                    Logger?.Log("Test Suites loaded into tree view.");
                    if (suitesForm.ShowDialog() == DialogResult.OK)
                    {
                        List<Suite> selectedSuites = suitesForm.GetCheckedSuites();
                        SelectSuiteButton.Text = $"{selectedSuites.Count} Suites Selected";
                        SuitesGridControl?.LoadSuites(selectedSuites);
                    }
                }
            }
            else
            {
                Logger?.Log("Failed to load Test Suites.");
            }
        }

        public string GetProjectName()
        {
            ComboItem selectedProject = (ComboItem)ProjectComboBox!.SelectedItem!;
            return selectedProject.Name;
        }

        public Guid GetProjectId()
        {
            ComboItem selectedProject = (ComboItem)ProjectComboBox!.SelectedItem!;
            return Guid.Parse(selectedProject.Id);
        }

        public int GetTestPlanId()
        {
            ComboItem selectedPlan = (ComboItem)TestPlanComboBox!.SelectedItem!;
            return int.Parse(selectedPlan.Id);
        }

        public void SetSuiteButtonText(string text)
        {
            SelectSuiteButton!.Text = text;
        }


    }
}
