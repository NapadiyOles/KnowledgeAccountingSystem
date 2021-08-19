using AutoMapper;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.Mapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<Skill, SkillModel>()
                .ReverseMap();

            CreateMap<Programmer, ProgrammerModel>()
                .ForMember(x => x.Name, z => z.MapFrom(y => y.User.Name))
                .ForMember(x => x.Surname, z => z.MapFrom(y => y.User.Surname))
                .ForMember(x => x.Skills, z => z.MapFrom(y => y.Skills))
                .ReverseMap();

            CreateMap<Programmer, ProgrammerFullModel>()
                .ForMember(x => x.Name, z => z.MapFrom(y => y.User.Name))
                .ForMember(x => x.Surname, z => z.MapFrom(y => y.User.Surname))
                .ForMember(x => x.Email, z => z.MapFrom(y => y.User.Email))
                .ForMember(x => x.Skills, z => z.MapFrom(y => y.Skills))
                .ReverseMap();

            CreateMap<Manager, ManagerModel>()
                .ReverseMap();
        }
    }
}
