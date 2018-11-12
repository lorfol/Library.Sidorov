﻿using AutoMapper;
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
        public MappingProfile() // TODO: create map
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => string.Join(", ", src.Authors.OrderBy(a => a.Name).Select(z => z.Name))))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher.Name))
                .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationDate.Year.ToString()));

            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Name));

            CreateMap<User, UserViewModel>();

            CreateMap<Book, BookUpdateViewModel>()
                .ForMember(dest => dest.SelectedPublisher, opt => opt.MapFrom(src => src.PublisherId));

            CreateMap<Book, BookCreateViewModel>()
                .ForMember(dest => dest.SelectedPublisher, opt => opt.MapFrom(src => src.PublisherId));

            CreateMap<BookUpdateViewModel, Book>()
                .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.SelectedPublisher));

            CreateMap<BookCreateViewModel, Book>()
                .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.SelectedPublisher));

            CreateMap<Author, AuthorCreateViewModel>().ReverseMap();
            CreateMap<Publisher, PublisherCreateViewModel>().ReverseMap();

            //CreateMap<AuthorCreateViewModel, Author>();
            //CreateMap<PublisherCreateViewModel, Publisher>();
        }
    }
}