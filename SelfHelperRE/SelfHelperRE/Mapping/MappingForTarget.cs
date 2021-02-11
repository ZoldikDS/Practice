using AutoMapper;
using Repository;

namespace Mapping
{
    public class MappingForTarget<T>
    {
        public Target TargetMapping(T obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, Target>());
            var mapper = new Mapper(config);

            Target target = mapper.Map<T, Target>(obj);

            return target;
        }

        public T BackTargetMapping(Target obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Target, T>());
            var mapper = new Mapper(config);

            T t = mapper.Map<Target, T>(obj);

            return t;
        }
    }
}
