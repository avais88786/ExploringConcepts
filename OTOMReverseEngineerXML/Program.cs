using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Fleet;

namespace OTOMReverseEngineerXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(MiniFleetNBRq));

            FileStream fs = new FileStream("RSAQuoteErrors.xml", FileMode.Open);
            MiniFleetNBRq deser = (MiniFleetNBRq)xmlSer.Deserialize(fs);
            fs.Close();

            FleetDataCapture fleetDC = new FleetDataCapture();
            fleetDC.BusinessDetails.InsuredDetails.InsuredOrCompanyName = deser.Insured.CompanyName;


        }
    }
}
