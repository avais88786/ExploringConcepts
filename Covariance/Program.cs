using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covariance
{
    class Program
    {
        static void Main(string[] args)
        {
            ISample<Person> p2 = new ASample<Athelete>();
            var x = p2.Display();
            Console.WriteLine(x.GetType().Name);
            Console.ReadLine();

        }
    }

    interface ISample<out T> //out makes the parameter covariant!
    {
        T Display();
    }

    class ASample<T> : ISample<T> where T : new()
    {

        public T Display()
        {
            return new T();
        }
    }

    class Person
    {
        public string Name { get; set; }
    }

    class Athelete : Person
    {
        public string SportsType { get; set; }
    }
}
