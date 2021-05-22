using Clifton.IntegrationTestWorkflowEngine;

using FluentWebApiIntegrationTestDemoModels;

namespace WorkflowTestMethods
{
    public static class ApiMethods
    {
        public static WorkflowPacket Home(this WorkflowPacket wp, string controller)
        {
            wp.Log("Home");
            var resp = RestService.Get($"{wp.BaseUrl}/{controller}");
            wp.LastResponse = resp.status;
            wp.LastContent = resp.content;

            return wp; 
        }

        public static WorkflowPacket Factorial<T>(this WorkflowPacket wp, string containerName, int n) where T: new()
        {
            wp.Log("Factorial");
            var resp = RestService.Get<T>($"{wp.BaseUrl}/Math/Factorial?n={n}");
            wp.LastResponse = resp.status;
            wp.LastContent = resp.content;
            wp.Container[containerName] = resp.item;

            return wp;
        }

        public static WorkflowPacket GetStatesAndCounties(this WorkflowPacket wp, string containerName)
        {
            wp.Log("GetStatesAndCounties");
            var resp = RestService.Get<StateModel>($"{wp.BaseUrl}/State");
            wp.LastResponse = resp.status;
            wp.LastContent = resp.content;
            wp.Container[containerName] = resp.item;

            return wp;
        }

        public static WorkflowPacket AddState(this WorkflowPacket wp, string stateName)
        {
            wp.Log("AddState");
            var resp = RestService.Post($"{wp.BaseUrl}/State", new { stateName });
            wp.LastContent = resp.content;
            wp.LastResponse = resp.status;

            return wp;
        }

        public static WorkflowPacket AddCounty(this WorkflowPacket wp, string stateName, string countyName)
        {
            wp.Log("AddCounty");
            var resp = RestService.Post($"{wp.BaseUrl}/State/County", new { stateName, countyName });
            wp.LastContent = resp.content;
            wp.LastResponse = resp.status;

            return wp;
        }

        public static WorkflowPacket CleanupStateTestData(this WorkflowPacket wp)
        {
            wp.Log("CleanupStateTestData");
            var resp = RestService.Post($"{wp.BaseUrl}/State/CleanupTestData");
            wp.LastResponse = resp.status;
            wp.LastContent = resp.content;

            return wp;
        }
    }
}
