using AutoMapper;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.BusinessDetailsQuestions;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.DeclarationQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTOMReverseEngineerXML.AutoMapperProfiles
{
    public class TradesmanNBRqProfile : BaseProfile
    {
        public TradesmanNBRqProfile()
            : base(typeof(TradesmanAllNBRq), typeof(TradesmanDataCapture))
        {

        }

        protected override void Configure()
        {
            
            base.Configure();

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanDeclarationGroup>()
                .ForMember(d => d.DeclaredBankrupt, opt => opt.MapFrom(model => model.Insured.Declaration.BankruptInd.Value))
                .ForMember(d => d.DischargeOfFumes, opt => opt.MapFrom(model => model.Insured.Declaration.DischargeOfWasteInd.Value))
                .ForMember(d => d.HarmfulSubstances, opt => opt.MapFrom(model => model.Insured.Declaration.HarmfulSubstancesInd.Value))
                .ForMember(d => d.ProposalRefused, opt => opt.MapFrom(model => model.Insured.Declaration.InsuranceInd.Value))
                .ForMember(d => d.SeparateDedicatedBusinessPremises, opt => opt.MapFrom(model => model.Insured.Declaration.BusinessPremisesInd.Value))
                .ForMember(d => d.UnSpentConvictions, opt => opt.MapFrom(model => model.Insured.Declaration.ConvictionsInd.Value));

            Mapper.CreateMap<TradesmanAllNBRq, BusinessDetailsGroup>()
                .ForMember(d => d.CompanyStatus, s => s.MapFrom(model => model.Insured.CompanyStatusCode))
                .ForMember(d => d.NumberOfYearsExperience, s => s.MapFrom(model => model.Insured.YearsExperience))
                .ForMember(d => d.AddressInformation, s => s.MapFrom(model => model.Insured.Address))
                .ForMember(d => d.TradingName, s => s.MapFrom(model => model.Insured.CompanyName))
                .ForMember(d => d.TheTradingName, s => s.MapFrom(model => model.Insured.TradingName.Name))
                .ForMember(d => d.YearEstablished, s => s.MapFrom(model => model.Insured.YearBusinessEstablished))
                .ForMember(d => d.NumberOfYearsExperience, s => s.MapFrom(model => model.Insured.YearsExperience));

            Mapper.CreateMap<TradesmanAllNBRq, IndividualLogicalGroup>()
                .ForMember(d => d.Individuals, s => s.MapFrom(model => model.Insured.IndividualName));

            Mapper.CreateMap<TradesmanAllNBRqInsuredIndividualName, TradesmanIndividual>()
                .ForMember(d => d.FirstName, s => s.MapFrom(model => model.FirstForename))
                .ForMember(d => d.LastName, s => s.MapFrom(model => model.Surname))
                .ForMember(d => d.IndividualTitle, s => s.MapFrom(model => model.TitleCode));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanStandardQuestionsGroup>()
                .ForMember(d => d.CoverStartDate, s => s.MapFrom(model => model.StartDate.Add(model.StartTime.TimeOfDay)))
                .ForMember(d => d.CurrentPreviousInsurer, s => s.MapFrom(model => model.PriorInsurer.Code))
                //.ForMember(d => d.InsurerInformation, s => s.MapFrom(model => model.PriorInsurer)); cannot find
                .ForMember(d => d.TargetPremium, s => s.MapFrom(model => model.Policy.TargetPremium));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanSubsidiaryCompaniesGroup>()
                .ForMember(d => d.SubsidiaryCompaniesExist, s => s.MapFrom(model => model.Insured.Subsidiary.Count() > 0 ? true : false));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanSubsidiaryCompaniesLogicalGroup>()
                .ForMember(d => d.SubsidiaryCompanies, s => s.MapFrom(model => model.Insured.Subsidiary));

            Mapper.CreateMap<TradesmanAllNBRqInsuredSubsidiary, TradesmanSubsidiaryCompany>()
                .ForMember(d => d.CompanyName, s => s.MapFrom(model => model.CompanyName))
                .ForMember(d => d.CoveredUnderPolicy, s => s.MapFrom(model => model.SubsidiaryIncludedInd.Value))
                .ForMember(d => d.EmployersReferenceNumberTwo, s => s.MapFrom(model => model.EmployersRefNo))
                .ForMember(d => d.ExemptFromHMCEEmployersReferenceNumber, s => s.MapFrom(model => model.PAYEThresholdInd.Value));

            Mapper.CreateMap<TradesmanAllNBRq, GasFitLogicalGroup>()
                .ForMember(d => d.IsGasFitOrInstallWorkUndertaken, s => s.MapFrom(model => model.Insured.GeneralActivity.GasFittingInstallationInd.Value));

            Mapper.CreateMap<TradesmanAllNBRq, Phase3LogicalGroup>()
                .ForMember(d => d.IsPhase3ElecWorkUndertaken, s => s.MapFrom(model => model.Insured.GeneralActivity.Phase3ElectricalWorkInd.Value));

            Mapper.CreateMap<TradesmanAllNBRq, MaximumDepthLogicalGroup>()
                .ForMember(d => d.MaximumDepthWorkUndertakenGroup, s => s.MapFrom(model => model.Insured.GeneralActivity.DepthLimit));

            Mapper.CreateMap<TradesmanAllNBRq, MaximumHeightLogicalGroup>()
                .ForMember(d => d.MaximumHeightWorkCarriedOut, s => s.MapFrom(model => model.Insured.GeneralActivity.HeightLimit));

            Mapper.CreateMap<TradesmanAllNBRq, TradeDetailsQuestionGroup>()
                .ForMember(d => d.Trades, s => s.MapFrom(model => model.Insured.Trade));
            // not there in schema ? //.ForMember(d => d.TradesmanHeatCoverLogicalGroup, s => s.MapFrom(model => model.Insured.Trade));

            Mapper.CreateMap<TradesmanAllNBRqInsuredTrade, TradesmanTrade>()
                .ForMember(d => d.NumberOfWorkers, s => s.MapFrom(model => model.NoOfWorkers))
                .ForMember(d => d.PercentageTurnover, s => s.MapFrom(model => model.TurnoverPercent))
                .ForMember(d => d.TradeType, s => s.MapFrom(model => model.Code))
                .ForMember(d => d.IsMainTrade, s => s.MapFrom(model => model.MainInd.Value));

            Mapper.CreateMap<TradesmanAllNBRqInsuredTrade, TreatmentDetailsLogicalGroup>()
                .ForMember(d => d.TreatmentDetails, s => s.MapFrom(model => model.Treatment));

            Mapper.CreateMap<TradesmanAllNBRqInsuredTradeTreatment, TreatmentDetail>()
                 .ForMember(d => d.NoofWorkers, s => s.MapFrom(model => model.NoOfWorkers))
                 .ForMember(d => d.TreatmentDetailsType, s => s.MapFrom(model => model.TreatmentCode));

        }
    }
}
