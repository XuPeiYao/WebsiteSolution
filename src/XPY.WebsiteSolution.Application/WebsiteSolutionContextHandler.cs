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
    public class WebsiteSolutionContextRequest: IRequest<WebsiteSolutionContext>
    {

    }

    public class WebsiteSolutionContextHandler :
        IRequestHandler<WebsiteSolutionContextRequest, WebsiteSolutionContext>
    {
        public WebsiteSolutionContext Context { get; set; }

        public WebsiteSolutionContextHandler(
            WebsiteSolutionContext context)
        {
            Context = context;
        }

        public async Task<WebsiteSolutionContext> Handle(WebsiteSolutionContextRequest request, CancellationToken cancellationToken)
        {
            return Context;
        }
    }
}
