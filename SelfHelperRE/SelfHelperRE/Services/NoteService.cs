using AutoMapper;
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
        IMapper noteMapper;


        public NoteService(INote<Note> service, IMapper noteMapper)
        {
            this.noteMapper = noteMapper;
            this.service = service;
        }

        public async Task<List<T>> LoadCategories(string login)
        {
            List<Note> notes = await service.GetCategories(login);

            List<T> result = new List<T>();

            foreach (var item in notes)
            {
                result.Add(noteMapper.Map<T>(item));
            }

            return result;
        }

        public async Task<List<T>> LoadNotes(T obj, string login)
        {
            Note note = noteMapper.Map<Note>(obj);

            List<Note> notes = await service.GetNotes(note, login);

            List<T> result = new List<T>();

            foreach (var item in notes)
            {
                result.Add(noteMapper.Map<T>(item));
            }

            return result;
        }

        public async Task<int> AddNote(T obj, string login)
        {
            Note note = noteMapper.Map<Note>(obj);

            return await service.AddNote(note, login);
        }

        public async Task<int> EditNote(T obj)
        {
            Note note = noteMapper.Map<Note>(obj);

            return await service.EditNote(note);
        }

        public async Task<int> DeleteNote(T obj)
        {
            Note note = noteMapper.Map<Note>(obj);

            return await service.DeleteNote(note);
        }
    }
}
