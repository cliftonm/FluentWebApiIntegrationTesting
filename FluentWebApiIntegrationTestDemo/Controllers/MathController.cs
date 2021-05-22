using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Clifton.Core.ExtensionMethods;

using FluentWebApiIntegrationTestDemoModels;

namespace FluentWebApiIntegrationTestDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        [HttpGet("Factorial")]
        public object Factorial([FromQuery, BindRequired] int n)
        {
            object ret;

            if (n <= 0)
            {
                ret = BadRequest("Value must be >= 1");
            }
            else
            {
                decimal factorial = 1;
                n.ForEach(i => factorial = factorial * i, 1);

                ret = Ok(new FactorialResult() { Result = factorial });
            }

            return ret;
        }
    }
}
