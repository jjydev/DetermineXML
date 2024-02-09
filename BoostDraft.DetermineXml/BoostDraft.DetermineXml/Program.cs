namespace BoostDraft.XmlUtils
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = args.Any() ? args[0] : "";

#if DEBUG
            if (string.IsNullOrEmpty(input))
                input =  "<Design><Code>hello world</Code></Design>";
#endif

            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Please provide an XML string");
            
            var result = XmlHelper.DetermineXml(input);

            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Output: {result}");

            Console.ReadKey();
        }
    }
}