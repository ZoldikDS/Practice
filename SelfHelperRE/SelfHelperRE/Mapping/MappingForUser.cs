using AutoMapper;
using Repository;

namespace Mapping
{
    public class MappingForUser <T>
    {
        public User UserMapping(T obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, User>());
            var mapper = new Mapper(config);

            User user = mapper.Map<T, User>(obj);

            return user;
        }

        public T BackUserMapping(User obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, T>());
            var mapper = new Mapper(config);

            T t = mapper.Map<User, T>(obj);

            return t;
        }
    }
}
