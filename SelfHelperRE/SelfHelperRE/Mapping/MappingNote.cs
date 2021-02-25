using AutoMapper;
using Repository;

namespace Mapping
{
    public class MappingNote<T> : Profile
    {
        public MappingNote()
        {
            CreateMap<string, bool>().ConvertUsing(str => str == "true");
            CreateMap<bool, string>().ConvertUsing(bol => bol == false? "false": "true");
            CreateMap<Note, T>();
            CreateMap<T, Note>();
        }
    }
}
