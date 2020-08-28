using MediatR;

using Microsoft.AspNetCore.Http;

using Minio;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XPY.WebsiteSolution.Application.Minio
{
    public class MinioPutFileAndGetObjectNameRequest : IRequest<(string url, string objectName)>
    {
        public IFormFile UploadFile { get; set; }
        public bool Shared { get; set; }
    }

    public class MinioPutFileAndGetObjectNameHandler : IRequestHandler<MinioPutFileAndGetObjectNameRequest, (string url, string objectName)>
    {
        public IMediator Mediator { get; set; }

        public MinioPutFileAndGetObjectNameHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<(string url, string objectName)> Handle(MinioPutFileAndGetObjectNameRequest request, CancellationToken cancellationToken)
        {
            var objectName = (request.Shared ? "shared_" : "") + Guid.NewGuid();

            return (await Mediator.Send(new MinioPutFileRequest()
            { 
                ObjectName = objectName,
                ContentType = request.UploadFile.ContentType,
                FileStream = request.UploadFile.OpenReadStream()
            }), objectName); 
        }
    }
}
