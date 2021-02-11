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

        public async Task AddEntry(Diary obj, string login)
        {
            User user = context.users.FirstOrDefault(u => u.Login == login);

            if (user != null)
            {
                obj.Id = context.diaries.Count();

                obj.User = user;

                context.diaries.Add(obj);
            }
        }

        public async Task DeleteEntry(Diary obj)
        {
            Diary diary = context.diaries.FirstOrDefault(u => u.Id == obj.Id);

            if (diary != null)
            {
                context.diaries.RemoveAt(obj.Id);
            }
        }

        public async Task EditEntry(Diary obj)
        {
            Diary diary = context.diaries.FirstOrDefault(e => e.Id == obj.Id);

            if (diary != null)
            {
                diary.Text = obj.Text;
            }
        }

        public bool CheckAddEntry(Diary obj, string login)
        {
            User user = context.users.FirstOrDefault(e => e.Login == login);

            Diary diary = context.diaries.FirstOrDefault(e => e.Text == obj.Text && e.User == user);


            if (diary == null)
            {
                return false;
            }

            return true;
        }

        public bool CheckEntry(Diary obj)
        {
            Diary diary = context.diaries.FirstOrDefault(e => e.Id == obj.Id);

            if (diary == null)
            {
                return false;
            }

            return true;
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
