using System.Collections.Generic;

namespace WebPageParser.cs.CsvRecordWriter
{
    public interface ICsvHandler
    {
        string CsvBasePath { get; set; }
        List<CsvRecord> CsvRecords { get; set; }

        void CreateRecords(string data);
        void WriteCsvRecords(string csvPath);
        void WriteSingleRecord(string csvName, string record);
    }
}