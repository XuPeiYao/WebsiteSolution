using System;
using System.Collections.Generic;
using System.Text;

namespace XPY.WebsiteSolution.Application.Minio
{
    public class MinioOptions
    {
        public string BucketName { get; set; }
        public bool WithSSL { get; set; }
        public string EndPoint { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
