using AutoMapper;
using WorkManagerDal.Models;
using WorkManagerDal.ViewModels;

namespace RiskerWorkManager.Services
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() {
            CreateMap<Role, RoleVm>();
            CreateMap<User, UserVm>().ReverseMap();
        }
    }
}
