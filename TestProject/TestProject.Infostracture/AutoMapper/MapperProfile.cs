using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Dtos;
using TestProject.Core.ViewModels;
using TestProject.Data.Models;

namespace TestProject.Infostracture.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserViewModel>().ForMember(x => x.UserType, x => x.MapFrom(x => x.UserType.ToString()));
            CreateMap<CreateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<UpdateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<User, UpdateUserDto>().ForMember(x => x.Image, x => x.Ignore());

            CreateMap<Category, CategoryViewModel>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, UpdateCategoryDto>();

            CreateMap<Advertisement, AdvertisementViewModel>().ForMember(x => x.StartDate , x => x.MapFrom(x => x.StartDate.ToString("yyyy/MM/dd"))).ForMember(x => x.EndDate, x => x.MapFrom(x => x.EndDate.ToString("yyyy/MM/dd")));
            CreateMap<CreateAdvertisementDto, Advertisement>().ForMember(x => x.ImgUrl , x => x.Ignore()).ForMember(x => x.Owner, x => x.Ignore());
            CreateMap<UpdateAdvertisementDto, Advertisement>().ForMember(x => x.ImgUrl, x => x.Ignore()).ForMember(x => x.Owner, x => x.Ignore());
            CreateMap<Advertisement, UpdateAdvertisementDto>().ForMember(x => x.ImgUrl, x => x.Ignore());

            CreateMap<Track, TrackViewModel>().ForMember(x => x.CreatedAt , x=> x.MapFrom(x => x.CreatedAt.ToString("yyyy/MM/dd")))
                                              .ForMember(x => x.Status, x => x.MapFrom(x => x.Status.ToString()));
            CreateMap<CreateTrackDto, Track>().ForMember(x => x.TrackUrl, x => x.Ignore());
            CreateMap<UpdateTrackDto, Track>().ForMember(x => x.TrackUrl, x => x.Ignore());
            CreateMap<Track, UpdateTrackDto>().ForMember(x => x.TrackUrl, x => x.Ignore());


            CreateMap<Post, PostviewModel>().ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy/MM/dd")))
                                            .ForMember(x => x.Status, x => x.MapFrom(x => x.Status.ToString()));
            CreateMap<CreatePostDto, Post>().ForMember(x => x.Attatchments , x => x.Ignore());
            CreateMap<UpdatePostDto, Post>().ForMember(x => x.Attatchments, x => x.Ignore());
            CreateMap<Post, UpdatePostDto>().ForMember(x => x.Attachments, x => x.Ignore()).ForMember(x => x.PostAttachments, x => x.Ignore());
            CreateMap<PostAttatchment, PostAttachmentViewModel>();


            CreateMap<ContentChangeLog, ContentChangeLogViewModel>().ForMember(x => x.ChangeAt, x => x.MapFrom(x => x.ChaneAt.ToString("yyyy/MM/dd hh:mm")));
        }
    }
}
