using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entity;
using Webapi.Model;

namespace Webapi.AutoMapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, ResUserModel>()
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.sex, opt => opt.MapFrom(src => src.sex));
        }
    }
}
