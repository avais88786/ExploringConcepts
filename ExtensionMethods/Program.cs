using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploringConcepts.ExtensionMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            String avais = "Avais";
            int x = 5;
            Console.WriteLine(avais.AppendHello());
            Console.WriteLine(avais.AppendHello<string>());
            Console.WriteLine(x.AppendHello<int>());
            Console.WriteLine(x.AppendHello<int,string>("I am appended at the end"));
            Console.ReadLine();
        }
    }
}
