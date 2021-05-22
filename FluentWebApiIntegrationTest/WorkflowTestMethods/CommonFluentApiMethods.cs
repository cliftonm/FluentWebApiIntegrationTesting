using System;
using System.Net;

using FluentAssertions;

using Clifton.IntegrationTestWorkflowEngine;

namespace WorkflowTestMethods
{
    public static partial class CommonFluentApiMethods
    {
        public static WorkflowPacket PrintLog(this WorkflowPacket wp)
        {
            wp.CallLog.ForEach(item => wp.Write(item));

            return wp;
        }

        public static WorkflowPacket Write(this WorkflowPacket wp, string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);

            return wp;
        }

        public static WorkflowPacket IShouldSeeOKResponse(this WorkflowPacket wp)
        {
            wp.Log("IShouldSeeOKResponse");
            wp.LastResponse.Should().Be(HttpStatusCode.OK, $"Did not expected {wp.LastContent}");

            return wp;
        }

        public static WorkflowPacket IShouldSeeNoContentResponse(this WorkflowPacket wp)
        {
            wp.Log("IShouldSeeNoContentResponse");
            wp.LastResponse.Should().Be(HttpStatusCode.NoContent, $"Did not expected {wp.LastContent}");

            return wp;
        }

        public static WorkflowPacket IShouldSeeBadRequestResponse(this WorkflowPacket wp)
        {
            wp.Log("IShouldSeeBadRequestResponse");
            wp.LastResponse.Should().Be(HttpStatusCode.BadRequest, $"Did not expected {wp.LastContent}");

            return wp;
        }

        public static WorkflowPacket ThenIShouldSee<T>(this WorkflowPacket wp, string containerName, Action<T> test) where T : class
        {
            wp.Log($"ThenIShouldSee in {containerName}");
            T obj = wp.GetObject<T>(containerName);
            test(obj);

            return wp;
        }

        public static WorkflowPacket ThenIShouldSee(this WorkflowPacket wp, string containerName, Func<dynamic, bool> test)
        {
            wp.Log($"ThenIShouldSee in {containerName}");
            var obj = wp.GetObject(containerName);
            bool b = test(obj);
            b.Should().BeTrue();

            return wp;
        }
    }
}
