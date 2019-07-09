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

        private Regex _regex = new Regex(@"(.)(?<=\1\1\1)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
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
                var tableHeader = BaseClean(tableHeaderNode.InnerText);
                //tableHeader ready to write on the csv
                _csvHandler.WriteSingleRecord(CsvName, tableHeader);

                HtmlNode tableNode = document.DocumentNode.SelectSingleNode("//*[@id='tablaranking']/tbody");
                var tableNodes = tableNode.ChildNodes;

                var recordsTowrite = tableNodes.Select(n => CleanRow(n.InnerText)).Where(r => !string.IsNullOrWhiteSpace(r));

                foreach(var row in recordsTowrite)
                {
                    _csvHandler.CreateRecords(row);
                }
            }
            _csvHandler.WriteCsvRecords(CsvName);
        }
        
        private string BaseClean(string rawRecord)
        {
            var headers = rawRecord.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Where(x => !string.IsNullOrWhiteSpace(x));
            var toClean = string.Join(",", headers);
            var rgx = new Regex(@"\s+");
           return rgx.Replace(toClean, string.Empty);
        }

        private string CleanRow(string rawRecord)
        {
            var cleaned = BaseClean(rawRecord);
            var rgx = new Regex(@"(\d+),\(\1\)");
            var match = rgx.Match(cleaned);
            return match.Success ? $"{match.Groups[1]}{cleaned.Substring(match.Index + match.Length)}" : cleaned;
        }
    }
}
