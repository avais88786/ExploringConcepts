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

namespace OTOMReverseEngineerXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(cfg => {
                cfg.ReplaceMemberName("Line1","AddressLineOne");
                cfg.ReplaceMemberName("Line2","AddressLineTwo");
                cfg.ReplaceMemberName("Line3","AddressLineThree");
                cfg.ReplaceMemberName("Line4","AddressLineFour");
            });


            Mapper.CreateMap<TradesmanAllNBRq, TradesmanDataCapture>()
                .ForMember(d => d.DeclarationQuestions, s => s.MapFrom(ss => ss))
                .ForMember(d => d.BusinessDetails, s => s.MapFrom(ss => ss))
                .ForMember(d => d.EmploymentAndTurnover, s => s.MapFrom(ss => ss))
                .ForMember(d => d.CoversRequired, s => s.MapFrom(ss => ss))
                .ForMember(d => d.Claims, s => s.MapFrom(ss => ss));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanBusinessDetailsTab>()
                .ForMember(d => d.SubsidiaryCompaniesGroup, s => CustomS(s));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanSubsidiaryCompaniesGroup>()
                .ForMember(d => d.SubsidiaryCompanies, s => s.MapFrom(model => model));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanSubsidiaryCompaniesLogicalGroup>()
                .ForMember(d => d.SubsidiaryCompanies, s => s.MapFrom(model => model.Insured.Subsidiary));

            Mapper.CreateMap<TradesmanAllNBRqInsuredSubsidiary, TradesmanSubsidiaryCompany>()
                .ForMember(d => d.CompanyName, s => s.MapFrom(model => model.CompanyName));

            Mapper.CreateMap<TradesmanAllNBRqInsuredSubsidiaryAddress, AddressInformation>();
                
            Mapper.CreateMap<TradesmanAllNBRq, TradesmanDeclarationQuestionsTab>()
                .ForMember(d => d.TradesmanDeclarationGroup, s => s.MapFrom(model => model));

            //Mapper.CreateMap<TradesmanAllNBRq,TradesmanDeclarationGroup>()
            //    .ForMember(d => d.DeclaredBankrupt, opt => opt.MapFrom(model => model.Insured.Declaration.BankruptInd.Value));

            

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
