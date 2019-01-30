using System;
using System.Collections.Generic;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lambda_MSSQLtoS3.Helpers
{
    static class S3Helper
    {
        private static AmazonS3Client s3Client;

        static S3Helper()
        {
            s3Client = new AmazonS3Client(
                            Environment.GetEnvironmentVariable("AwsAccessKey"),
                            Environment.GetEnvironmentVariable("AwsSecretKey"),
                            S3Region.APS2);
        }

        //Will take any CSV and save it to S3
        public static void SaveToS3AsCsv(StringBuilder contents)
        {
            //Save CSV to S3
            PutObjectRequest request = new PutObjectRequest
            {
                ContentBody = contents.ToString(),
                ContentType = "text/csv",
                BucketName = Environment.GetEnvironmentVariable("S3Bucket"),
                Key = Environment.GetEnvironmentVariable("OutputFilePathCSV")
            };

            try
            {
                var result = s3Client.PutObjectAsync(request);
                Console.WriteLine(result.Result.HttpStatusCode);
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"ERROR SAVING TO S3 AT {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}\n{ex.InnerException.Message.ToString()}");
            }
        }

        //Can pass any object, get it serialised to JSON and saved to S3
        public static void SaveToS3AsJSON(object POCOtoBeJSONed)
        {
            //Save CSV to S3
            PutObjectRequest request = new PutObjectRequest
            {
                ContentBody = JsonConvert.SerializeObject(POCOtoBeJSONed),
                ContentType = "pplication/json",
                BucketName = Environment.GetEnvironmentVariable("S3Bucket"),
                Key = Environment.GetEnvironmentVariable("OutputFilePathJSON")
            };

            try
            {
                var result = s3Client.PutObjectAsync(request);
                Console.WriteLine(result.Result.HttpStatusCode);
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"ERROR SAVING TO S3 AT {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}\n{ex.InnerException.Message.ToString()}");
            }
        }
    }
}