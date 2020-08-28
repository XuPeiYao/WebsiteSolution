using MediatR;

using Microsoft.Extensions.Options;

using Minio;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XPY.WebsiteSolution.Application.Minio
{
    public class MinioRemoveFileRequest : IRequest
    {
        public string ObjectName { get; set; }
    }

    public class MinioRemoveFileHandler : IRequestHandler<MinioRemoveFileRequest>
    {
        public MinioClient Client { get; set; }

        public IOptions<MinioOptions> Options { get; set; }

        public MinioRemoveFileHandler(
            MinioClient client,
            IOptions<MinioOptions> options)
        {
            Client = client;
            Options = options;
        }

        public async Task<Unit> Handle(MinioRemoveFileRequest request, CancellationToken cancellationToken)
        {
            await Client.RemoveObjectAsync(
                Options.Value.BucketName,
                request.ObjectName);

            return Unit.Value;
        }
    }
}
