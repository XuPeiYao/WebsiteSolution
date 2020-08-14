﻿using AutoMapper;

using Microsoft.Extensions.ObjectPool;

using System;
using System.Collections.Generic;
using System.Text;

using XPY.WebsiteSolution.Database;
using XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Autofac;
using XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Injectable;
using XPY.WebsiteSolution.Utilities.Token;

namespace XPY.WebsiteSolution.Services
{
    [Injectable]
    public class WebsiteSolutionServices
    {
        [Dependency]
        public WebsiteSolutionContext Context { get; set; }

        [Dependency]
        public JwtHelper<DefaultJwtTokenModel> JwtHelper { get; set; }

        [Dependency]
        public IMapper Mapper { get; set; }
    }
}
