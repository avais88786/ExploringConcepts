using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTOMReverseEngineerXML.AutoMapperProfiles
{
    public class BaseProfile : Profile
    {
        private Type sourceType;
        private Type destinationType;

        public BaseProfile(Type source, Type destination)
        {
            sourceType = source;
            destinationType = destination;
        }

        protected override void Configure()
        {
            BaseConfigurations.ConfigureAndInitializeForBusinessLine(sourceType, destinationType);
        }
    }
}
