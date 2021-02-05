using AutoMapper;
using CoreWebApiTraining.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Models
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<Users, UserModel>().ReverseMap();
        }
    }
}
