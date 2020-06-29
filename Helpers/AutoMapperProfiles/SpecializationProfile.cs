using AutoMapper;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Helpers.AutoMapperProfiles
{
    public class SpecializationProfile : Profile
    {
        public SpecializationProfile()
        {
            CreateMap<int, Specialization>().ConvertUsing<EntityConventer<Specialization>>();

            CreateMap<SpecializationDTO, Specialization>()
                .ForMember(dest => dest.Major, opt =>
                {
                    opt.MapFrom(src => src.MajorID);
                });

            CreateMap<Specialization, SpecializationSendDTO>();

            CreateMap<Specialization, SpecializationDefaultInformationsDTO>();
        }
    }
}
