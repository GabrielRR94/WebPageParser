using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPageParser.cs.ArgsParser;

namespace WebPageParser.cs.WebPageUtils
{
    public class URLGenerator : IURLGenerator
    {
        private readonly IArgumentsParser _argumentsParser;
        private const string BASE_URL = "http://www.infocif.es/ranking/ventas-empresas/espana?pagina=";


        public URLGenerator(IArgumentsParser argumentsParser)
        {
            _argumentsParser = argumentsParser;
        }

        public List<string> GenerateURlsToVisit()
        {
            List<string> UrlToAttack  = new List<string>();
            for (int i = 1; i <= _argumentsParser.NumberOfpagesToSteal; i++)
            {
                string newUrl = BASE_URL;
                newUrl += i;
                UrlToAttack.Add(newUrl);
            }
            return UrlToAttack;
        }
    }
}
