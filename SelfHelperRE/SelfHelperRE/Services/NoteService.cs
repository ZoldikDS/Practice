using DbModels;
using Mapping;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class NoteService <T>
    {
        INote<Note> service;
        MappingForNote<T> mapping;

        bool TestMod;

        public NoteService(INote<Note> service)
        {
            mapping = new MappingForNote<T>();
            this.service = service;
        }

        public async Task<List<T>> LoadCategories(string login)
        {
            List<Note> notes = await service.GetCategories(login);

            List<T> result = new List<T>();

            foreach (var item in notes)
            {
                result.Add(mapping.BackNoteMapping(item));
            }

            return result;
        }

        public async Task<List<T>> LoadNotes(T obj, string login)
        {
            Note note = mapping.NoteMapping(obj);

            List<Note> notes = await service.GetNotes(note, login);

            List<T> result = new List<T>();

            foreach (var item in notes)
            {
                result.Add(mapping.BackNoteMapping(item));
            }

            return result;
        }

        public async Task AddNote(T obj, string login)
        {
            Note note = mapping.NoteMapping(obj);

            await service.AddNote(note, login);

        }

        public async Task EditNote(T obj)
        {
            Note note = mapping.NoteMapping(obj);

            await service.EditNote(note);

        }

        public async Task DeleteNote(T obj)
        {
            Note note = mapping.NoteMapping(obj);

            await service.DeleteNote(note);

        }

        public bool CheckAddNoteForTest(T obj, string login)
        {
            Note note = mapping.NoteMapping(obj);

            return service.CheckAddNote(note, login);
        }    
        
        public bool CheckNoteForTest(T obj)
        {
            Note note = mapping.NoteMapping(obj);

            return service.CheckNote(note);
        }
    }
}
