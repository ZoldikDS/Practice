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

        public async Task AddTarget(string login, Target obj)
        {
            User user = context.users.FirstOrDefault(e => e.Login == login);

            if (user != null)
            {
                obj.Id = context.targets.Count();

                obj.User = user;

                context.targets.Add(obj);
            }
        }

        public bool CheckAddTarget(string login, Target obj)
        {
            User user = context.users.FirstOrDefault(e => e.Login == login);

            Target target = context.targets.FirstOrDefault(e => e.User == user && e.Text == obj.Text);

            return target != null;
        }

        public async Task ChangeStatus(Target obj)
        {
            Target target = context.targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                target.Status = obj.Status;
            }
        }

        public bool CheckStatus(Target obj)
        {
            Target target = context.targets.FirstOrDefault(e => e.Status == obj.Status && e.Id == obj.Id);

            return target != null;
        }

        public async Task CheckTimeStatus()
        {
            IEnumerable<Target> targets = context.targets.Where(e => e.Status == "Performed").ToList();

            foreach (var item in targets)
            {
                if (item.DateTimeSecond < DateTime.Now)
                {
                    item.Status = "Failed";
                }
            }
        }

        public bool CheckPerformedStatus()
        {
            Target target = context.targets.FirstOrDefault(e => e.Status == "Performed" && e.DateTimeSecond < DateTime.Now);

            return target != null;
        }

        public async Task DeleteTarget(Target obj)
        {
            Target target = context.targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                context.targets.Remove(target);

            }
        }

        public async Task EditTarget(Target obj)
        {
            Target target = context.targets.FirstOrDefault(e => e.Id == obj.Id);

            if (target != null)
            {
                target.Text = obj.Text;
                target.DateTimeFirst = obj.DateTimeFirst;
                target.DateTimeSecond = obj.DateTimeSecond;
            }
        }

        public bool CheckTarget(Target obj)
        {
            Target target = context.targets.FirstOrDefault(e => e.Id == obj.Id);

            return target != null;
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
