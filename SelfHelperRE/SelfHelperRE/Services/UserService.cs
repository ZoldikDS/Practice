using System.Threading.Tasks;
using Mapping;
using Repository;
using RepositoryForTests;

namespace Services
{
    public class UserService <T>
    {
        WorkingWithUser workingWithUser;
        WorkingWithUserForTest workingWithUserForTest;
        MappingForUser<T> mapping;

        bool TestMod;

        public UserService(bool TestMod = false)
        {
            workingWithUser = new WorkingWithUser();
            mapping = new MappingForUser<T>();
            workingWithUserForTest = new WorkingWithUserForTest();

            this.TestMod = TestMod;
        }

        public async Task AddUser(T obj)
        {
            User user = mapping.UserMapping(obj);

            if (TestMod)
            {
                await workingWithUserForTest.AddUser(user);
            }
            else
            {
                await workingWithUser.AddUser(user);
            }
        }

        public async Task<bool> CheckUser(T obj, string param)
        {
            User user = mapping.UserMapping(obj);

            if (TestMod)
            {
                return await workingWithUserForTest.CheckUser(user, param);
            }
            else
            {
                return await workingWithUser.CheckUser(user, param);
            }
        }

        public async Task<T> GetData(T obj)
        {
            User user = mapping.UserMapping(obj);

            if (TestMod)
            {
                user = await workingWithUserForTest.GetData(user);
            }
            else
            {
                user = await workingWithUser.GetData(user);
            }

            return mapping.BackUserMapping(user);
        }

    }
}
