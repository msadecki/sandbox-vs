using System;
using System.IO;
using System.Linq;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Configurations;
using CSV.Parser.Logic.Factories;

namespace CSV.Parser.App
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var filePath = args.FirstOrDefault();

            if (string.IsNullOrEmpty(filePath))
            {
                Console.Write("Please pass file path as first parameter");
            }
            else if (!File.Exists(filePath))
            {
                Console.Write($"Cannot find file on path given as first parameter \"{filePath}\"");
            }
            else
            {
                ICsvReaderFactory csvReaderFactory = new CsvReaderFactory(
                    new CsvConfiguration(),
                    new CsvFieldBuilderConfiguration(),
                    new BufferableReaderConfiguration(),
                    new OutputConfiguration(),
                    new EncodingConfiguration());

                var csvReader = csvReaderFactory.Create();

                try
                {
                    csvReader.Read(filePath);
                }
                catch (Exception exception)
                {
                    Console.WriteLine();
                    Console.WriteLine("---------------------");
                    Console.WriteLine("An exception occurred");
                    Console.WriteLine("---------------------");

                    Console.WriteLine(exception);
                }
            }

            Console.WriteLine();
            Console.WriteLine("---------------------");
            Console.WriteLine("Press any key to exit");
            Console.WriteLine("---------------------");
            Console.Read();
        }
    }
}