using AutoMapper;
using BookDirectory.Dtos;
using BookDirectory.Models;
using System;

namespace BookDirectory.MappingProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, ViewBookDto>();

            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateBookDto, Book>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
