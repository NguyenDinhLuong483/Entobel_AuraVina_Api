using AutoMapper;
using ENTOBEL_AURAVINA_API.Domains.Models;
using ENTOBEL_AURAVINA_API.Resources;

namespace ENTOBEL_AURAVINA_API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<AddUserViewModel, User>();
        }
    }
}
