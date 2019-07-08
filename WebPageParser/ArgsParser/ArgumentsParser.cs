using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageParser.cs.ArgsParser
{
    public class ArgumentsParser : IArgumentsParser
    {
        public int NumberOfpagesToSteal { get; set; }

        public void ArgumentReader(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Make sure you launch the program with the number of pages to colect.");
                Console.WriteLine("Example: ./WebPageParser.exe 3");
            }
            else
            {
                NumberOfpagesToSteal = int.Parse(args[0]);
            }
        }

    }
}
