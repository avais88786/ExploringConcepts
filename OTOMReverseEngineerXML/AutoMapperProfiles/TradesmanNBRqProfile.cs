using AutoMapper;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.BusinessDetailsQuestions;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.Claims;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.CoversRequired;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.CoversRequired.AdditionalCover;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.CoversRequired.BusinessInterruptionCover;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.CoversRequired.ContractWorksCover;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.CoversRequired.HiredInPlantCover;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.CoversRequired.OwnPlantCover;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.CoversRequired.PersonalAccidentCover;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.CoversRequired.ProfessionalIndemnityCover;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.CoversRequired.ToolsCover;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.CoversRequired.TravelCover;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.CoversRequired.TurnoverCover;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.DeclarationQuestions;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Tradesman.EmploymentAndTurnover;
using OpenGI.MVC.ViewModels.Tradesman;
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

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanCoversRequiredTab>()
                .ForMember(d => d.IncludeAdditionalCover, s => s.MapFrom(src => src.AdditionalCover.Any() ? true : false))
                .ForMember(d => d.IncludeBusinessInterruptionCover, s => s.MapFrom(src => src.BusinessInterruptionCover != null ? true : false))
                .ForMember(d => d.IncludeContractWorksCover, s => s.MapFrom(src => src.ContractWorksCover != null ? true : false))
                .ForMember(d => d.IncludeEmployerLiabilityCover, s => s.MapFrom(src => src.EmployersLiabilityCover.CoverRequiredInd.Value))
                .ForMember(d => d.IncludeHiredInPlantCover, s => s.MapFrom(src => src.HiredInPlantCover != null ? true : false))
                .ForMember(d => d.IncludeOwnPlantCover, s => s.MapFrom(src => src.OwnPlantCover != null ? true : false))
                .ForMember(d => d.IncludePersonalAccidentCover, s => s.MapFrom(src => src.PersonalAccidentCover.Any() ? true : false))
                .ForMember(d => d.IncludeProfessionalIndemnityCover, s => s.MapFrom(src => src.ProfessionalIndemnityCover != null ? true : false))
                .ForMember(d => d.IncludeToolsCover, s => s.MapFrom(src => src.ToolCover.Any() ? true : false))
                .ForMember(d => d.IncludeTravelCover, s => s.MapFrom(src => src.TravelCover != null ? true : false))
                .ForMember(d => d.PublicLiabilityIndemnityLimit, s => s.MapFrom(src => src.PublicLiabilityCover.CoverDetail.SumInsured.Amount))
                .ForMember(d => d.PublicLiabilityAdditionalExcessRequired, s => s.MapFrom(src => src.PublicLiabilityCover.CoverDetail.Excess.Amount));
                //.ForMember(d => d.BusinessInterruptionCover, s => s.MapFrom(src => src));

            Mapper.CreateMap<TradesmanAllNBRq, AdditionalCoverLogicalGroup>()
                .ForMember(d => d.AdditionalCover, s => s.MapFrom(src => src.AdditionalCover));

            Mapper.CreateMap<TradesmanAllNBRqAdditionalCover, AdditionalCover>()
                .ForMember(d => d.AdditionalType, s => s.MapFrom(src => src.Code))
                .ForMember(d => d.AdditionalSumInsured, s => s.MapFrom(src => src.SumInsured.Amount));

            Mapper.CreateMap<TradesmanAllNBRq, BusinessInterruptionCoverLogicalGroup>()
               //.ForMember(d => d.AdditionalBusinessInterruptionCover, s => s.MapFrom(src => src))
               .ForMember(d => d.BusinessInterruptionCoverBasis, s => s.MapFrom(src => src.BusinessInterruptionCover.CoverDetail.BasisCode))
               .ForMember(d => d.BusinessInterruptionIndemnityPeriod, s => s.MapFrom(src => src.BusinessInterruptionCover.CoverDetail.IndemnityPeriod))
               .ForMember(d => d.BusinessInterruptionSumInsured, s => s.MapFrom(src => src.BusinessInterruptionCover.CoverDetail.SumInsured.Amount))
               .ForMember(d => d.IncludeAdditionalBusinessInterruptionCover, s => s.MapFrom(src => src.BusinessInterruptionCover.CoverDetail.AdditionalCover.Any() ? true : false));

            Mapper.CreateMap<TradesmanAllNBRq, AdditionalBusinessInterruptionCoverLogicalGroup>()
                .ForMember(d => d.AdditionalBusinessInterruptionCover, s => s.MapFrom(src => src.BusinessInterruptionCover.CoverDetail.AdditionalCover));

            Mapper.CreateMap<TradesmanAllNBRqBusinessInterruptionCoverCoverDetailAdditionalCover, AdditionalBusinessInterruptionCover>()
                .ForMember(d => d.AdditionalBusinessInterruptionSumInsured, s => s.MapFrom(src => src.SumInsured.Amount))
                .ForMember(d => d.AdditionalBusinessInterruptionType, s => s.MapFrom(src => src.Code));

            Mapper.CreateMap<TradesmanAllNBRq, ContractWorksCoverLogicalGroup>()
                .ForMember(d => d.ContractWorksExcess, s => s.MapFrom(src => src.ContractWorksCover.CoverDetail.Excess.Amount))
                .ForMember(d => d.ContractWorksSumInsured, s => s.MapFrom(src => src.ContractWorksCover.CoverDetail.SumInsured.Amount));

            Mapper.CreateMap<TradesmanAllNBRq, HiredInPlantCoverLogicalGroup>()
                .ForMember(d => d.HiredInPlantAnnualCharges, s => s.MapFrom(src => src.HiredInPlantCover.CoverDetail.HiringCharges))
                .ForMember(d => d.HiredInPlantExcess, s => s.MapFrom(src => src.HiredInPlantCover.CoverDetail.Excess.Amount))
                .ForMember(d => d.HiredInPlantSumInsured, s => s.MapFrom(src => src.HiredInPlantCover.CoverDetail.SumInsured.Amount));

            Mapper.CreateMap<TradesmanAllNBRq, OwnPlantCoverLogicalGroup>()
               .ForMember(d => d.OwnPlantExcess, s => s.MapFrom(src => src.OwnPlantCover.CoverDetail.Excess.Amount))
               .ForMember(d => d.OwnPlantMaximumValueOfAnyOneItem, s => s.MapFrom(src => src.OwnPlantCover.CoverDetail.Plant.Value))
               .ForMember(d => d.OwnPlantSumInsured, s => s.MapFrom(src => src.OwnPlantCover.CoverDetail.SumInsured.Amount));

            Mapper.CreateMap<TradesmanAllNBRq, PersonalAccidentCoverLogicalGroup>()
               .ForMember(d => d.PersonalAccidentCover, s => s.MapFrom(src => src.PersonalAccidentCover));

            Mapper.CreateMap<TradesmanAllNBRqCoverDetail1, PersonalAccidentCover>()
                .ForMember(d => d.CapitalBenefitAmount, s => s.MapFrom(src => src.CapitalBenefit.Amount))
                .ForMember(d => d.PersonalAccidentEmploymentType, s => s.MapFrom(src => src.GroupDetail.EmploymentTypeCode))
                .ForMember(d => d.PersonalAccidentIndividualCover, s => s.MapFrom(src => src.IndividualDetail));

            Mapper.CreateMap<TradesmanAllNBRqCoverDetailIndividualDetail, PersonalAccidentIndividualCover>()
                .ForMember(d => d.PersonalAccidentIndividualDateOfBirth, s => s.MapFrom(src => src.BirthDate))
                .ForMember(d => d.PersonalAccidentIndividualForename, s => s.MapFrom(src => src.IndividualName.FirstForename))
                .ForMember(d => d.PersonalAccidentIndividualSurname, s => s.MapFrom(src => src.IndividualName.Surname))
                .ForMember(d => d.PersonalAccidentIndividualTitle, s => s.MapFrom(src => src.IndividualName.TitleCode));

            Mapper.CreateMap<TradesmanAllNBRq, ProfessionalIndemnityCoverLogicalGroup>()
              .ForMember(d => d.Accreditation, s => s.MapFrom(src => src.ProfessionalIndemnityCover.CoverDetail.Accreditation))
              .ForMember(d => d.ProfessionalIndemnityUndertakeDesignWork, s => s.MapFrom(src => src.ProfessionalIndemnityCover.CoverDetail.Declaration.DesignUndertakenInd.Value))
              .ForMember(d => d.ProfessionalIndemnityDateCoverRequired, s => s.MapFrom(src => src.ProfessionalIndemnityCover.CoverDetail.RetroactiveDate))
              .ForMember(d => d.ProfessionalIndemnityFiveYearsExperience, s => s.MapFrom(src => src.ProfessionalIndemnityCover.CoverDetail.Declaration.YearsExperienceInd.Value))
              .ForMember(d => d.ProfessionalIndemnityGiveTaxationAdvice, s => s.MapFrom(src => src.ProfessionalIndemnityCover.CoverDetail.Declaration.TaxationAdviceInd.Value))
              .ForMember(d => d.ProfessionalIndemnityRelevantQualifications, s => s.MapFrom(src => src.ProfessionalIndemnityCover.CoverDetail.Declaration.RelevantQualificationsInd.Value))
              .ForMember(d => d.ProfessionalIndemnitySumInsured, s => s.MapFrom(src => src.ProfessionalIndemnityCover.CoverDetail.SumInsured.Amount))
              .ForMember(d => d.ProfessionalIndemnityUndertakePropertyManagement, s => s.MapFrom(src => src.ProfessionalIndemnityCover.CoverDetail.Declaration.PropertyManagementInd.Value));

            Mapper.CreateMap<TradesmanAllNBRqProfessionalIndemnityCoverCoverDetailAccreditation, Accreditation>()
              .ForMember(d => d.QualificationsHeld, s => s.MapFrom(src => src.Code));

            Mapper.CreateMap<TradesmanAllNBRq, DesignWorkTypeLogicalGroup>()
             .ForMember(d => d.DesignWorkType, s => s.MapFrom(src => src.ProfessionalIndemnityCover.CoverDetail.Declaration.Activity.Code));

            Mapper.CreateMap<TradesmanAllNBRq, ToolsCoverLogicalGroup>()
              .ForMember(d => d.ToolsCover, s => s.MapFrom(src => src.ToolCover));

            Mapper.CreateMap<TradesmanAllNBRqCoverDetail, ToolsCover>()
                .ForMember(d => d.IncludeAdditionalToolsCover, s => s.MapFrom(src => src.AdditionalCover != null ? true : false))
                .ForMember(d => d.ToolsEmploymentType, s => s.MapFrom(src => src.Employee.EmploymentTypeCode))
                .ForMember(d => d.ToolsNumberOfEmployees, s => s.MapFrom(src => src.Employee.NoOfEmployees))
                .ForMember(d => d.ToolsSumInsured, s => s.MapFrom(src => src.SumInsured.Amount))
                .ForMember(d => d.AdditionalToolsType, s => s.MapFrom(src => src.AdditionalCover));

            Mapper.CreateMap<TradesmanAllNBRqCoverDetailAdditionalCover, AdditionalToolsTypeLogicalGroup>()
                .ForMember(d => d.AdditionalToolsType, s => s.MapFrom(src => src.Code));

            Mapper.CreateMap<TradesmanAllNBRq, TravelCoverLogicalGroup>()
              .ForMember(d => d.TravelAnnualPersonDays, s => s.MapFrom(src => src.TravelCover.CoverDetail.NoOfPersonDays))
              .ForMember(d => d.TravelGeographicalLimit, s => s.MapFrom(src => src.TravelCover.CoverDetail.LocationCode));

            Mapper.CreateMap<TradesmanAllNBRq, TurnoverCoverLogicalGroup>()
              .ForMember(d => d.LargestClientFee, s => s.MapFrom(src => src.Insured.Turnover.Fees.MaxAmount))
              .ForMember(d => d.MaximumContractValue, s => s.MapFrom(src => src.Insured.Turnover.Contract.MonetaryAmount.MaxAmount));

            Mapper.CreateMap<TradesmanAllNBRq, Turnover>()
              .ForMember(d => d.AnnualTurnover, s => s.MapFrom(src => src.Insured.Turnover.TurnoverAmount));

            Mapper.CreateMap<TradesmanAllNBRq, Employment>()
                //.ForMember(d => d.AllEmployeesPaidBelowPAYEThreshold, s => s.MapFrom(src => src.Employee.Wages)); //cannot find this
              .ForMember(d => d.EstimatedWages, s => s.MapFrom(src => src.Employee.Wages))
              .ForMember(d => d.NoOfPeopleInBusiness, s => s.MapFrom(src => src.Employee.NoOfEmployees))
              .ForMember(d => d.EmployeeDetails, s => s.MapFrom(src => src.Employee.EmployeeDetail));

            Mapper.CreateMap<TradesmanAllNBRqEmployeeEmployeeDetail, EmployeeDetails>()
                .ForMember(d => d.EmploymentType, s => s.MapFrom(src => src.EmploymentTypeCode))
                .ForMember(d => d.EmployeeGeneralActivity, s => s.MapFrom(src => src.GeneralActivity));

            Mapper.CreateMap<TradesmanAllNBRqEmployeeEmployeeDetailGeneralActivity, EmployeeGeneralActivity>()
                .ForMember(d => d.GeneralActivity, s => s.MapFrom(src => src.Code))
                .ForMember(d => d.NoOfWorkers, s => s.MapFrom(src => src.NoOfWorkers));

            Mapper.CreateMap<TradesmanAllNBRq, TradesmanClaimsTab>()
                .ForMember(d => d.AnyLossesOrIncidentsInLastFiveYears, s => s.MapFrom(src => src.Insured.Declaration.LossesInd.Value));

            Mapper.CreateMap<TradesmanAllNBRq, ClaimsDetailsGroup>()
                .ForMember(d => d.ClaimDetails, s => s.MapFrom(src => src.Insured.Declaration.Losses));

            Mapper.CreateMap<TradesmanAllNBRqInsuredDeclarationLosses, TradesmanClaimDetail>()
                .ForMember(d => d.CauseOfLoss, s => s.MapFrom(src => src.CauseCode))
                .ForMember(d => d.ClaimReporting, s => s.MapFrom(src => src.ClaimReportingStatus))
                .ForMember(d => d.LossBreakdown, s => s.MapFrom(src => src.LossBreakdown.CoverCode))
                .ForMember(d => d.OccuranceDate, s => s.MapFrom(src => src.OccurrenceDate))
                .ForMember(d => d.TotalAmount, s => s.MapFrom(src => src.MonetaryAmount.Amount));




        }
    }
}
