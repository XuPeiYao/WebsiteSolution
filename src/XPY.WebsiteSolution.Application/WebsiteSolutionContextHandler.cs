using MediatR;

using Microsoft.Extensions.ObjectPool;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using XPY.WebsiteSolution.Database;

namespace XPY.WebsiteSolution.Application
{
    public class WebsiteSolutionRequest: IRequest<WebsiteSolutionContext>
    {

    }

    public class WebsiteSolutionContextHandler :
        IRequestHandler<WebsiteSolutionRequest, WebsiteSolutionContext>
    {
        public WebsiteSolutionContext Context { get; set; }

        public WebsiteSolutionContextHandler(
            WebsiteSolutionContext context)
        {
            Context = context;
        }

        public async Task<WebsiteSolutionContext> Handle(WebsiteSolutionRequest request, CancellationToken cancellationToken)
        {
            return Context;
        }
    }
}
