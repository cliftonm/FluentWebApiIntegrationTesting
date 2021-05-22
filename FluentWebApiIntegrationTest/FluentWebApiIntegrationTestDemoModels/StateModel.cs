using System;
using System.Collections.Generic;
using System.Linq;

using Clifton.Core.Assertions;

namespace FluentWebApiIntegrationTestDemoModels
{
    public class StateModelException : Exception
    {
        public StateModelException() { } 
        public StateModelException(string msg) : base(msg) { }
    }

    public class County : List<string> { }

    public class StateModel
    {
        protected Dictionary<string, County> stateCounties = new Dictionary<string, County>();

        public IEnumerable<string> GetStates()
        {
            var ret = stateCounties.Select(kvp => kvp.Key);

            return ret;
        }

        public void AddState(string stateName)
        {
            Assertion.That<StateModelException>(!stateCounties.ContainsKey(stateName), "State already exists.");

            stateCounties[stateName] = new County();
        }

        public void AddCounty(string stateName, string countyName)
        {
            Assertion.That<StateModelException>(stateCounties.ContainsKey(stateName), "State does not exists.");
            Assertion.That<StateModelException>(!stateCounties[stateName].Contains(countyName), "County already exists.");

            stateCounties[stateName].Add(countyName);
        }
    }
}
