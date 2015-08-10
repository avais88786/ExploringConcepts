using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLSettingsUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            //xmlSettings.Indent = true;

            XmlWriter writer = XmlWriter.Create(@"..\..\App_data\data.xml", xmlSettings);
            writer.WriteStartElement("book");
            writer.WriteElementString("item", "testing");
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }
    }
}
