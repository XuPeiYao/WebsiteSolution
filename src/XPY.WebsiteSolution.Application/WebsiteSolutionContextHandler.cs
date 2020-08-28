using MediatR;

using Microsoft.Extensions.ObjectPool;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using XPY.WebsiteSolution.Database;
using XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Autofac;

namespace XPY.WebsiteSolution.Application
{
    public class WebsiteSolutionRequest: IRequest<WebsiteSolutionContext>
    {

    }

    public class WebsiteSolutionContextHandler :
        IRequestHandler<WebsiteSolutionRequest, WebsiteSolutionContext>,
        IDisposable
    {
        public ObjectPool<WebsiteSolutionContext> ContextPool { get; set; }

        private WebsiteSolutionContext _context;

        public WebsiteSolutionContextHandler(
            ObjectPool<WebsiteSolutionContext> contextPool)
        {
            ContextPool = contextPool;
        }

        public async Task<WebsiteSolutionContext> Handle(WebsiteSolutionRequest request, CancellationToken cancellationToken)
        {
            if (_context != null)
            {
                return _context;
            }
            _context = ContextPool.Get();
            return _context;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                ContextPool.Return(_context);
                _context = null;
            }
        }
    }
}
