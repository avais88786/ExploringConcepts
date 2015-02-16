using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StructureMap;

namespace LinqToXML
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(x => { x.AddRegistry(new DefaultRegistry()); });

            XElement xElement = new XElement("Products",
                                    new XElement("Product","Computer"),
                                    new XElement("Product","Laptop"),
                                    new XElement("Product","Mouse"),
                                    new XElement("Product","Keyboard")
                                );

            Console.WriteLine(xElement.ToString());
            Console.ReadLine();

        }
    }
}
