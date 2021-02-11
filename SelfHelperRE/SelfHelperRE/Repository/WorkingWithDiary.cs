using DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class WorkingWithDiary : IDiary<Diary>
    {
        ApplicationContext db;

        public WorkingWithDiary()
        {
            db = new ApplicationContext();
        }

        public async Task AddEntry(Diary obj, string login)
        {
            DateTime dateTime = DateTime.Now;

            User user = db.Users.FirstOrDefault(e => e.Login == login);

            if (user != null)
            {
                db.Diaries.Add(new Diary { Text = obj.Text, DateTime = dateTime, User = user });

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteEntry(Diary obj)
        {
            Diary diary = await db.Diaries.FirstOrDefaultAsync(u => u.Id == obj.Id);

            if (diary != null)
            {
                db.Diaries.Remove(diary);

                await db.SaveChangesAsync();
            }
        }

        public async Task EditEntry(Diary obj)
        {
            Diary diary = db.Diaries.FirstOrDefault(e => e.Id == obj.Id);

            if (diary != null)
            {
                diary.Text = obj.Text;

                await db.SaveChangesAsync();
            }
        }
        public async Task<List<Diary>> GetDates(string login)
        {
            var result = await db.Diaries.AsNoTracking().OrderBy(e => e.DateTime).
                Where(e => e.User.Login == login).Select(e => new Diary { DateTime = e.DateTime }).ToListAsync();

            return result;
        }

        public async Task<List<Diary>> GetEntries(Diary obj, string login)
        {
            var result = await db.Diaries.AsNoTracking()
                .Where(e => (e.DateTime.Date == obj.DateTime.Date && e.User.Login == login))
                .Select(e => new Diary { Text = e.Text, DateTime = e.DateTime, Id = e.Id }).ToListAsync();

            return result;
        }
    }
}
