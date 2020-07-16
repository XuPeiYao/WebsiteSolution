using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

using Unity;

using XPY.WebsiteSolution.Database;

namespace XPY.WebsiteSolution.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]     
    public class SampleController : ControllerBase
    {
        [Dependency]
        public SampleContext Context { get; set; }

        [HttpGet]
        public IEnumerable<object> Get()
        {
            return new object[] { "A", "B", "C", new { D="D" } };
        }
    }
}
