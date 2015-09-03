using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman;
using AutoMapper;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.DeclarationQuestions;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.BusinessDetailsQuestions;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels;
using System.Reflection;
using System.Linq.Expressions;
using OTOMReverseEngineerXML.AutoMapperProfiles;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Fleet;
using OTOMReverseEngineerXML.Helpers;

namespace OTOMReverseEngineerXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(mapper =>
                {
                    BaseConfigurations.AutoMapperOneTimeConfigurations();
                    mapper.AddProfile<TradesmanNBRqProfile>();
                    mapper.AddProfile<MiniFleetNBRqProfile>();
                });

            XmlSerializer xmlSer;
            FileStream fs;
            object x;
            StringBuilder sbuilder;
             StringWriter sWriter ;
            string y ;
            object deser; 

            #region Tradesman Just UnComment Me

            xmlSer = new XmlSerializer(typeof(TradesmanAllNBRq));

            fs = new FileStream(@"XMLs\Tradesman1.xml", FileMode.Open);
            deser = (TradesmanAllNBRq)xmlSer.Deserialize(fs);
            fs.Close();

            x = Mapper.Map<TradesmanAllNBRq, TradesmanDataCapture>((TradesmanAllNBRq)deser, opt =>
            {
                opt.AfterMap((src, dest) => dest.ProcessGroupVisibleBool());
            });

            xmlSer = new XmlSerializer(typeof(TradesmanDataCapture));

            sbuilder = new StringBuilder();
            sWriter = new StringWriter(sbuilder);
            xmlSer.Serialize(sWriter, x);
            y = sbuilder.ToString();

            #endregion

            #region Fleet 

            xmlSer = new XmlSerializer(typeof(MiniFleetNBRq));

            fs = new FileStream(@"XMLs\RSAQuoteErrors.xml", FileMode.Open);
            deser = (MiniFleetNBRq)xmlSer.Deserialize(fs);
            fs.Close();

            x = Mapper.Map<MiniFleetNBRq, FleetDataCapture>((MiniFleetNBRq)deser);

            xmlSer = new XmlSerializer(typeof(FleetDataCapture));

            sbuilder = new StringBuilder();
            sWriter = new StringWriter(sbuilder);
            xmlSer.Serialize(sWriter, x);
            y = sbuilder.ToString();


            #endregion


        }

    }

}
