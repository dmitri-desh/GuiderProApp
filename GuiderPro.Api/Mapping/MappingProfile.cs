using AutoMapper;
using GuiderPro.Api.DTOs;
using GuiderPro.Core.Entities;

namespace GuiderPro.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Place, PlaceDTO>()
           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)) // Маппинг CategoryName из Category
           .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.Tags.Select(tag => tag.Id).ToList())); // Маппинг TagIds из коллекции Tags

            // Маппинг для PlaceDTO -> Place
            CreateMap<PlaceDTO, Place>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                    src.TagIds.Select(tagId => new Tag { Id = tagId, Name = string.Empty }).ToList())) // Маппинг коллекции Tags из TagIds
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // Игнорируем прямое присваивание Category, так как оно связывается по CategoryId

            // Маппинг для Tag -> TagDTO
            CreateMap<Tag, TagDTO>().ReverseMap();

            // Маппинг для Category -> CategoryDTO
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
