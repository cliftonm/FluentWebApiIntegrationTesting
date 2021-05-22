using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

using FluentAssertions;

using Clifton.IntegrationTestWorkflowEngine;

using WorkflowTestMethods;
using FluentWebApiIntegrationTestDemoModels;

namespace IntegrationTests
{
    [TestClass]
    public class DemoTests
    {
        [TestMethod]
        public void GetTest()
        {
            string baseUrl = "http://localhost/FluentWebApiIntegrationtestDemo";

            new WorkflowPacket(baseUrl)
                .Home("Demo")
                .IShouldSeeOKResponse();
        }

        [TestMethod]
        public void FactorialTest()
        {
            string baseUrl = "http://localhost/FluentWebApiIntegrationtestDemo";

            new WorkflowPacket(baseUrl)
                .Factorial<FactorialResult>("factResult", 6)
                .IShouldSeeOKResponse()
                .ThenIShouldSee<FactorialResult>("factResult", r => r.Result.Should().Be(720));
                // .ThenIShouldSee("factResult", r => r.Result == 720M);
        }

        [TestMethod]
        public void BadFactorialTest()
        {
            string baseUrl = "http://localhost/FluentWebApiIntegrationtestDemo";

            new WorkflowPacket(baseUrl)
                .Factorial<FactorialResult>("factResult", -1)
                .IShouldSeeBadRequestResponse();
        }

        [TestMethod]
        public void AddStateTest()
        {
            string baseUrl = "http://localhost/FluentWebApiIntegrationtestDemo";

            new WorkflowPacket(baseUrl)
                .AddState("NY")
                .IShouldSeeNoContentResponse()
                .AddState("CT")
                .IShouldSeeNoContentResponse()
                .GetStatesAndCounties("myStates")
                .IShouldSeeNoContentResponse()
                .ThenIShouldSee<StateModel>("myStates", m => m.GetStates().Count().Should().Be(2));
        }

        [TestMethod]
        public void AddDuplicateStateTest()
        {
            string baseUrl = "http://localhost/FluentWebApiIntegrationtestDemo";

            new WorkflowPacket(baseUrl)
                .AddState("NY")
                .IShouldSeeNoContentResponse()
                .AddState("NY")
                .IShouldSeeBadRequestResponse();
        }

        [TestMethod]
        public void AddCountyTest()
        {
            string baseUrl = "http://localhost/FluentWebApiIntegrationtestDemo";

            new WorkflowPacket(baseUrl)
                .AddState("NY")
                .IShouldSeeNoContentResponse()
                .AddCounty("NY", "Columbia")
                .GetStatesAndCounties("myStates")
                .IShouldSeeNoContentResponse()
                .ThenIShouldSee<StateModel>("myStates", m => m.GetStates().Count().Should().Be(1))
                .ThenIShouldSee<StateModel>("myStates", m => m.GetStates().First().Count().Should().Be(1));
        }

        [TestMethod]
        public void AddCountyNoStateTest()
        {
            string baseUrl = "http://localhost/FluentWebApiIntegrationtestDemo";

            new WorkflowPacket(baseUrl)
                .AddCounty("NY", "Columbia")
                .IShouldSeeBadRequestResponse();
        }

        [TestMethod]
        public void AddDuplicateCountyTest()
        {
            string baseUrl = "http://localhost/FluentWebApiIntegrationtestDemo";

            new WorkflowPacket(baseUrl)
                .AddState("NY")
                .IShouldSeeNoContentResponse()
                .AddCounty("NY", "Columbia")
                .IShouldSeeNoContentResponse()
                .AddCounty("NY", "Columbia")
                .IShouldSeeBadRequestResponse();
        }
    }
}
