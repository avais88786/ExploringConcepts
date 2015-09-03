using AutoMapper;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OTOMReverseEngineerXML.AutoMapperProfiles
{
    public static class BaseConfigurations
    {
        public static void AutoMapperOneTimeConfigurations()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ReplaceMemberName("Line1", "AddressLineOne");
                cfg.ReplaceMemberName("Line2", "AddressLineTwo");
                cfg.ReplaceMemberName("Line3", "AddressLineThree");
                cfg.ReplaceMemberName("Line4", "AddressLineFour");
            });

        }

        public static void ConfigureAndInitializeForBusinessLine(Type source,Type destination)
        {
            CreateNestedMappers(source, destination);
            ParseSourceType(source, destination);
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

                if (sourceProperty.Name.Contains("Code") || sourceProperty.Name.Contains("ClaimReportingStatus"))
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

                    var mi = typeof(BaseConfigurations).GetMethod("GenericsMap").MakeGenericMethod(sourceType, destinationType);
                    mi.Invoke(null, new object[] { destinationProperty.Name,null });

                        if (destinationProperty.PropertyType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Count() == 1 && destinationProperty.PropertyType.GetProperties().First().PropertyType.IsClass){
                        {
                                CreateMappers(sourceType, destinationPropertyType);
                        }
                    }

                    CreateNestedMappers(sourceType, destinationProperty.PropertyType);
                }
                //Tried to Map a list from destination to any array type from source, DIDNOT WORK :'( 
                //else if (destinationProperty.PropertyType.IsGenericType && destinationProperty.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                //{
                //    var anyListTypeInSource = sourceProperties.FirstOrDefault(s => s.PropertyType.IsArray);
                //    if (anyListTypeInSource != null)
                //    {
                //        var mi = typeof(BaseConfigurations).GetMethod("GenericsMap").MakeGenericMethod(sourceType, destinationType);
                //        mi.Invoke(null, new object[] { destinationProperty.Name,anyListTypeInSource.Name });
                //    }
                //}

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

        public static void GenericsMap<T1, T2>(string destPropertyName,string sourcePropertyName = null)
        {
            //http://www.crowbarsolutions.com/dynamically-generating-lambda-expressions-at-runtime-from-properties-obtained-through-reflection-on-generic-types/
            //http://stackoverflow.com/questions/8315819/expression-lambda-and-query-generation-at-runtime-simplest-where-example
            //http://www.andymcm.com/blog/2009/09/building-lambda-expressions-at-runtime.html
            //http://www.codeproject.com/Articles/17575/Lambda-Expressions-and-Expression-Trees-An-Introdu

            //Mapper.CreateMap<TradesmanAllNBRq, TradesmanDataCapture>();
            //.ForMember(d => d.DeclarationQuestions, s => s.MapFrom(ss => ss))


            //Mapper.CreateMap<T1, T2>();

            var destparameterExpression = Expression.Parameter(typeof(T2), "dest");
            var destmemberExpression = Expression.PropertyOrField(destparameterExpression, destPropertyName);
            var destmemberExpressionConversion = Expression.Convert(destmemberExpression, typeof(object));
            var destlambda = Expression.Lambda<Func<T2, object>>(destmemberExpressionConversion, destparameterExpression);

            MemberExpression srcmemberExpression;
            Expression srcmemberExpressionConversion;
            var srcparameterExpression = Expression.Parameter(typeof(T1), "src");
            if (!string.IsNullOrEmpty(sourcePropertyName)) { 
             srcmemberExpression = Expression.Property(srcparameterExpression,sourcePropertyName);
             srcmemberExpressionConversion = Expression.Convert(srcmemberExpression, typeof(object));
            }
            else
            {
                srcmemberExpressionConversion = Expression.Convert(srcparameterExpression, typeof(object));
            }
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

