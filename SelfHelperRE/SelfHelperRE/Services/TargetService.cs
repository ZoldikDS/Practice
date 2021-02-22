using DbModels;
using Mapping;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class TargetService<T>
    {

        ITarget<Target> service;
        MappingForTarget<T> mapping;


        public TargetService(ITarget<Target> service)
        {
            mapping = new MappingForTarget<T>();
            this.service = service;

        }

        public async Task<List<T>> LoadTargets(string login, T obj)
        {
            Target target = mapping.TargetMapping(obj);

            List<Target> targets = await service.GetTargets(login, target);

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

            await service.ChangeStatus(target);
        }

        public async Task CheckTimeStatus()
        {
            await service.CheckTimeStatus();
        }

        public async Task AddTarget(string login, T obj)
        {
            Target target = mapping.TargetMapping(obj);

            await service.AddTarget(login, target);
        }

        public async Task EditTarget(T obj)
        {
            Target target = mapping.TargetMapping(obj);

            await service.EditTarget(target);
        }

        public async Task DeleteTarget(T obj)
        {
            Target target = mapping.TargetMapping(obj);

            await service.DeleteTarget(target);
        }

        public bool CheckTarget(T obj)
        {
            Target target = mapping.TargetMapping(obj);

            return service.CheckTarget(target);
        }

        public bool CheckAddTarget(T obj, string login)
        {
            Target target = mapping.TargetMapping(obj);

            return service.CheckAddTarget(target, login);
        }

        public bool CheckPerformedStatus()
        {
            return service.CheckPerformedStatus();
        }

        public bool CheckStatus(T obj)
        {
            Target target = mapping.TargetMapping(obj);

            return service.CheckStatus(target);
        }

    }
}
