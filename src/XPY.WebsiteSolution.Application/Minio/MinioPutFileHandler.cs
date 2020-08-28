using MediatR;

using Microsoft.Extensions.Options;

using Minio;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Autofac;

namespace XPY.WebsiteSolution.Application.Minio
{
    public class MinioPutFileRequest: IRequest<string>
    {
        public string ObjectName { get; set; }
        public string ContentType { get; set; }
        public Stream FileStream { get; set; }

        public byte[] Binary {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                FileStream = new MemoryStream(value);
            }
        }
    }
     
    public class MinioPutFileHandler : IRequestHandler<MinioPutFileRequest, string>
    {
        public MinioClient Client { get; set; }

        public IOptions<MinioOptions> Options { get; set; }

        public MinioPutFileHandler(
            MinioClient client,
            IOptions<MinioOptions> options)
        {
            Client = client;
            Options = options;
        }

        public async Task<string> Handle(MinioPutFileRequest request, CancellationToken cancellationToken)
        {
            await Client.PutObjectAsync(
                Options.Value.BucketName,
                request.ObjectName,
                request.FileStream,
                request.FileStream.Length,
                request.ContentType);

            if (request.ObjectName.StartsWith("shared_"))
            {
                var httpSchema = Options.Value.WithSSL ? "https" : "http";
                return $"{httpSchema}://{Options.Value.EndPoint}/{Options.Value.BucketName}/{request.ObjectName}";
            }

            return await Client.PresignedGetObjectAsync(Options.Value.BucketName, request.ObjectName, 60 * 60 * 24);
        }
    }
}
