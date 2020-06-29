using AutoMapper;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Helpers.AutoMapperProfiles
{
    public class MajorProfile : Profile
    {
        public MajorProfile()
        {
            CreateMap<int, Major>().ConvertUsing<EntityConventer<Major>>();

            CreateMap<MajorDTO, Major>()
               .ForMember(dest => dest.Institute, opt =>
               {
                   opt.MapFrom(src => src.InstituteID);
               });

            CreateMap<Major, MajorSendDTO>();

            CreateMap<Major, MajorDefaultInformationsDTO>();
        }
    }
}
