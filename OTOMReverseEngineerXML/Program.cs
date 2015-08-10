using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OpenGI.MVC

namespace OTOMReverseEngineerXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(MiniFleetNBRq));

            FileStream fs = new FileStream("RSAQuoteErrors.xml", FileMode.Open);
            var deser = xmlSer.Deserialize(fs);
            fs.Close();



        }
    }
}
