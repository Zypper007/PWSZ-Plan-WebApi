using AutoMapper;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Helpers.AutoMapperProfiles
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<PlanDTO, Plan>();
            CreateMap<Plan, PlanDefaultInformationsDTO>()
                .ForMember(dest => dest.Institute, opt => 
                {
                    opt.MapFrom(src => src.Year.Specialization.Major.Institute);
                })
                .ForMember(dest => dest.Major, opt =>
                {
                    opt.MapFrom(src => src.Year.Specialization);
                })
                .ForMember(dest => dest.Specialization, opt =>
                {
                    opt.MapFrom(src => src.Year.Specialization);
                })
                .ForMember(dest => dest.Classes, opt =>
                {
                    opt.MapFrom(src => src.Classes.All(pc => pc.PlanID == src.ID) );
                });
        }
    }
}
