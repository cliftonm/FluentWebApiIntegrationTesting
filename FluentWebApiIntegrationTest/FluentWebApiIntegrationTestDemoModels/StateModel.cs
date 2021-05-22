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
        // Public for serialization
        public Dictionary<string, County> StateCounties { get; set; } = new Dictionary<string, County>();

        public IEnumerable<string> GetStates()
        {
            var ret = StateCounties.Select(kvp => kvp.Key);

            return ret;
        }

        public IEnumerable<string> GetCounties(string stateName)
        {
            Assertion.That<StateModelException>(StateCounties.ContainsKey(stateName), "State does not exist.");

            return StateCounties[stateName];
        }

        public void AddState(string stateName)
        {
            Assertion.That<StateModelException>(!StateCounties.ContainsKey(stateName), "State already exists.");

            StateCounties[stateName] = new County();
        }

        public void AddCounty(string stateName, string countyName)
        {
            Assertion.That<StateModelException>(StateCounties.ContainsKey(stateName), "State does not exists.");
            Assertion.That<StateModelException>(!StateCounties[stateName].Contains(countyName), "County already exists.");

            StateCounties[stateName].Add(countyName);
        }
    }
}
