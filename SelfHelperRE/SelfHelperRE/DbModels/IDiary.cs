using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbModels
{
    public interface IDiary <T>
    {
        public Task<List<T>> GetDates(string login);
        public Task<List<T>> GetEntries(T obj, string login);
        public Task AddEntry(T obj, string login);
        public Task EditEntry(T obj);
        public Task DeleteEntry(T obj);

        public bool CheckEntry(T obj);
        public bool CheckAddEntry(T obj, string login);
    }
}
