using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageParser.cs.CsvRecordWriter
{
    public class CsvRecord : ICsvRecord
    {
        public string Rank { get; set; }
        public string Company { get; set; }
        public string Sells_2017 { get; set; }
        public string Sells_2016 { get; set; }
        public string Revenue_2017 { get; set; }
        public string EBITDA_2017 { get; set; }
        public string Employees { get; set; }
        public string IndustrialActivity { get; set; }
        public string Location { get; set; }

    }
}
