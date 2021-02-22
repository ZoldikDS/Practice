using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbModels
{
    public interface ITarget<T>
    {
        public Task<List<T>> GetTargets(string login, T obj);
        public Task ChangeStatus(T obj);
        public Task CheckTimeStatus();
        public Task AddTarget(string login, T obj);
        public Task EditTarget(T obj);
        public Task DeleteTarget(T obj);

        public bool CheckTarget(T obj);
        public bool CheckAddTarget(T obj, string login);
        public bool CheckPerformedStatus();
        public bool CheckStatus(T obj);

    }
}
