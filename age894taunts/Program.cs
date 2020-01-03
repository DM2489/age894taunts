using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;

namespace age894taunts
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Processing...");

            List<Aoe2Taunt> age894Taunts = new List<Aoe2Taunt>();

            // Get rooms from the csv file.
            using (StreamReader streamReader = new StreamReader("../../../Source/aoe2.csv"))
            {
                using (CsvReader csvReader = new CsvReader(streamReader))
                {
                    while (csvReader.Read())
                    {
                        string row = csvReader.GetField(0);

                        string number = row[0..3];

                        string note = "";
                        int dashIndex = row.IndexOf(" - ");

                        // If we have a note defined.
                        if (dashIndex > 0)
                        {
                            note = row[4..(dashIndex)];
                        }

                        int textChar = dashIndex > 0 ? dashIndex + 2 : 4;

                        string text = row[textChar..];

                        Console.WriteLine($"Number: {number} Note: {note} text: {text}");

                        age894Taunts.Add(new Aoe2Taunt
                        {
                            Number = number,
                            Note = note,
                            Text = text
                        });
                    }
                }
            }
            Console.WriteLine($"{Environment.NewLine}Saving...");

            using (StreamWriter streamWriter = new StreamWriter("../../../Output/aoe2.csv"))
            {
                using (var csvWriter = new CsvWriter(streamWriter))
                {
                    csvWriter.WriteRecords(age894Taunts);
                }
            }
            
            Console.WriteLine($"{Environment.NewLine}Processing Complete.");
            Console.ReadLine();
        }
    }

    class Aoe2Taunt
    {
        public string Number { get; set; }
        public string Text { get; set; }
        public string Note { get; set; }
    }
}
