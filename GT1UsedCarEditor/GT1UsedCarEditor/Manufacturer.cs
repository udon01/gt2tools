﻿using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using StreamExtensions;

namespace GT1.UsedCarEditor
{
    public class Manufacturer
    {
        private static readonly CsvConfiguration csvConfig = new(CultureInfo.CurrentCulture) { ShouldQuote = (args) => true };

        public string Name { get; set; } = "";
        public Car[] Cars { get; set; } = Array.Empty<Car>();

        public static Manufacturer ReadFromFile(Stream file, string name)
        {
            int carCount = file.ReadInt();
            return new Manufacturer
            {
                Name = name,
                Cars = Enumerable.Range(0, carCount).Select(i => Car.ReadFromFile(file)).ToArray()
            };
        }

        public void WriteToCSV(string directory)
        {
            if (Cars.Length == 0)
            {
                return;
            }

            using (TextWriter file = new StreamWriter(File.Create(Path.Combine(directory, $"{Name}.csv")), Encoding.UTF8))
            {
                using (CsvWriter csv = new(file, csvConfig))
                {
                    csv.WriteField("Car");
                    csv.WriteField("Price");
                    csv.WriteField("Colour");
                    csv.NextRecord();

                    foreach (Car car in Cars)
                    {
                        car.WriteToCSV(csv);
                    }
                }
            }
        }
    }
}