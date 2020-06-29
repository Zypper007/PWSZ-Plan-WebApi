using AutoMapper;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            #region User
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
            #endregion

            #region Institute
            CreateMap<int, Institute>().ConvertUsing<EntityConventer<Institute>>();

            CreateMap<InstituteDTO, Institute>();

            CreateMap<Institute, InstituteSendDTO>();
            #endregion

            #region Major
            CreateMap<int, Major>().ConvertUsing<EntityConventer<Major>>();

            

            CreateMap<MajorDTO, Major>()
               .ForMember(dest => dest.Institute, opt =>
               {
                   opt.MapFrom(src => src.InstituteID);
               });

            CreateMap<Major, MajorSendDTO>();
            #endregion

            #region Specialization
            CreateMap<int, Specialization>().ConvertUsing<EntityConventer<Specialization>>();

            CreateMap<SpecializationDTO, Specialization>()
                .ForMember(dest => dest.Major, opt =>
                {
                    opt.MapFrom(src => src.MajorID);
                });

            CreateMap<Specialization, SpecializationSendDTO>();
            #endregion

            #region Year
            CreateMap<int, Year>().ConvertUsing<EntityConventer<Year>>();

            CreateMap<YearDTO, Year>()
                .ForMember(dest => dest.Specialization, opt =>
                {
                    opt.MapFrom(src => src.SpecializationID);
                }); ;

            CreateMap<Year, YearSendDTO>();
            #endregion

            #region Plan
            CreateMap<PlanDTO, Plan>()
                .ForMember(dest => dest.Year, opt => 
                {
                    opt.MapFrom(src => src.YearID);
                })
                .ForMember(dest => dest.Classes, opt =>
                {
                    opt.MapFrom(src => src.ClassesID);
                });

            CreateMap<Plan, PlanSendDTO>()
                .ForMember(dest => dest.InstituteName, opt => 
                {
                    opt.MapFrom(src => src.Year.Specialization.Major.Institute.Name);
                })
                .ForMember(dest => dest.MajorName, opt =>
                {
                    opt.MapFrom(src => src.Year.Specialization.Major.Name);
                })
                .ForMember(dest => dest.SpecializatonName, opt =>
                {
                    opt.MapFrom(src => src.Year.Specialization.Name);
                })
                .ForMember(dest => dest.YearName, opt =>
                {
                    opt.MapFrom(src => src.Year.Name);
                });
            #endregion

            #region Class
            CreateMap<ClassDTO, Class>();

            #endregion

            #region Room
            CreateMap<RoomDTO, Room>();

            CreateMap<Room, RoomSendDTO>();
            #endregion

            #region Subject
            CreateMap<SubjectDTO, Subject>();

            CreateMap<Subject, SubjectSendDTO>();
            #endregion

            #region Information

            #endregion
        }
    }
}
