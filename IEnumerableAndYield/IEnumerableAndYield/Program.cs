using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploringConcepts.IEnumerableAndYield
{
    class Program
    {
        static void Main(string[] args)
        {

            int x = 10;
            int? y = 20;
            int? z = null;
            Console.WriteLine(x + z ?? y);
            int a = 1000;
            a <<= 4;
            Console.WriteLine(a);

            int[] array = new int[10];
            array.ToList().ForEach(gg => Console.WriteLine(gg));
            //MyClass testClass = new MyClass();
            //testClass.add(1);
            //testClass.add(7);
            //testClass.add(5);

            //MyGenericClass<string> genericTypeString = new MyGenericClass<string>();
            //genericTypeString.add("Rais");
            //genericTypeString.add("Avais");
            //genericTypeString.add("Saif");

            //MyGenericClass<int> genericTypeInt = new MyGenericClass<int>();
            //List<int> x = new List<int>();
            //genericTypeInt.add(12);
            //genericTypeInt.add(9);
            //genericTypeInt.add(6);


            //x.Add(4);
            //x.Add(9);
            //x.Add(6);


            //var y = x.OrderBy(t => t);
            //var z = genericTypeString.OrderBy(t => t);
            //var z2 = genericTypeInt.OrderBy(t => t);
            ////Console.WriteLine(testClass.m_items.Average(t=>Convert.ToDecimal(t.ToString())));

            //foreach (object s in testClass)
            //{
            //    Console.WriteLine(s);
            //}

            //foreach (var item in genericTypeString)
            //    Console.WriteLine(item);
            
            
            //foreach (var item in genericTypeInt)
            //    Console.WriteLine(item);

            Console.ReadLine();
        }
    }
}
