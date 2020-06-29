using AutoMapper;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Helpers.AutoMapperProfiles
{
    public class InstituteProfile : Profile
    {
        public InstituteProfile()
        {
            CreateMap<int, Institute>().ConvertUsing<EntityConventer<Institute>>();

            CreateMap<InstituteDTO, Institute>();

            CreateMap<Institute, InstituteSendDTO>();

            CreateMap<Institute, InstituteDefaultInformationsDTO>();
        }
    }
}
