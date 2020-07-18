using AutoMapper;

using System;
using System.Collections.Generic;
using System.Text;

namespace XPY.WebsiteSolution.Models.MapperProfiles
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            CreateMap<SampleUserModel,SampleUser>();
        }
    }
}
