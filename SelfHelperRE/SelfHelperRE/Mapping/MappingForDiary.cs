using AutoMapper;
using Repository;

namespace Mapping
{
    public class MappingForDiary<T>
    {
        public Diary DiaryMapping(T obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, Diary>());

            var mapper = new Mapper(config);

            Diary diary = mapper.Map<T, Diary>(obj);

            return diary;
        }

        public T BackDiaryMapping(Diary obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Diary, T>());

            var mapper = new Mapper(config);

            T t = mapper.Map<Diary, T>(obj);

            return t;
        }
    }
}
