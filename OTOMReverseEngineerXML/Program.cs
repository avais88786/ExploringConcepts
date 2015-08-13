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

namespace OTOMReverseEngineerXML
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureAndInitialize();

            Mapper.CreateMap<TradesmanAllNBRq, BusinessDetailsGroup>()
                .ForMember(d => d.CompanyStatus, s => s.MapFrom(model => model.Insured.CompanyStatusCode))
                .ForMember(d => d.NumberOfYearsExperience, s => s.MapFrom(model => model.Insured.YearsExperience))
                .ForMember(d => d.AddressInformation, s => s.MapFrom(model => model.Insured.Address));

            Mapper.CreateMap<TradesmanAllNBRq, IndividualLogicalGroup>()
                .ForMember(d => d.Individuals, s => s.MapFrom(model => model.Insured.IndividualName));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanSubsidiaryCompaniesLogicalGroup>()
                .ForMember(d => d.SubsidiaryCompanies, s => s.MapFrom(model => model.Insured.Subsidiary));

            Mapper.CreateMap<TradesmanAllNBRqInsuredSubsidiary, TradesmanSubsidiaryCompany>()
                .ForMember(d => d.CompanyName, s => s.MapFrom(model => model.CompanyName));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanDeclarationGroup>()
                .ForMember(d => d.DeclaredBankrupt, opt => opt.MapFrom(model => model.Insured.Declaration.BankruptInd.Value))
                .ForMember(d => d.DischargeOfFumes, opt => opt.MapFrom(model => model.Insured.Declaration.DischargeOfWasteInd.Value))
                .ForMember(d => d.HarmfulSubstances, opt => opt.MapFrom(model => model.Insured.Declaration.HarmfulSubstancesInd.Value))
                .ForMember(d => d.ProposalRefused, opt => opt.MapFrom(model => model.Insured.Declaration.InsuranceInd.Value))
                .ForMember(d => d.SeparateDedicatedBusinessPremises, opt => opt.MapFrom(model => model.Insured.Declaration.BusinessPremisesInd.Value))
                .ForMember(d => d.UnSpentConvictions, opt => opt.MapFrom(model => model.Insured.Declaration.ConvictionsInd.Value));

            

            XmlSerializer xmlSer = new XmlSerializer(typeof(TradesmanAllNBRq));

            FileStream fs = new FileStream(@"XMLs\Tradesman1.xml", FileMode.Open);
            TradesmanAllNBRq deser = (TradesmanAllNBRq)xmlSer.Deserialize(fs);
            fs.Close();

            var x = Mapper.Map<TradesmanAllNBRq, TradesmanDataCapture>(deser);

            xmlSer = new XmlSerializer(typeof(TradesmanDataCapture));
            
            StringBuilder sbuilder = new StringBuilder();
            StringWriter sWriter = new StringWriter(sbuilder);
            xmlSer.Serialize(sWriter, x);
            var y = sbuilder.ToString();

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
            ParseSourceType(typeof(TradesmanAllNBRq), typeof(TradesmanDataCapture));
        }

        private static void ParseSourceType(Type sourceType, Type destinationType)
        {
            PropertyInfo[] sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var sourceProperty in sourceProperties)
            {
                Type sourcePropertyType = sourceProperty.PropertyType;
                if (Filter(sourcePropertyType))
                    continue;

                if (sourceProperty.Name.Contains("Address"))
                {
                    Mapper.CreateMap(sourcePropertyType, typeof(AddressInformation));
                    continue;
                }

                if (sourceProperty.Name.Contains("Code"))
                {
                    Mapper.CreateMap(sourcePropertyType, typeof(CodeList)).ConvertUsing<ToCodeListConverter>();
                }

                if (sourcePropertyType.IsArray)
                {
                    ParseSourceType(sourcePropertyType.GetElementType(), destinationType);
                }

                if (sourcePropertyType.IsClass)
                {
                    ParseSourceType(sourcePropertyType, destinationType);
                }
            }
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

                if (destinationProperty.PropertyType.IsClass && !(destinationProperty.PropertyType.IsGenericType && destinationProperty.PropertyType.GetGenericTypeDefinition() == typeof(List<>))) {

                    var mi = typeof(Program).GetMethod("GenericsMap").MakeGenericMethod(sourceType, destinationType);
                    mi.Invoke(null, new object[] { destinationProperty.Name });

                        if (destinationProperty.PropertyType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Count() == 1 && destinationProperty.PropertyType.GetProperties().First().PropertyType.IsClass){
                        {
                                CreateMappers(sourceType, destinationPropertyType);
                        }
                    }

                    CreateNestedMappers(sourceType, destinationProperty.PropertyType);
                }


                PropertyInfo sourceProperty = sourceProperties.FirstOrDefault(prop => NameMatches(prop.Name, destinationProperty.Name));
                Type sourcePropertyType;
                if (sourceProperty == null) {
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

                

                Mapper.CreateMap(sourceType, destinationType);
            }

            Mapper.CreateMap(sourceType, destinationType);

        }

        public static void GenericsMap<T1, T2>(string destPropertyName)
        {
            //http://www.crowbarsolutions.com/dynamically-generating-lambda-expressions-at-runtime-from-properties-obtained-through-reflection-on-generic-types/

            //Mapper.CreateMap<TradesmanAllNBRq, TradesmanDataCapture>();
            //.ForMember(d => d.DeclarationQuestions, s => s.MapFrom(ss => ss))


            //Mapper.CreateMap<T1, T2>();

            var destparameterExpression = Expression.Parameter(typeof(T2), "dest");
            var destmemberExpression = Expression.PropertyOrField(destparameterExpression, destPropertyName);
            var destmemberExpressionConversion = Expression.Convert(destmemberExpression, typeof(object));
            var destlambda = Expression.Lambda<Func<T2, object>>(destmemberExpressionConversion, destparameterExpression);


            var srcparameterExpression = Expression.Parameter(typeof(T1), "src");
            //var srcmemberExpression = Expression.Property()
            var srcmemberExpressionConversion = Expression.Convert(srcparameterExpression, typeof(object));
            var srclambda = Expression.Lambda<Func<T1, object>>(srcmemberExpressionConversion, srcparameterExpression);

            Mapper.CreateMap<T1, T2>()
             .ForMember(destlambda, s => s.MapFrom(srclambda));

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

    public class ToCodeListConverter : ITypeConverter<object, CodeList>
    {

        public CodeList Convert(ResolutionContext context)
        {
            var valueProperty = context.SourceType.GetProperty("Value");
            var shortDescrProperty = context.SourceType.GetProperty("ShortDescription");
            return new CodeList() { SelectedDescription = (string)shortDescrProperty.GetValue(context.SourceValue), SelectedValue = (string)valueProperty.GetValue(context.SourceValue) };
        }
    }


}
