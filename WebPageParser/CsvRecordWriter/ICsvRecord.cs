namespace WebPageParser.cs.CsvRecordWriter
{
    public interface ICsvRecord
    {
        string Company { get; set; }
        string EBITDA_2017 { get; set; }
        string Employees { get; set; }
        string IndustrialActivity { get; set; }
        string Location { get; set; }
        string Rank { get; set; }
        string Revenue_2017 { get; set; }
        string Sells_2016 { get; set; }
        string Sells_2017 { get; set; }
    }
}