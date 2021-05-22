using System;

using Microsoft.AspNetCore.Mvc;

namespace FluentWebApiIntegrationTestDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            return Ok();
        }
    }
}
