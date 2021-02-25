using AutoMapper;
using Repository;

namespace Mapping
{
    public class MappingUser<T> : Profile
    {
        public MappingUser()
        {
            CreateMap<User, T>();
            CreateMap<T, User>();
        }
    }
}
