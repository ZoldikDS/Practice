using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbModels
{
    public interface ITarget<T>
    {
        public Task<List<T>> GetTargets(string login, T obj);
        public Task<int> ChangeStatus(T obj);
        public Task<int> CheckTimeStatus();
        public Task<int> AddTarget(string login, T obj);
        public Task<int> EditTarget(T obj);
        public Task<int> DeleteTarget(T obj);

    }
}
