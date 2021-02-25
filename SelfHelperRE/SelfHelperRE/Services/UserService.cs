using AutoMapper;
using DbModels;
using Repository;
using System.Threading.Tasks;

namespace Services
{
    public class UserService<T>
    {
        IUser<User> service;
        IMapper mapper;

        public UserService(IUser<User> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task AddUser(T obj)
        {
            User user = mapper.Map<User>(obj);

            await service.AddUser(user);
        }

        public async Task<bool> CheckUser(T obj, string param)
        {
            User user = mapper.Map<User>(obj);

            return await service.CheckUser(user, param);
        }

        public async Task<T> GetData(T obj)
        {
            User user = mapper.Map<User>(obj);

            user = await service.GetData(user);

            return mapper.Map<T>(user);
        }
    }
}
