using AutoMapper;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Helpers.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<string, byte[]>().ConvertUsing<CodeConventer>();

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.HashCode, opt =>
                {
                    opt.PreCondition(src => src.Code != null);
                    opt.MapFrom(src => src.Code);
                })
                .ForMember(dest => dest.Institute, opt =>
                {
                    opt.PreCondition(src => src.InstituteID != null);
                    opt.MapFrom(src => src.InstituteID);
                });

            CreateMap<User, UserSendDTO>()
                .ForMember(dest => dest.Institute, opt =>
                {
                    opt.PreCondition(src => src.Institute != null);
                });

            CreateMap<User, UserDefaultInformationsDTO>();
        }
    }
}
