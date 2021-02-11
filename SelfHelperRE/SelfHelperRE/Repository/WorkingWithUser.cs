using DbModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repository
{
    public class WorkingWithUser : IUser<User>
    {
        ApplicationContext db;
        public WorkingWithUser()
        {
            db = new ApplicationContext();
        }
        public async Task AddUser(User user)
        {
            db.Users.Add(new User { Login = user.Login, Email = user.Email, Password = user.Password });

            await db.SaveChangesAsync();
        }

        public async Task<bool> CheckUser(User user, string param)
        {
            User check = new User();

            switch (param)
            {
                case "login":
                    check = await db.Users.FirstOrDefaultAsync(u => u.Login == user.Login && u.Password == user.Password);
                    break;
                case "registration":
                    check = await db.Users.FirstOrDefaultAsync(u => u.Login == user.Login || u.Email == user.Email);
                    break;
                case "email":
                    check = await db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                    break;
            }

            return check != null;
        }

        public async Task<User> GetData(User user)
        {
            User result = await db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            return result;
        }
    }
}
