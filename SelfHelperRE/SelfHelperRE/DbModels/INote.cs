using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbModels
{
    public interface INote<T>
    {
        public Task<List<T>> GetCategories(string login);
        public Task<List<T>> GetNotes(T obj, string login);
        public Task AddNote(T obj, string login);
        public Task EditNote(T obj);
        public Task DeleteNote(T obj);

        public bool CheckAddNote(T obj, string login);
        public bool CheckNote(T obj);
    }
}
