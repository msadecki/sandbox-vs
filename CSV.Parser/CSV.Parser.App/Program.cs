using System;
using System.IO;
using System.Linq;
using CSV.Parser.Logic.Configurations;
using CSV.Parser.Logic.Factories;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.App
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var filePath = args.FirstOrDefault();

            if (string.IsNullOrEmpty(filePath))
            {
                Console.Write("Please pass filePath as first parameter");
            }
            else if (!File.Exists(filePath))
            {
                Console.Write($"Cannot find filePath given as first parameter \"{filePath}\"");
            }
            else
            {
                var csvReader = new CsvReader(
                    new TextReaderFactory(
                        new EncodingConfiguration()),
                    new CsvLineConsumerFactory(),
                    new CsvStreamReaderFactory());

                csvReader.Read(filePath);
            }

            Console.WriteLine();
            Console.WriteLine("---------------------");
            Console.WriteLine("Press any key to exit");
            Console.WriteLine("---------------------");
            Console.Read();
        }
    }
}