using DbModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryForTests
{
    public class WorkingWithTargetForTest : ITarget<Target>
    {
        ApplicationContextForTests context;
        public WorkingWithTargetForTest()
        {
            context = new ApplicationContextForTests();
        }

        public async Task<int> AddTarget(string login, Target obj)
        {
            User user = context.users.FirstOrDefault(e => e.Login == login);

            if (user != null)
            {
                try
                {
                    obj.Id = context.targets.Count();

                    obj.User = user;

                    context.targets.Add(obj);

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
            Target target = context.targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                try
                {
                    target.Status = obj.Status;

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
                IEnumerable<Target> targets = context.targets.Where(e => e.Status == "Performed").ToList();

                foreach (var item in targets)
                {
                    if (item.DateTimeSecond < DateTime.Now)
                    {
                        item.Status = "Failed";
                    }
                }

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public async Task<int> DeleteTarget(Target obj)
        {
            Target target = context.targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                try
                {
                    context.targets.Remove(target);

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
            Target target = context.targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                try
                {
                    target.Text = obj.Text;
                    target.DateTimeFirst = obj.DateTimeFirst;
                    target.DateTimeSecond = obj.DateTimeSecond;

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
                result = context.targets
                 .Where(e => (e.User.Login == login))
                 .Select(e => new Target { Id = e.Id, Text = e.Text, Status = e.Status, DateTimeFirst = e.DateTimeFirst, DateTimeSecond = e.DateTimeSecond }).ToList();
            }
            else
            {
                result = context.targets
                .Where(e => (e.User.Login == login && e.Status == obj.Status))
                .Select(e => new Target { Id = e.Id, Text = e.Text, Status = e.Status, DateTimeFirst = e.DateTimeFirst, DateTimeSecond = e.DateTimeSecond }).ToList();
            }

            return result;
        }
    }
}
