using System.Collections.Generic;

namespace WebPageParser.cs.WebPageUtils
{
    public interface IURLGenerator
    {
        List<string> GenerateURlsToVisit();
    }
}