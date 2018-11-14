using AutoMapper;
using Library.App.ViewModels;
using Library.Domain.Core.Models;
using System.Linq;

namespace Library.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => string.Join(", ", src.Authors.OrderBy(a => a.Name).Select(z => z.Name))))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher.Name));

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
        }
    }
}