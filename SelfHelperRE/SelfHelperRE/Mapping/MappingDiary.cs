using AutoMapper;
using Repository;

namespace Mapping
{
    public class MappingDiary<T> : Profile
    {
        public MappingDiary()
        {
            CreateMap<Diary, T>();
            CreateMap<T, Diary>();

        }
    }
}
