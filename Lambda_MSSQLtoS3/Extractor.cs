using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Amazon.Lambda.Core;
using Lambda_MSSQLtoS3.Models;
using Dapper;
using System.Text;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Lambda_MSSQLtoS3
{
    public class Extractor
    {
        public string ExtractData()
        {
            string query = $"select * from customers";
            List<Customer> customers = new List<Customer>();

            //Extract from MSSQL using Dapper
            using (var db = GetMSSQLConnection())
                customers = db.Query<Customer>(query).ToList();

            //Convert results to CSV, Save to S3
            var csvData = ConvertResultsToCSV(customers);
            Helpers.S3Helper.SaveToS3AsCsv(csvData);

            //
            //OR
            //save it as JSON to S3
            Helpers.S3Helper.SaveToS3AsJSON(customers);

            return "Jobs done!";
        }

        private StringBuilder ConvertResultsToCSV(List<Customer> customers)
        {
            StringBuilder sb = new StringBuilder();

            //Set the headers
            sb.AppendLine("CustomerId,FirstName,LastName,Age,DateOfBirth");

            foreach (var customer in customers)
            {
                sb.AppendLine($"{customer.CustomerId},{customer.FirstName},{customer.LastName},{customer.Age},{customer.DateOfBirth.ToString("yyyy-MM-dd")}");
            }

            return sb;
        }

        public static SqlConnection GetMSSQLConnection()
        {
            return new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
        }
    }
}