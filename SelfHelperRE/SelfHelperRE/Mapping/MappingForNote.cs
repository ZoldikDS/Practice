using AutoMapper;
using Repository;

namespace Mapping
{
    public class MappingForNote<T>
    {
        public Note NoteMapping(T obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, Note>());
            var mapper = new Mapper(config);

            Note note = mapper.Map<T, Note>(obj);

            return note;
        }

        public T BackNoteMapping(Note obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Note, T>());
            var mapper = new Mapper(config);

            T t = mapper.Map<Note, T>(obj);

            return t;
        }
    }
}
