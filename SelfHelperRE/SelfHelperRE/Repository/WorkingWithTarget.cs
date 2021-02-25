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
        public async Task<int> AddTarget(string login, Target obj)
        {
            User user = db.Users.FirstOrDefault(e => e.Login == login);

            if (user != null)
            {
                try
                {
                    db.Targets.Add(new Target { Text = obj.Text, Status = "Performed", DateTimeFirst = obj.DateTimeFirst, DateTimeSecond = obj.DateTimeSecond, User = user });

                    await db.SaveChangesAsync();

                    return 1;
                }
                catch (Exception)
                {
                    return -1;
                }

            }

            return -1;
        }

        public async Task<int> ChangeStatus(Target obj)
        {
            Target target = db.Targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                try
                {
                    target.Status = obj.Status;

                    await db.SaveChangesAsync();

                    return 1;
                }
                catch (Exception)
                {
                    return -1;
                }

            }

            return -1;
        }

        public async Task<int> CheckTimeStatus()
        {
            try
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

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public async Task<int> DeleteTarget(Target obj)
        {
            Target target = db.Targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                try
                {
                    db.Targets.Remove(target);

                    await db.SaveChangesAsync();

                    return 1;
                }
                catch (Exception)
                {
                    return -1;
                }

            }

            return -1;
        }

        public async Task<int> EditTarget(Target obj)
        {
            Target target = db.Targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                try
                {
                    target.Text = obj.Text;
                    target.DateTimeFirst = obj.DateTimeFirst;
                    target.DateTimeSecond = obj.DateTimeSecond;

                    await db.SaveChangesAsync();

                    return 1;
                }
                catch (Exception)
                {
                    return -1;
                }

            }

            return -1;
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
