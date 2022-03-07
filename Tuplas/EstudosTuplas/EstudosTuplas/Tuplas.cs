var tupla = new Tuple<string, string>("Testando", "Tupla 1");
var tupla2 = Tuple.Create<string, string>("Testando", "Tupla 2");
(string, int) tupla3 = ("Testando", 3);
var tupla4 = ("Testando", 4);
(string Sring, int Numero) tupla5 = ("Testando", 5);

Console.WriteLine("");
Console.WriteLine("            Tuplas           ");
Console.WriteLine("-----------------------------");
Console.WriteLine($"| TUPLA 1 {tupla.Item1} - {tupla.Item2}|");
Console.WriteLine($"| TUPLA 2 {tupla2.Item1} - {tupla2.Item2}|");
Console.WriteLine($"| TUPLA 3 {tupla3.Item1} - {tupla3.Item2}      |");
Console.WriteLine($"| TUPLA 4 {tupla4.Item1} - {tupla4.Item2}      |");
Console.WriteLine($"| TUPLA 5 {tupla5.Sring} - {tupla5.Numero}      |");
Console.WriteLine("-----------------------------");
Console.ReadLine();
