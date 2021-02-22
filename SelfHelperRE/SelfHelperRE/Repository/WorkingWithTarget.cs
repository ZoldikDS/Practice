using DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class WorkingWithTarget : ITarget<Target>
    {
        ApplicationContext db;
        public WorkingWithTarget()
        {
            db = new ApplicationContext();
        }
        public async Task AddTarget(string login, Target obj)
        {
            User user = db.Users.FirstOrDefault(e => e.Login == login);

            if (user != null)
            {
                db.Targets.Add(new Target { Text = obj.Text, Status = "Performed", DateTimeFirst = obj.DateTimeFirst, DateTimeSecond = obj.DateTimeSecond, User = user });

                await db.SaveChangesAsync();
            }
        }

        public async Task ChangeStatus(Target obj)
        {
            Target target = db.Targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                target.Status = obj.Status;

                await db.SaveChangesAsync();
            }
        }

        public bool CheckAddTarget(Target obj, string login)
        {
            throw new NotImplementedException();
        }

        public bool CheckPerformedStatus()
        {
            throw new NotImplementedException();
        }

        public bool CheckStatus(Target obj)
        {
            throw new NotImplementedException();
        }

        public bool CheckTarget(Target obj)
        {
            throw new NotImplementedException();
        }

        public async Task CheckTimeStatus()
        {
            IEnumerable<Target> targets = db.Targets.Where(e => e.Status == "Performed").ToList();

            foreach (var item in targets)
            {
                if (item.DateTimeSecond < DateTime.Now)
                {
                    item.Status = "Failed";
                }
            }

            await db.SaveChangesAsync();
        }

        public async Task DeleteTarget(Target obj)
        {
            Target target = db.Targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                db.Targets.Remove(target);

                await db.SaveChangesAsync();
            }
        }

        public async Task EditTarget(Target obj)
        {
            Target target = db.Targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                target.Text = obj.Text;
                target.DateTimeFirst = obj.DateTimeFirst;
                target.DateTimeSecond = obj.DateTimeSecond;

                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Target>> GetTargets(string login, Target obj)
        {
            List<Target> result;

            if (obj.Status == "Всё")
            {
                result = await db.Targets.AsNoTracking()
				 .Where(e => (e.User.Login == login))
				 .Select(e => new Target { Id = e.Id, Text = e.Text, Status = e.Status, DateTimeFirst = e.DateTimeFirst, DateTimeSecond = e.DateTimeSecond }).ToListAsync();
            }
            else
            {
                result = await db.Targets.AsNoTracking()
                .Where(e => (e.User.Login == login && e.Status == obj.Status))
                .Select(e => new Target { Id = e.Id, Text = e.Text, Status = e.Status, DateTimeFirst = e.DateTimeFirst, DateTimeSecond = e.DateTimeSecond }).ToListAsync();
            }

            return result;
        }
    }
}
