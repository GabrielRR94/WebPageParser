using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using WebPageParser.cs.ArgsParser;
using WebPageParser.cs.CsvRecordWriter;
using WebPageParser.cs.WebPageUtils;

namespace WebPageParser.cs
{
    public class Launcher
    {
        private readonly IArgumentsParser _argumentsParser;
        private readonly IURLGenerator _URLGenerator;
        private readonly ICsvHandler _csvHandler;
        private readonly string CsvName = $"{DateTime.UtcNow.ToString("yyyyMMdd")}.csv";

        private Regex _regex = new Regex("(.)(?<=\\1\\1\\1)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        public Launcher(IArgumentsParser argumentsParser, IURLGenerator URLGenerator,ICsvHandler csvHandler)
        {
            _argumentsParser = argumentsParser;
            _URLGenerator = URLGenerator;
            _csvHandler = csvHandler;
        }

        public void Execute(string[] args)
        {
            _argumentsParser.ArgumentReader(args);
            var urlsToVisit = _URLGenerator.GenerateURlsToVisit();

            HtmlWeb htmlWeb = new HtmlWeb();
            foreach(var urlToVisit in urlsToVisit)
            {
                HtmlDocument document = htmlWeb.Load(urlToVisit);
                HtmlNode tableHeaderNode = document.DocumentNode.SelectSingleNode("//*[@id='tablaranking']/thead/tr");
                var tableHeader = CleanRecord(tableHeaderNode.InnerText);
                //tableHeader ready to write on the csv
                _csvHandler.WriteSingleRecord(CsvName, tableHeader);

                HtmlNode tableNode = document.DocumentNode.SelectSingleNode("//*[@id='tablaranking']/tbody");
                var tableNodes = tableNode.ChildNodes;

                foreach (var _tableNode in tableNodes)
                {
                    var tableRowData = CleanRecord(_tableNode.InnerText);
                    //tableRowData ready to store the record of the data
                    _csvHandler.CreateRecords(tableRowData);
                }
            }
            _csvHandler.WriteCsvRecords(CsvName);
        }

        private string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }

        private string CleanRecord(string rawRecord)
        {
            rawRecord = rawRecord.Replace("\t", string.Empty);
            rawRecord = rawRecord.Replace("\r\n", ",");
            rawRecord = RemoveWhitespace(rawRecord);
            rawRecord = _regex.Replace(rawRecord, string.Empty);
            rawRecord = rawRecord.Replace(",,", ",");
            string record = string.Empty;
            try
            {
                rawRecord = rawRecord.Remove(0, 1);
                record = rawRecord.Remove(rawRecord.Length - 1, 1);
            }
            catch (Exception)
            {
                //do nothing in case of error, suicide coming up!!!!
            }
            return record;
        }
    }
}
