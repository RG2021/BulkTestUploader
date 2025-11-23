using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Test.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCase = Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestCase;

namespace BulkTestUploader.Service
{
    public class DevopsService
    {
        private readonly string _baseUrl = "https://dev.azure.com/";
        private readonly string _apiVersion = "6.0";
        private readonly string _patToken = string.Empty;
        private readonly string _orgName = string.Empty;
        private readonly string _orgUrl = string.Empty;
        public DevopsService(string patToken, string organization)
        {
            this._patToken = patToken;
            this._orgName = organization;
            this._orgUrl = $"{_baseUrl}{organization}";
        }
        public List<TeamProjectReference> GetProjects()
        {
            VssBasicCredential creds = new VssBasicCredential(string.Empty, _patToken);

            using (VssConnection connection = new VssConnection(new Uri(_orgUrl), creds))
            {
                ProjectHttpClient projectClient = connection.GetClient<ProjectHttpClient>();
                IPagedList<TeamProjectReference> projects = projectClient.GetProjects().Result;
                return projects.ToList();
            }
        }

        public List<TestPlan> GetTestPlans(string projectId)
        {
            VssBasicCredential creds = new VssBasicCredential(string.Empty, _patToken);
            using (VssConnection connection = new VssConnection(new Uri(_orgUrl), creds))
            {
                TestPlanHttpClient testClient = connection.GetClient<TestPlanHttpClient>();
                List<TestPlan> testPlans = testClient.GetTestPlansAsync(projectId).Result;
                return testPlans;
            }
        }

        public List<TestSuite> GetTestSuites(string projectId, int planId)
        {
            VssBasicCredential creds = new VssBasicCredential(string.Empty, _patToken);
            using (VssConnection connection = new VssConnection(new Uri(_orgUrl), creds))
            {
                TestPlanHttpClient testClient = connection.GetClient<TestPlanHttpClient>();
                List<TestSuite> testSuites = testClient.GetTestSuitesForPlanAsync(projectId, planId, asTreeView:true).Result;
                return testSuites;
            }
        }

        public List<TestCase> CreateTestCases(string projectId, int planId, int suiteId, List<SuiteTestCaseCreateUpdateParameters> testCases)
        {
            VssBasicCredential creds = new VssBasicCredential(string.Empty, _patToken);
            using (VssConnection connection = new VssConnection(new Uri(_orgUrl), creds))
            {
                TestPlanHttpClient testClient = connection.GetClient<TestPlanHttpClient>();
                List<TestCase> createdTestCases = testClient.AddTestCasesToSuiteAsync(testCases, projectId, planId, suiteId).Result;
                return createdTestCases;
            }
        }
    }
}
