using Mapping;
using Repository;
using RepositoryForTests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class TargetService<T>
    {
        WorkingWithTarget workingWithTarget;
        MappingForTarget<T> mapping;
        WorkingWithTargetForTest workingWithTargetForTest;

        bool TestMod;

        public TargetService(bool TestMod = false)
        {
            workingWithTarget = new WorkingWithTarget();
            mapping = new MappingForTarget<T>();
            workingWithTargetForTest = new WorkingWithTargetForTest();

            this.TestMod = TestMod;
        }

        public async Task<List<T>> LoadTargets(string login, T obj)
        {
            Target target = mapping.TargetMapping(obj);
            List<Target> targets;

            if (TestMod)
            {
                targets = await workingWithTargetForTest.GetTargets(login, target);
            }
            else
            {
                targets = await workingWithTarget.GetTargets(login, target);
            }

            List<T> result = new List<T>();

            foreach (var item in targets)
            {
                result.Add(mapping.BackTargetMapping(item));
            }

            return result;
        }
        public async Task ChangeStatus(T obj)
        {
            Target target = mapping.TargetMapping(obj);

            if (TestMod)
            {
                await workingWithTargetForTest.ChangeStatus(target);
            }
            else
            {
                await workingWithTarget.ChangeStatus(target);
            }

        }
        public async Task CheckTimeStatus()
        {
            if (TestMod)
            {
                await workingWithTargetForTest.CheckTimeStatus();
            }
            else
            {
                await workingWithTarget.CheckTimeStatus();
            }

        }
        public async Task AddTarget(string login, T obj)
        {
            Target target = mapping.TargetMapping(obj);

            if (TestMod)
            {
                await workingWithTargetForTest.AddTarget(login, target);
            }
            else
            {
                await workingWithTarget.AddTarget(login, target);
            }

        }
        public async Task EditTarget(T obj)
        {
            Target target = mapping.TargetMapping(obj);

            if (TestMod)
            {
                await workingWithTargetForTest.EditTarget(target);
            }
            else
            {
                await workingWithTarget.EditTarget(target);
            }
;
        }
        public async Task DeleteTarget(T obj)
        {
            Target target = mapping.TargetMapping(obj);

            if (TestMod)
            {
                await workingWithTargetForTest.DeleteTarget(target);
            }
            else
            {
                await workingWithTarget.DeleteTarget(target);
            }

        }

        public bool CheckTarget(T obj)
        {
            Target target = mapping.TargetMapping(obj);

            if (TestMod)
            {
                return workingWithTargetForTest.CheckTarget(target);
            }

            return false;
        }

        public bool CheckAddTarget(T obj, string login)
        {
            Target target = mapping.TargetMapping(obj);

            if (TestMod)
            {
                return workingWithTargetForTest.CheckAddTarget(login, target);
            }

            return false;
        }

        public bool CheckPerformedStatus()
        {

            if (TestMod)
            {
                return workingWithTargetForTest.CheckPerformedStatus();
            }

            return false;
        }

        public bool CheckStatus(T obj)
        {
            Target target = mapping.TargetMapping(obj);

            if (TestMod)
            {
                return workingWithTargetForTest.CheckStatus(target);
            }

            return false;
        }

    }
}
