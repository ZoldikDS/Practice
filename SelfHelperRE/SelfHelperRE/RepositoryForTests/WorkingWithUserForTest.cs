using DbModels;
using System.Threading.Tasks;
using System.Linq;
using Repository;

namespace RepositoryForTests
{
    public class WorkingWithUserForTest : IUser<User>
    {

        ApplicationContextForTests context;

        public WorkingWithUserForTest()
        {
            context = new ApplicationContextForTests();
        }
        public async Task AddUser(User user)
        {
            user.Id = context.users.Count();

            context.users.Add(user);

        }

        public async Task<bool> CheckUser(User user, string param)
        {
            User check = null;

            switch (param)
            {
                case "login":
                    check = context.users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
                    break;
                case "registration":
                    check = context.users.FirstOrDefault(u => u.Login == user.Login || u.Email == user.Email);
                    break;
                case "email":
                    check = context.users.FirstOrDefault(u => u.Email == user.Email);
                    break;
            }

            return check != null;

        }

        public async Task<User> GetData(User user)
        {
            User result = context.users.FirstOrDefault(u => u.Email == user.Email);

            return result;
        }
    }
}
