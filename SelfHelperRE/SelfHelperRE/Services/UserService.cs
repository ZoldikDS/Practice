using DbModels;
using Mapping;
using Repository;
using System.Threading.Tasks;

namespace Services
{
    public class UserService<T>
    {
        IUser<User> service;
        MappingForUser<T> mapping;

        public UserService(IUser<User> service)
        {
            mapping = new MappingForUser<T>();
            this.service = service;

        }

        public async Task AddUser(T obj)
        {
            User user = mapping.UserMapping(obj);

            await service.AddUser(user);
        }

        public async Task<bool> CheckUser(T obj, string param)
        {
            User user = mapping.UserMapping(obj);

            return await service.CheckUser(user, param);
        }

        public async Task<T> GetData(T obj)
        {
            User user = mapping.UserMapping(obj);

            user = await service.GetData(user);

            return mapping.BackUserMapping(user);
        }

    }
}
