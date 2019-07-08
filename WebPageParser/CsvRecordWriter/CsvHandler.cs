using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageParser.cs.CsvRecordWriter
{
    public class CsvHandler : ICsvHandler
    {
        public string CsvBasePath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public List<CsvRecord> CsvRecords { get; set; } = new List<CsvRecord>();

        public void CreateRecords(string data)
        {
            var splittedData = data.Split(',');
            List<string> rowElements = new List<string>();

            foreach (var element in splittedData)
            {
                rowElements.Add(element);
            }

            if (rowElements.Count != 10)
            {
                Console.WriteLine($"Error generating row, discarding data: {data.ToString()}");
            }
            else
            {
                rowElements.RemoveAt(1);
                CsvRecord csvRecord = new CsvRecord
                {
                    Rank = rowElements[0],
                    Company = rowElements[1],
                    Sells_2017 = rowElements[2],
                    Sells_2016 = rowElements[3],
                    Revenue_2017 = rowElements[4],
                    EBITDA_2017 = rowElements[5],
                    Employees = rowElements[6],
                    IndustrialActivity = rowElements[7],
                    Location = rowElements[8]
                };
                CsvRecords.Add(csvRecord);
            }
        }

        public void WriteCsvRecords(string csvName)
        {
            var csvFullPath = Path.Combine(CsvBasePath,csvName);
            using (var writer = new StreamWriter(csvFullPath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(CsvRecords);
            }
        }

        public void WriteSingleRecord(string csvName,string record)
        {
            var csvFullPath = Path.Combine(CsvBasePath, csvName);
            using (var writer = new StreamWriter(csvFullPath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteHeader<CsvRecord>();
            }
        }
    }
}
