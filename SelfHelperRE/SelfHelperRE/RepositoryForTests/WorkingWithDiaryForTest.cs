using DbModels;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryForTests
{
    public class WorkingWithDiaryForTest : IDiary<Diary>
    {
        ApplicationContextForTests context;
        public WorkingWithDiaryForTest()
        {
            context = new ApplicationContextForTests();
        }

        public async Task<int> AddEntry(Diary obj, string login)
        {
            User user = context.users.FirstOrDefault(u => u.Login == login);

            if (user != null)
            {
                try
                {
                    obj.Id = context.diaries.Count();

                    obj.User = user;

                    context.diaries.Add(obj);

                    return 1;
                }
                catch (System.Exception)
                {
                    return -1;
                }
            }

            return -1;
        }

        public async Task<int> DeleteEntry(Diary obj)
        {
            Diary diary = context.diaries.FirstOrDefault(u => u.Id == obj.Id);

            if (diary != null)
            {
                try
                {
                    context.diaries.RemoveAt(obj.Id);
                }
                catch (System.Exception)
                {

                    return -1;
                }

                return 1;
            }

            return -1;
        }

        public async Task<int> EditEntry(Diary obj)
        {
            Diary diary = context.diaries.FirstOrDefault(e => e.Id == obj.Id);

            if (diary != null)
            {
                try
                {
                    diary.Text = obj.Text;
                }
                catch (System.Exception)
                {
                    return -1;
                }
            }

            return 1;
        }

        public async Task<List<Diary>> GetDates(string login)
        {
            var result = context.diaries.OrderBy(e => e.DateTime).
                Where(e => e.User.Login == login).Select(e => new Diary { DateTime = e.DateTime }).ToList();

            return result;
        }

        public async Task<List<Diary>> GetEntries(Diary obj, string login)
        {
            var result = context.diaries
                .Where(e => (e.DateTime.Date == obj.DateTime.Date && e.User.Login == login))
                .Select(e => new Diary { Text = e.Text, DateTime = e.DateTime, Id = e.Id }).ToList();

            return result;
        }
    }
}
