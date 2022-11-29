using AutoMapper;
using WorkManagerDal.Models;
using WorkManagerDal.ViewModels;

namespace RiskerWorkManager.Services
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() {
            CreateMap<Role, RoleVm>().ReverseMap();
            CreateMap<User, UserVm>()
                .ForMember(x => x.RoleName, o => o.MapFrom(s => s.Role == null ? "" : s.Role.Name))
                .ReverseMap();
        }
    }
}
