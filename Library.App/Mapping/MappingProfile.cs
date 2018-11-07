using AutoMapper;
using Library.App.ViewModels;
using Library.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>();

            CreateMap<Order, OrderViewModel>();

            CreateMap<User, UserViewModel>();

        }
    }
}