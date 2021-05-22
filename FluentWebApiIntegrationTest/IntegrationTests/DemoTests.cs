using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

using FluentAssertions;

using Clifton.IntegrationTestWorkflowEngine;

using WorkflowTestMethods;
using FluentWebApiIntegrationTestDemoModels;

namespace IntegrationTests
{
    [TestClass]
    public class DemoTests : Setup
    {
        private WorkflowPacket wp;

        [TestInitialize]
        public void InitializeTest()
        {
            wp = new WorkflowPacket(baseUrl)
                .CleanupStateTestData()
                .IShouldSeeOKResponse();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            wp.PrintLog();
        }

        [TestMethod]
        public void GetTest()
        {
            wp
                .Home("Demo")
                .IShouldSeeOKResponse();
        }

        [TestMethod]
        public void FactorialTest()
        {
            wp
                .Factorial<FactorialResult>("factResult", 6)
                .IShouldSeeOKResponse()
                .ThenIShouldSee<FactorialResult>("factResult", r => r.Result.Should().Be(720));
                // .ThenIShouldSee("factResult", r => r.Result == 720M);
        }

        [TestMethod]
        public void BadFactorialTest()
        {
            wp
                .Factorial<FactorialResult>("factResult", -1)
                .IShouldSeeBadRequestResponse();
        }

        [TestMethod]
        public void AddStateTest()
        {
            wp
                .AddState("NY")
                .IShouldSeeNoContentResponse()
                .AddState("CT")
                .IShouldSeeNoContentResponse()
                .GetStatesAndCounties("myStates")
                .IShouldSeeOKResponse()
                .ThenIShouldSee<StateModel>("myStates", m => m.GetStates().Count().Should().Be(2));
        }

        [TestMethod]
        public void AddDuplicateStateTest()
        {
            wp
                .AddState("NY")
                .IShouldSeeNoContentResponse()
                .AddState("NY")
                .IShouldSeeBadRequestResponse();
        }

        [TestMethod]
        public void AddCountyTest()
        {
            wp
                .AddState("NY")
                .IShouldSeeNoContentResponse()
                .AddCounty("NY", "Columbia")
                .IShouldSeeNoContentResponse()
                .GetStatesAndCounties("myStates")
                .IShouldSeeOKResponse()
                .ThenIShouldSee<StateModel>("myStates", m => m.GetStates().Count().Should().Be(1))
                .ThenIShouldSee<StateModel>("myStates", m => m.GetStates().First().Should().Be("NY"))
                .ThenIShouldSee<StateModel>("myStates", m => m.GetCounties("NY").Count().Should().Be(1))
                .ThenIShouldSee<StateModel>("myStates", m => m.GetCounties("NY").First().Should().Be("Columbia"));
        }

        [TestMethod]
        public void AddCountyAndAutoCreateStateTest()
        {
            wp
                .AddCounty("NY", "Columbia")
                .IShouldSeeNoContentResponse()
                .GetStatesAndCounties("myStates")
                .IShouldSeeOKResponse()
                .ThenIShouldSee<StateModel>("myStates", m => m.GetStates().Count().Should().Be(1))
                .ThenIShouldSee<StateModel>("myStates", m => m.GetStates().First().Should().Be("NY"))
                .ThenIShouldSee<StateModel>("myStates", m => m.GetCounties("NY").Count().Should().Be(1))
                .ThenIShouldSee<StateModel>("myStates", m => m.GetCounties("NY").First().Should().Be("Columbia"));
        }

        [TestMethod]
        public void AddCountyNoStateTest()
        {
            wp
                .AddCounty("NY", "Columbia")
                .IShouldSeeBadRequestResponse();
        }

        [TestMethod]
        public void AddDuplicateCountyTest()
        {
            wp
                .AddState("NY")
                .IShouldSeeNoContentResponse()
                .AddCounty("NY", "Columbia")
                .IShouldSeeNoContentResponse()
                .AddCounty("NY", "Columbia")
                .IShouldSeeBadRequestResponse();
        }
    }
}
