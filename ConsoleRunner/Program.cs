using System;
using Lambda_MSSQLtoS3;

namespace ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            //Not part of the code that gets deployed to Lamdba.
            //A simple local testing project

            Lambda_MSSQLtoS3.Extractor extractor = new Extractor();
            extractor.ExtractData();


            Console.WriteLine("Hello World!");
        }
    }
}
