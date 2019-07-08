namespace WebPageParser.cs.ArgsParser
{
    public interface IArgumentsParser
    {
        int NumberOfpagesToSteal { get; set; }

        void ArgumentReader(string[] args);
    }
}