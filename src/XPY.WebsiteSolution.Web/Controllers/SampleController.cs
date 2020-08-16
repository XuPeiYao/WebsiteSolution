using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using Microsoft.AspNetCore.Mvc; 

using XPY.WebsiteSolution.Models;
using XPY.WebsiteSolution.Services;
using XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Autofac;

namespace XPY.WebsiteSolution.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SampleController : ControllerBase
    {
        [Dependency]
        public WebsiteSolutionServices Context { get; set; }
        
        [HttpGet]
        [CallLogAttribute]
        public virtual SampleUser Get()
        {
            return Context.Mapper.Map<SampleUser>(new SampleUserModel()
            {
                UserId = "xxxxx"
            }); 
        }
    }
}
