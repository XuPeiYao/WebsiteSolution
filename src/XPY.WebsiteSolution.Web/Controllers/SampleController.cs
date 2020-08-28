using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 

using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using XPY.WebsiteSolution.Application;
using XPY.WebsiteSolution.Models;
using XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Autofac;
using XPY.WebsiteSolution.Utilities.Token;

namespace XPY.WebsiteSolution.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SampleController : ControllerBase
    { 
        [Dependency]
        public IMapper Mapper { get; set; }

        [Dependency]
        public IMediator Mediator { get; set; }

        [Dependency]
        public JwtHelper<DefaultJwtTokenModel> JwtHelper { get; set; }

        [HttpGet]
        [CallLog]
        public virtual async Task<SampleUser> Get()
        {
            var context = await Mediator.Send(new WebsiteSolutionRequest());

            return Mapper.Map<SampleUser>(new SampleUserModel()
            {
                UserId = await Mediator.Send(new SampleRequest() { Text = "XXXXX" })
            });
        }
    }
}
