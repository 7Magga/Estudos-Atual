using System;

namespace TesteDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite um CEP");
            var CEP = Console.ReadLine();

            CriaçãoDLL.Class1 API = new CriaçãoDLL.Class1();
            var retornoString = API.BuscaCep(CEP, "string");
            var retornoJson = API.BuscaCep(CEP, "json");
            Console.WriteLine(retornoString);
        }
    }
}
