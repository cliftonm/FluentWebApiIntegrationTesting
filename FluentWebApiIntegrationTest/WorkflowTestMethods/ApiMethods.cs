using System;
using System.Net;

using FluentAssertions;

using Clifton.IntegrationTestWorkflowEngine;

using FluentWebApiIntegrationTestDemoModels;

namespace WorkflowTestMethods
{
    public static class ApiMethods
    {
        public static WorkflowPacket Home(this WorkflowPacket wp, string controller)
        {
            var resp = RestService.Get($"{wp.BaseUrl}/{controller}");
            wp.LastResponse = resp.status;

            return wp; 
        }

        public static WorkflowPacket Factorial<T>(this WorkflowPacket wp, string containerName, int n) where T: new()
        {
            var resp = RestService.Get<T>($"{wp.BaseUrl}/Math/Factorial?n={n}");
            wp.LastResponse = resp.status;
            wp.Container[containerName] = resp.item;

            return wp;
        }

        public static WorkflowPacket GetStatesAndCounties(this WorkflowPacket wp, string containerName)
        {
            var resp = RestService.Get<StateModel>($"{wp.BaseUrl}/State");
            wp.LastResponse = resp.status;
            wp.Container[containerName] = resp.item;

            return wp;
        }

        public static WorkflowPacket AddState(this WorkflowPacket wp, string stateName)
        {
            var resp = RestService.Post($"{wp.BaseUrl}/State", new { stateName });
            wp.LastResponse = resp.status;

            return wp;
        }

        public static WorkflowPacket AddCounty(this WorkflowPacket wp, string stateName, string countyName)
        {
            var resp = RestService.Post($"{wp.BaseUrl}/State/County", new { stateName, countyName });
            wp.LastResponse = resp.status;

            return wp;
        }

        public static WorkflowPacket IShouldSeeOKResponse(this WorkflowPacket wp)
        {
            wp.LastResponse.Should().Be(HttpStatusCode.OK);

            return wp;
        }

        public static WorkflowPacket IShouldSeeNoContentResponse(this WorkflowPacket wp)
        {
            wp.LastResponse.Should().Be(HttpStatusCode.NoContent);

            return wp;
        }

        public static WorkflowPacket IShouldSeeBadRequestResponse(this WorkflowPacket wp)
        {
            wp.LastResponse.Should().Be(HttpStatusCode.BadRequest);

            return wp;
        }

        public static WorkflowPacket ThenIShouldSee<T>(this WorkflowPacket wp, string containerName, Action<T> test) where T : class
        {
            T obj = wp.GetObject<T>(containerName);
            test(obj);

            return wp;
        }

        public static WorkflowPacket ThenIShouldSee(this WorkflowPacket wp, string containerName, Func<dynamic, bool> test)
        {
            var obj = wp.GetObject(containerName);
            bool b = test(obj);
            b.Should().BeTrue();

            return wp;
        }
    }
}
