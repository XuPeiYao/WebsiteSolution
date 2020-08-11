using AutoMapper;

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
    public class WebsiteSolutionServices: IDisposable
    {
        [Dependency]
        public ObjectPool<WebsiteSolutionContext> ContextPool { get; set; }


        private WebsiteSolutionContext _context;        
        public WebsiteSolutionContext Context {
            get
            {
                if(_context != null)
                {
                    return _context;
                }
                return ContextPool.Get();
            }
        }


        [Dependency]
        public JwtHelper<DefaultJwtTokenModel> JwtHelper { get; set; }

        [Dependency]
        public IMapper Mapper { get; set; }

        public void Dispose()
        {
            if(_context!=null)
            {
                ContextPool.Return(_context);
                _context = null;
            }
        }
    }
}
