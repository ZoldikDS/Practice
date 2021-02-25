using AutoMapper;
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
        IMapper targetMapper;

        public TargetService(ITarget<Target> service, IMapper targetMapper)
        {
            this.targetMapper = targetMapper;
            this.service = service;

        }

        public async Task<List<T>> LoadTargets(string login, T obj)
        {
            Target target = targetMapper.Map<Target>(obj);

            List<Target> targets = await service.GetTargets(login, target);

            List<T> result = new List<T>();

            foreach (var item in targets)
            {
                result.Add(targetMapper.Map<T>(item));
            }

            return result;
        }

        public async Task<int> ChangeStatus(T obj)
        {
            Target target = targetMapper.Map<Target>(obj);

            return await service.ChangeStatus(target);
        }

        public async Task<int> CheckTimeStatus()
        {
            return await service.CheckTimeStatus();
        }

        public async Task<int> AddTarget(string login, T obj)
        {
            Target target = targetMapper.Map<Target>(obj);

            return await service.AddTarget(login, target);
        }

        public async Task<int> EditTarget(T obj)
        {
            Target target = targetMapper.Map<Target>(obj);

            return await service.EditTarget(target);
        }

        public async Task<int> DeleteTarget(T obj)
        {
            Target target = targetMapper.Map<Target>(obj);

            return await service.DeleteTarget(target);
        }

    }
}
