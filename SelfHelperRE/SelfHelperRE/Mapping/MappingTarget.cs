using AutoMapper;
using Repository;

namespace Mapping
{
    public class MappingTarget<T> : Profile
    {
        public MappingTarget()
        {
            CreateMap<Target, T>();
            CreateMap<T, Target>();

        }
    }
}
