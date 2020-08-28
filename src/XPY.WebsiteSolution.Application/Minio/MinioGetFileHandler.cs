using MediatR;

using Microsoft.Extensions.Options;

using Minio;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Autofac;

namespace XPY.WebsiteSolution.Application.Minio
{
    public class MinioGetFileRequest : IRequest<string>
    {
        public string ObjectName { get; set; }
    }

    public class MinioGetFileHandler : IRequestHandler<MinioGetFileRequest, string>
    {
        public MinioClient Client { get; set; }

        public IOptions<MinioOptions> Options { get; set; }

        public MinioGetFileHandler(
            MinioClient client,
            IOptions<MinioOptions> options)
        {
            Client = client;
            Options = options;
        }

        public async Task<string> Handle(MinioGetFileRequest request, CancellationToken cancellationToken)
        {
            if (request.ObjectName.StartsWith("shared_"))
            {
                var httpSchema = Options.Value.WithSSL ? "https" : "http";
                return $"{httpSchema}://{Options.Value.EndPoint}/{Options.Value.BucketName}/{request.ObjectName}";
            }

            return await Client.PresignedGetObjectAsync(Options.Value.BucketName, request.ObjectName, 60 * 60 * 24);
        }
    }
}
