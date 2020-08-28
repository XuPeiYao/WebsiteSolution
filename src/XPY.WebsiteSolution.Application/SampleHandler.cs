using MediatR;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XPY.WebsiteSolution.Application
{
    public class SampleRequest : IRequest<string> {
        public string Text { get; set; }
    }

    public class SampleHandler : IRequestHandler<SampleRequest, string>
    {
        public async Task<string> Handle(SampleRequest request, CancellationToken cancellationToken)
        {
            return request.Text;
        }
    }
}
