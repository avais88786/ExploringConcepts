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

namespace OTOMReverseEngineerXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper.CreateMap<TradesmanAllNBRq, TradesmanDataCapture>()
                .ForMember(d => d.DeclarationQuestions, s => s.MapFrom(ss => ss))
                .ForMember(d => d.BusinessDetails, s => s.MapFrom(ss => ss));
                //.ForMember(d => d.DeclarationQuestions, src => src.MapFrom(s=> new TradesmanDeclarationQuestionsTab(){TradesmanDeclarationGroup = new TradesmanDeclarationGroup(){UnSpentConvictions = s.Insured.Declaration.ConvictionsInd.Value}} ));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanBusinessDetailsTab>()
                .ForMember(d => d.SubsidiaryCompaniesGroup, s => s.MapFrom(model => model));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanSubsidiaryCompaniesGroup>()
                .ForMember(d => d.SubsidiaryCompanies, s => s.MapFrom(model => model));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanSubsidiaryCompaniesLogicalGroup>()
                .ForMember(d => d.SubsidiaryCompanies, s => s.MapFrom(model => model));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanSubsidiaryCompany>()
                .ForMember(d => d.CompanyName, s => s.MapFrom(model => model.Insured.CompanyName));
            
            Mapper.CreateMap<TradesmanAllNBRq, TradesmanDeclarationQuestionsTab>()
                .ForMember(d => d.TradesmanDeclarationGroup, s => s.MapFrom(model => model));

            Mapper.CreateMap<TradesmanAllNBRq,TradesmanDeclarationGroup>()
                .ForMember(d => d.DeclaredBankrupt, opt => opt.MapFrom(model => model.Insured.Declaration.BankruptInd.Value));

            

            XmlSerializer xmlSer = new XmlSerializer(typeof(TradesmanAllNBRq));

            FileStream fs = new FileStream(@"XMLs\Tradesman1.xml", FileMode.Open);
            TradesmanAllNBRq deser = (TradesmanAllNBRq)xmlSer.Deserialize(fs);
            fs.Close();

            var x = Mapper.Map<TradesmanAllNBRq, TradesmanDataCapture>(deser);

            var y = new TradesmanDataCapture();
            y.DeclarationQuestions.TradesmanDeclarationGroup.UnSpentConvictions = deser.Insured.Declaration.ConvictionsInd.Value;
            


        }
    }
}
