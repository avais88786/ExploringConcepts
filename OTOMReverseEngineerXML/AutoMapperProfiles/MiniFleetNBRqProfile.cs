using AutoMapper;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Fleet;
using OpenGI.MVC.BusinessLines.ViewModels.ViewModels.Fleet.DeclarationQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTOMReverseEngineerXML.AutoMapperProfiles
{
    public class MiniFleetNBRqProfile : BaseProfile
    {
        public MiniFleetNBRqProfile()
            : base(typeof(MiniFleetNBRq), typeof(FleetDataCapture))
        {

        }

        protected override void Configure()
        {
            base.Configure();

            Mapper.CreateMap<MiniFleetNBRq, FleetDeclarationQuestionsTab>()
                .ForMember(d => d.DriversHoldValidLicences, s => s.MapFrom(src => src.RiskProfile.Declarations.ValidDriverLicenceInd.Value))
                .ForMember(d => d.ProposerBankruptOrInsolvent, s => s.MapFrom(src => src.Insured.Declaration.BankruptInd.Value))
                .ForMember(d => d.ProposerBreachHealthAndSafety, s => s.MapFrom(src => src.Insured.Declaration.HealthAndSafetyInd.Value))
                .ForMember(d => d.ProposerCountyCourtJudgement, s => s.MapFrom(src => src.Insured.Declaration.CountyCourtJudgementInd.Value))
                .ForMember(d => d.ProposerDisqualifiedDirectorship, s => s.MapFrom(src => src.Insured.Declaration.DisqualificationInd.Value))
                .ForMember(d => d.ProposerInsuranceRefusedOrCancelled, s => s.MapFrom(src => src.Insured.Declaration.InsuranceInd.Value))
                .ForMember(d => d.ProposerOffencesOrProsecutionsPending, s => s.MapFrom(src => src.Insured.Declaration.ConvictionsInd.Value))
                .ForMember(d => d.ProposerRecoveryAction, s => s.MapFrom(src => src.Insured.Declaration.CustomsOrInlandRevActionInd.Value))
                .ForMember(d => d.VehiclesCarryHazardousGoods, s => s.MapFrom(src => src.RiskProfile.Declarations.HazardousGoodsInd.Value))
                .ForMember(d => d.Location, s => s.MapFrom(src => src.RiskProfile.Declarations.HazardousIndustry));

            Mapper.CreateMap<MiniFleetNBRqRiskProfileDeclarationsHazardousIndustry, HazardousIndustryOrLocation>()
                .ForMember(d => d.Location, s => s.MapFrom(src => new CodeList() { SelectedDescription = src.DestinationCode.ShortDescription, SelectedValue = src.DestinationCode.Value }))
                .ForMember(d => d.LocationID, s => s.ResolveUsing<IgnoringResolver<int>>())
                .ForMember(d => d.Id, s => s.ResolveUsing<IgnoringResolver<int>>());

            Mapper.CreateMap<MiniFleetNBRqRiskProfileDeclarationsHazardousIndustry, int>();

        }
    }

    public class IgnoringResolver<TDest> : ValueResolver<object,TDest>
    {

        protected override TDest ResolveCore(object source)
        {
            return default(TDest);
        }
    }
}
