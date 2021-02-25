using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbModels
{
    public interface INote<T>
    {
        public Task<List<T>> GetCategories(string login);
        public Task<List<T>> GetNotes(T obj, string login);
        public Task<int> AddNote(T obj, string login);
        public Task<int> EditNote(T obj);
        public Task<int> DeleteNote(T obj);
    }
}
