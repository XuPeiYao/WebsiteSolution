using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using Minio;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Injectable;

namespace XPY.WebsiteSolution.Services
{
    public class MinioOptions
    {
        public string BucketName { get; set; }
        public bool WithSSL { get; set; }
        public string EndPoint { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }

    [Injectable]
    public class MinioService
    {
        public MinioClient MinioClient { get; private set; }
        public MinioOptions Options { get; private set; }

        public MinioService(IOptions<MinioOptions> options)
        {
            Options = options.Value;
            MinioClient = new MinioClient(Options.EndPoint, Options.AccessKey, Options.SecretKey);

            if (Options.WithSSL)
            {
                MinioClient = MinioClient.WithSSL();
            }

            if (!MinioClient.BucketExistsAsync(Options.BucketName).GetAwaiter().GetResult())
            {
                MinioClient.MakeBucketAsync(Options.BucketName).GetAwaiter().GetResult();
                MinioClient.SetPolicyAsync(
                    Options.BucketName,
                    "{\"Version\":\"2012-10-17\",\"Statement\":[{\"Effect\":\"Allow\",\"Principal\":{\"AWS\":[\"*\"]},\"Action\":[\"s3:GetBucketLocation\"],\"Resource\":[\"arn:aws:s3:::" + Options.BucketName + "\"]},{\"Effect\":\"Allow\",\"Principal\":{\"AWS\":[\"*\"]},\"Action\":[\"s3:ListBucket\"],\"Resource\":[\"arn:aws:s3:::" + Options.BucketName + "\"],\"Condition\":{\"StringEquals\":{\"s3:prefix\":[\"shared_\"]}}},{\"Effect\":\"Allow\",\"Principal\":{\"AWS\":[\"*\"]},\"Action\":[\"s3:GetObject\"],\"Resource\":[\"arn:aws:s3:::" + Options.BucketName + "/shared_*\"]}]}"
                ).GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="uploadFile">上傳檔案</param>
        /// <param name="shared">是否上傳</param>
        /// <param name="objectName">物件名稱</param>
        /// <returns>檔案網址</returns>
        public string PutFileAndGetObjectName(IFormFile uploadFile, out string objectName, bool shared = true)
        {
            objectName = (shared ? "shared_" : "") + Guid.NewGuid();
            return PutFile(objectName, uploadFile.ContentType, uploadFile.OpenReadStream()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="uploadFile">上傳檔案</param>
        /// <param name="shared">是否上傳</param>
        /// <returns>檔案網址</returns>
        public async Task<string> PutFile(IFormFile uploadFile, bool shared = true)
        {
            return await PutFile((shared ? "shared_" : "") + Guid.NewGuid(), uploadFile.ContentType, uploadFile.OpenReadStream());
        }

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="objectName">檔案名稱(如為"shared_"開頭則為公開存取檔案)</param>
        /// <param name="contentType">ContentType</param>
        /// <param name="uploadFile">上傳檔案</param>
        /// <returns>檔案網址</returns>
        public async Task<string> PutFile(string objectName, string contentType, IFormFile uploadFile)
        {
            return await PutFile(objectName, uploadFile.ContentType, uploadFile.OpenReadStream());
        }

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="objectName">檔案名稱(如為"shared_"開頭則為公開存取檔案)</param>
        /// <param name="contentType">ContentType</param>
        /// <param name="data">資料</param>
        /// <returns>檔案網址</returns>
        public async Task<string> PutFile(string objectName, string contentType, byte[] data)
        {
            return await PutFile(objectName, contentType, new MemoryStream(data));
        }

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="objectName">檔案名稱(如為"shared_"開頭則為公開存取檔案)</param>
        /// <param name="contentType">ContentType</param>
        /// <param name="data">資料</param>
        /// <returns>檔案網址</returns>
        public async Task<string> PutFile(string objectName, string contentType, Stream data)
        {
            await MinioClient.PutObjectAsync(
                Options.BucketName,
                objectName,
                data,
                data.Length,
                contentType);


            if (objectName.StartsWith("shared_"))
            {
                var httpSchema = Options.WithSSL ? "https" : "http";
                return $"{httpSchema}://{Options.EndPoint}/{Options.BucketName}/{objectName}";
            }

            return await MinioClient.PresignedGetObjectAsync(Options.BucketName, objectName, 60 * 60 * 24);
        }
         
        /// <summary>
        /// 取得檔案
        /// </summary>
        /// <param name="objectName">檔案名稱</param>
        /// <returns>檔案網址</returns>
        public async Task<string> GetFile(string objectName)
        {
            if (objectName.StartsWith("shared_"))
            {
                var httpSchema = Options.WithSSL ? "https" : "http";
                return $"{httpSchema}://{Options.EndPoint}/{Options.BucketName}/{objectName}";
            }

            return await MinioClient.PresignedGetObjectAsync(Options.BucketName, objectName, 60 * 60 * 24);
        }

        /// <summary>
        /// 刪除檔案
        /// </summary>
        /// <param name="objectName">檔案名稱</param>
        public async Task RemoveFile(string objectName)
        {
            await MinioClient.RemoveObjectAsync(
                Options.BucketName,
                objectName);
        }
    }
}
