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
            //var container = new Container(x => { x.AddRegistry(new DefaultRegistry()); });

            XNamespace aw = "http://www.adventure-works.com";
            XElement xmlTree1 = new XElement("Root",
                new XElement("Child1", 1,new XAttribute("Index",0)),
                new XElement("Child2", 2),
                new XElement("Child3", 3),
                new XElement("Child4", 4),
                new XElement("Child5", 5),
                new XElement("Child6", 6)
            );

            Console.WriteLine(xmlTree1.ToString());

            XElement xmlTree2 = new XElement("Root",
                from el in xmlTree1.Elements()
                where (Convert.ToInt32(el.Value) >= 3 && (int)el <= 5)
                select el
            );

            foreach (var xELe in xmlTree1.Elements())
            {
                var y = xELe;
            }


            XElement xElement = new XElement("Products",
                                    new XElement("Product","Computer"),
                                    new XElement("Product","Laptop"),
                                    new XElement("Product","Mouse"),
                                    new XElement("Product","Keyboard")
                                );

            Console.WriteLine(xmlTree2.ToString());
            Console.ReadLine();

        }
    }
}
