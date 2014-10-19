using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WorkingWithXMLSerializer
{
    class Program
    {
        static void Main(string[] args)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkingWithXMLSerializer.XMLSyntax.Root));
            XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(WorkingWithXMLSerializer.xyz.SSC));

            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringWriter stringWriter = new StringWriter(sb);
            //TextWriter tw = new StringWriter();

            WorkingWithXMLSerializer.XMLSyntax.Root root = new XMLSyntax.Root();
            root.FirstElement.ElementName = "FirstElement";

            FileStream fs = new FileStream(@"C:\Users\Avaisuddin\Desktop\sunxml.xml", FileMode.Open);

            WorkingWithXMLSerializer.xyz.SSC deserailized = (WorkingWithXMLSerializer.xyz.SSC) xmlSerializer2.Deserialize(fs);
            fs.Close();




            WorkingWithXMLSerializer.xyz.SSC sunXML = new WorkingWithXMLSerializer.xyz.SSC();

            
            WorkingWithXMLSerializer.xyz.SSCSunSystemsContext sunsystemscontext = new WorkingWithXMLSerializer.xyz.SSCSunSystemsContext();
            sunsystemscontext.BusinessUnit = "TST";
            sunsystemscontext.BudgetCode = "A";
            object[] objectA = new object[10];
            objectA[0] = sunsystemscontext;
            sunXML.Items = objectA;

            var xx = sunXML.GetType();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            //stringWriter
            //xmlSerializer.Serialize(stringWriter, root, ns);
            xmlSerializer2.Serialize(stringWriter, sunXML, ns);

            stringWriter.Dispose();
            Console.WriteLine(sb.ToString());

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.OmitXmlDeclaration = true;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Auto;
            //xmlWriterSettings.


            using (XmlWriter xmlWriter = XmlWriter.Create(sb2, xmlWriterSettings))
            {
                
                xmlWriter.WriteString(sb.ToString());
            }

            Console.WriteLine();
            Console.WriteLine(sb2.ToString());
            Console.ReadLine();


        }
    }
}
