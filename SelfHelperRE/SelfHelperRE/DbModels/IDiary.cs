using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbModels
{
    public interface IDiary <T>
    {
        public Task<List<T>> GetDates(string login);
        public Task<List<T>> GetEntries(T obj, string login);
        public Task<int> AddEntry(T obj, string login);
        public Task<int> EditEntry(T obj);
        public Task<int> DeleteEntry(T obj);
    }
}
