using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            delegate_Multiply delMultiply = new delegate_Multiply(MyClass.multiply);

            Console.WriteLine(delMultiply(2, 5));

            delMultiply = MyClass.multiply;

            Console.WriteLine(delMultiply(5, 5));
            Console.WriteLine(delMultiply(7, 5));
            Console.ReadLine();

        }

        
    }

    public class MyClass
    {
        public static double multiply(int p1, int p2)
        {
            return p1 * p2;
        }
        
    }

    public delegate double delegate_Multiply(int a,int b);
}
