using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Clifton.Core.ExtensionMethods;

using FluentWebApiIntegrationTestDemoModels;

namespace FluentWebApiIntegrationTestDemo.Controllers
{
    public class StateCountyName
    {
        public string StateName { get; set; }
        public string CountyName { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class StateController : ControllerBase
    {
        public static StateModel stateModel = new StateModel();

        [HttpGet("")]
        public object GetStates()
        {
            var states = stateModel.GetStates();

            return Ok(states);
        }

        [HttpPost("")]
        public object AddState([FromBody] StateCountyName name)
        {
            object ret = Try<StateModelException>(
                NoContent(), 
                () => stateModel.AddState(name.StateName));

            return ret;
        }

        [HttpPost("County")]
        public object AddCounty(
            [FromBody] StateCountyName name)
        {
            object ret = Try<StateModelException>(
                NoContent(), 
                () => stateModel.AddCounty(name.StateName, name.CountyName));

            return ret;
        }

        private object Try<T>(object defaultReturn, Action action)
        {
            object ret = defaultReturn;

            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == typeof(T).Name)
                {
                    ret = BadRequest(ex.Message);
                }
                else
                {
                    throw;
                }
            }

            return ret;
        }
    }
}
