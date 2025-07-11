using AutoMapper;
using Store.G01.Core.Dtos.Auth;
using Store.G01.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Core.Mapping.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}
