using AutoMapper;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Helpers.AutoMapperProfiles
{
    public class YearProfile: Profile
    {
        public YearProfile()
        {
            CreateMap<int, Year>().ConvertUsing<EntityConventer<Year>>();

            CreateMap<YearDTO, Year>()
                .ForMember(dest => dest.Specialization, opt =>
                {
                    opt.MapFrom(src => src.SpecializationID);
                });

            CreateMap<Year, YearSendDTO>();

            CreateMap<Year, YearDefaultInformationsDTO>();
        }
    }
}
