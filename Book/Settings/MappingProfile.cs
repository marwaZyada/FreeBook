using AutoMapper;
using Domain.Entity;
using Domain.ViewModel;

namespace Book.Settings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryView>().ReverseMap();
        }
    }
}
