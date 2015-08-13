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

namespace OTOMReverseEngineerXML
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureAndInitialize();

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanDataCapture>();
                //.ForMember(d => d.DeclarationQuestions, s => s.MapFrom(ss => ss))
                //.ForMember(d => d.BusinessDetails, s => s.MapFrom(ss => ss))
                //.ForMember(d => d.EmploymentAndTurnover, s => s.MapFrom(ss => ss))
                //.ForMember(d => d.CoversRequired, s => s.MapFrom(ss => ss))
                //.ForMember(d => d.Claims, s => s.MapFrom(ss => ss));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanBusinessDetailsTab>()
                .ForMember(d => d.SubsidiaryCompaniesGroup, s => s.MapFrom(model => model));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanSubsidiaryCompaniesGroup>()
                .ForMember(d => d.SubsidiaryCompanies, s => s.MapFrom(model => model));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanSubsidiaryCompaniesLogicalGroup>()
                .ForMember(d => d.SubsidiaryCompanies, s => s.MapFrom(model => model.Insured.Subsidiary));

            Mapper.CreateMap<TradesmanAllNBRqInsuredSubsidiary, TradesmanSubsidiaryCompany>()
                .ForMember(d => d.CompanyName, s => s.MapFrom(model => model.CompanyName));

            Mapper.CreateMap<TradesmanAllNBRqInsuredSubsidiaryAddress, AddressInformation>();

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanDeclarationQuestionsTab>()
                .ForMember(d => d.TradesmanDeclarationGroup, s => s.MapFrom(model => model));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanDeclarationGroup>()
                .ForMember(d => d.DeclaredBankrupt, opt => opt.MapFrom(model => model.Insured.Declaration.BankruptInd.Value));

            

            XmlSerializer xmlSer = new XmlSerializer(typeof(TradesmanAllNBRq));

            FileStream fs = new FileStream(@"XMLs\Tradesman1.xml", FileMode.Open);
            TradesmanAllNBRq deser = (TradesmanAllNBRq)xmlSer.Deserialize(fs);
            fs.Close();

            var x = Mapper.Map<TradesmanAllNBRq, TradesmanDataCapture>(deser);

            var y = new TradesmanDataCapture();
            y.DeclarationQuestions.TradesmanDeclarationGroup.UnSpentConvictions = deser.Insured.Declaration.ConvictionsInd.Value;
            


        }

        private static void ConfigureAndInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ReplaceMemberName("Line1", "AddressLineOne");
                cfg.ReplaceMemberName("Line2", "AddressLineTwo");
                cfg.ReplaceMemberName("Line3", "AddressLineThree");
                cfg.ReplaceMemberName("Line4", "AddressLineFour");
            });

            CreateNestedMappers(typeof(TradesmanAllNBRq), typeof(TradesmanDataCapture));
        }

        public static void CreateNestedMappers(Type sourceType, Type destinationType)
        {
            PropertyInfo[] sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] destinationProperties = destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var destinationProperty in destinationProperties)
            {
                Type destinationPropertyType = destinationProperty.PropertyType;
                if (Filter(destinationPropertyType))
                    continue;

                if (destinationProperty.PropertyType.IsClass) {

                    if (destinationProperty.PropertyType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Count() == 1 && destinationProperty.PropertyType.GetProperties().First().PropertyType.IsClass){
                    //if (destinationProperty.PropertyType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Any(p => p.PropertyType.IsClass)) {
                    //    foreach (var destProp in destinationProperty.PropertyType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType.IsClass))
                        {
                            CreateMappers(sourceType, destinationPropertyType);
                        }

                        
                    }

                    CreateNestedMappers(sourceType, destinationProperty.PropertyType);
                }


                PropertyInfo sourceProperty = sourceProperties.FirstOrDefault(prop => NameMatches(prop.Name, destinationProperty.Name));
                Type sourcePropertyType;
                if (sourceProperty == null) {
                    Mapper.CreateMap(sourceType, destinationType).ForMember(destinationProperty.Name, s => s.MapFrom("AgencyAccountRef"));
                    CreateMappers(sourceType, destinationPropertyType);
                    continue;
                }

                sourcePropertyType = sourceProperty.PropertyType;
                if (destinationPropertyType.IsGenericType)
                {
                    Type destinationGenericType = destinationPropertyType.GetGenericArguments()[0];
                    if (Filter(destinationGenericType))
                        continue;
                    Type sourceGenericType;
                    
                    if (sourcePropertyType.IsArray)
                        sourceGenericType = sourcePropertyType.GetElementType();
                    else
                        sourceGenericType = sourcePropertyType.GetGenericArguments()[0];
                    CreateMappers(sourceGenericType, destinationGenericType);
                }
                else
                {
                    CreateMappers(sourcePropertyType, destinationPropertyType);
                }

                Mapper.CreateMap(sourceType, destinationType).ForMember(destinationProperty.Name, s => s.MapFrom("AgencyAccountRef"));
            }

            Mapper.CreateMap(sourceType, destinationType);
            
        }

        private static void CreateMappers(Type sourcePropertyType, Type destinationPropertyType)
        {
            Mapper.CreateMap(sourcePropertyType, destinationPropertyType);
        }


        static bool Filter(Type type)
        {
            return type.IsPrimitive || NoPrimitiveTypes.Contains(type.Name);
        }

        static readonly HashSet<string> NoPrimitiveTypes = new HashSet<string>() { "String", "DateTime", "Decimal" };

        private static bool NameMatches(string memberName, string nameToMatch)
        {
            return String.Compare(memberName, nameToMatch, StringComparison.OrdinalIgnoreCase) == 0;
        }


    }
}
