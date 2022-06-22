using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpressoesRegulares
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EncontrarCpf();
        }

        public static void EncontrarCpf()
        {

            string texto = "Cpfs dos aprovados: 379.199.128-06, 021.370.098-03, 123.456.789-00,321.654.987-99";
            string expressão = @"/\d{3}\.\d{3}\.{3}-\d{2}/g";
            string result = (texto, expressão).ToString();
            MatchCollection match = Regex.Matches(texto, expressão, RegexOptions.IgnoreCase);
            foreach(Match _match in Regex.Matches(texto, expressão, RegexOptions.IgnoreCase))
            {
                Console.WriteLine(_match.Value);
            }
            Console.ReadKey();
        }

        public static void defaul()
        {
            string pattern = "(Mr\\.? |Mrs\\.? |Miss |Ms\\.? )";
            string[] names = { "Mr. Henry Hunt", "Ms. Sara Samuels",
                         "Abraham Adams", "Ms. Nicole Norris" };
            foreach (string name in names)
                Console.WriteLine(Regex.Replace(name, pattern, String.Empty));
            Console.WriteLine("");
            string _pattern = @"\b(\w+?)\s\1\b";
            string input = "This this is a nice day. What about this? This tastes good. I saw a a dog.";
            foreach (Match match in Regex.Matches(input, _pattern, RegexOptions.IgnoreCase))
                Console.WriteLine("{0} (duplicates '{1}') at position {2}",
                                  match.Value, match.Groups[1].Value, match.Index);
            Console.ReadLine();
        }

    }
}
