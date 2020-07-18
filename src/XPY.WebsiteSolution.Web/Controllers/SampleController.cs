using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using Microsoft.AspNetCore.Mvc; 

using Unity;

using XPY.WebsiteSolution.Models;
using XPY.WebsiteSolution.Services;

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
        public SampleUser Get()
        {
            return Context.Mapper.Map<SampleUser>(new SampleUserModel()
            {
                UserId = "xxxxx"
            }); 
        }
    }
}
