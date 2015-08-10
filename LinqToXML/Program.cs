using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenGI.OpenTrader.Common.Risk;
using System.Globalization;
using System.Xml;

namespace LinqToXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument preQuoteData = XDocument.Load(@"..\..\PreQuoteXml.xml");


            foreach (XElement XE in preQuoteData.Root.DescendantsAndSelf())
            {
                // Stripping the namespace by setting the name of the element to it's localname only
                XE.Name = XE.Name.LocalName;
                // replacing all attributes with attributes that are not namespaces and their names are set to only the localname
                XE.ReplaceAttributes((from xattrib in XE.Attributes().Where(xa => !xa.IsNamespaceDeclaration) select new XAttribute(xattrib.Name.LocalName, xattrib.Value)));
            }

            var x222 = XmlConvert.ToDateTime("2015-02-19T08:52:37.5147347+00:00",XmlDateTimeSerializationMode.Local).ToString("yyyy-MM-dd");

            var stringAmount = "100.556";

            double decimalValue;

            decimalValue =  Double.Parse(stringAmount);
            //decimalValue  = Math.Round(decimalValue, 2);
            //string convertedAmount = decimalValue.ToString();

          //  Console.WriteLine(convertedAmount.ToString(new DoubleFormatter()));

            Console.WriteLine(string.Format("{0:0.00}", stringAmount));

            //double width = 15;
            //double height = 12.8497979;
            //Console.WriteLine(
            //  string.Format(new DoubleFormatter(), "w={0}", decimalValue));

            //claimDetails.Element("OccuranceDate").Value
            //var x222 = XmlConvert.ToDateTime(claimDetails.Element("OccuranceDate").Value, XmlDateTimeSerializationMode.Local).ToString("yyyy-MM-dd");

            //#region go

            //RiskMetadata x;
            //XElement xElement = new XElement("BusinessSource",
            //                        new XElement("Code",
            //                            new XElement("Value","B183 002"),
            //                            new XElement("ShortDescription","Broker Office")
            //                                     )
            //                    );

            //XElement standardQuestions = preQuoteData.Root.Element("StandardQuestions");
            //XElement declarationQuestions = preQuoteData.Root.Element("DeclarationQuestions");
            //XElement claimQuestions = preQuoteData.Root.Element("ClaimsQuestions");

            //bool employeesPaidBelowPAYEThreshold = Convert.ToBoolean(standardQuestions.Element("AllEmployeesPaidBelowPAYEThreshold").Value);

            //XElement empReferenceNumberElement = null;

            //if (!employeesPaidBelowPAYEThreshold)
            //{
            //    empReferenceNumberElement = new XElement("EmployersRefNo", standardQuestions.Element("EmployersReferenceNumber").Value);
            //}

            //IEnumerable<XElement> individualNames = null;

            //if (standardQuestions.Descendants("Individual").Any())
            //{
            //    individualNames =

            //    from Individual in standardQuestions.Descendants("Individual")
            //    select new XElement("IndividualName",
            //                            new XElement("TitleCode",
            //                                Individual.Element("Title").CodeListXElement()),
            //                            new XElement("FirstForeName",
            //                                Individual.Element("FirstName").Value),
            //                            new XElement("Surname",
            //                                Individual.Element("LastName").Value));
            //}

            //IEnumerable<XElement> claimsElements = GetClaimElements(claimQuestions);

            //XElement otherTradingName = standardQuestions.Element("TradingNameTwo") != null ?
            //                            new XElement("TradingName", new XElement("Name", standardQuestions.Element("TradingNameTwo").Value)) : null;


            //XElement xElementInsured = new XElement("Insured",
            //                                new XElement("CompanyStatusCode", standardQuestions.Element("CompanyStatus").CodeListXElement()),
            //                                new XElement("CompanyName", standardQuestions.Element("TradingName").Value),
            //                                new XElement("PAYEThresholdInd", new XElement("Value",
            //                                                                 standardQuestions.Element("AllEmployeesPaidBelowPAYEThreshold").Value)),
            //                                empReferenceNumberElement,
            //                                individualNames,
            //                                new XElement("Address",
            //                                                       new XElement("Line1", standardQuestions.Element("AddressInformation").Element("AddressLineOne").Value),
            //                                                       new XElement("Line2", standardQuestions.Element("AddressInformation").Element("AddressLineTwo").Value),
            //                                                       new XElement("Postcode", standardQuestions.Element("AddressInformation").Element("Postcode").Value)),
            //                                new XElement("Trade",  new XElement("Code",
            //                                                                    standardQuestions.Element("Trade").CodeListXElement())),
            //                                otherTradingName,
            //                                new XElement("YearBusinessEstablished", standardQuestions.Element("YearEstablished").Value),
            //                                new XElement("Declaration",
            //                                                           new XElement("LossesInd", 
            //                                                                                    new XElement("Value",claimQuestions.Element("AnyLossesOrIncidentsInLastFiveYears").Value)),
            //                                                                                    claimsElements,
            //                                                           new XElement("ConvictionsInd",
            //                                                                                      new XElement("Value",declarationQuestions.Element("ConvictionsCriminalOffencesOrProsecutionsOtherThanMotorOffences").Value)),
            //                                                           new XElement("BankruptInd",
            //                                                                                      new XElement("Value",declarationQuestions.Element("DeclaredBankruptOrInsolvent").Value)),
            //                                                           new XElement("InsuranceInd",
            //                                                                                      new XElement("Value",declarationQuestions.Element("InsuranceForRiskOrSimilarHeldByInsured").Value)),
            //                                                           new XElement("PreviouslyInsuredInd",
            //                                                                                      new XElement("Value",declarationQuestions.Element("ProposalRefusedOrDeclined").Value)),
            //                                                           new XElement("RegulatoryReformInd",
            //                                                                                      new XElement("Value",declarationQuestions.Element("BusinessComplyWithRequirementsOfTheRegulatoryReformOrder2005").Value))
            //                                                                                         )
                                            
            //                                //subsidiaries(preQuoteData)

            //                                );


            //Console.WriteLine(xElementInsured.ToString());
            Console.ReadLine();

            //#endregion

        }

        private static IEnumerable<XElement> GetClaimElements(XElement claimQuestions)
        {
            IEnumerable<XElement> claims = null;
            var claimsEntered = Convert.ToBoolean(claimQuestions.Element("AnyLossesOrIncidentsInLastFiveYears").Value);
            if (claimsEntered) 
            { 
                claims =
                from claimDetails in claimQuestions.Descendants("ClaimDetail")
                select new XElement("Losses",
                                             new XElement("ClaimReportingStatus", claimDetails.Element("ClaimReportingStatus").CodeListXElement()),
                                             new XElement("OccurenceDate", XmlConvert.ToDateTime(claimDetails.Element("OccuranceDate").Value, XmlDateTimeSerializationMode.Local).ToString("yyyy-MM-dd")), //ask culture info
                                             new XElement("CauseCode", claimDetails.Element("CauseOfLoss").CodeListXElement()),
                                             new XElement("MonetaryAmount", 
                                                                           new XElement("Amount",claimDetails.Element("TotalAmountOfClaim").Value)),
                                             new XElement("Premises",
                                                                     claimDetails.Element("PropertyId") == null ? null : new XElement("Id", claimDetails.Element("PropertyId").Value),
                                                                     new XElement("Address",
                                                                                            new XElement("Postcode", claimDetails.Element("PropertyPostcode").Value))),
                                             new XElement("LossBreakdown",
                                                                          new XElement("CoverCode", claimDetails.Element("LossBreakdownApplicableCover").CodeListXElement()))
                                             );
            }
            return claims;
        }
    }

    public static class Extensions
    {
        public static IEnumerable<XElement> CodeListXElement(this XElement source)
        {
            return new List<XElement>(){
                        { new XElement("Value", source.Element("Value").Value) },
                        { new XElement("ShortDescription", source.Element("ShortDescription").Value) }};
        }
    }

    public class DoubleFormatter : IFormatProvider, ICustomFormatter
    {
        // always use dot separator for doubles
        private CultureInfo enUsCulture = CultureInfo.GetCultureInfo("en-US");

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            // format doubles to 3 decimal places
            return string.Format(enUsCulture, "{0:0.000}", arg);
        }

        public object GetFormat(Type formatType)
        {
            return (formatType == typeof(ICustomFormatter)) ? this : null;
        }
    }

}
