using Mapping;
using Repository;
using RepositoryForTests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class NoteService <T>
    {
        WorkingWithNote workingWithNote;
        MappingForNote<T> mapping;
        WorkingWithNoteForTests workingWithNoteForTests;

        bool TestMod;

        public NoteService(bool TestMod = false)
        {
            workingWithNote = new WorkingWithNote();
            mapping = new MappingForNote<T>();
            workingWithNoteForTests = new WorkingWithNoteForTests();

            this.TestMod = TestMod;
        }

        public async Task<List<T>> LoadCategories(string login)
        {
            List<Note> notes;

            if (TestMod)
            {
                notes = await workingWithNoteForTests.GetCategories(login);
            }
            else
            {
                notes = await workingWithNote.GetCategories(login);
            }

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

            List<Note> notes;

            if (TestMod)
            {
                notes = await workingWithNoteForTests.GetNotes(note, login);
            }
            else
            {
                notes = await workingWithNote.GetNotes(note, login);
            }

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

            if (TestMod)
            {
                await workingWithNoteForTests.AddNote(note, login);
            }
            else
            {
                await workingWithNote.AddNote(note, login);
            }

        }

        public async Task EditNote(T obj)
        {
            Note note = mapping.NoteMapping(obj);

            if (TestMod)
            {
                await workingWithNoteForTests.EditNote(note);
            }
            else
            {
                await workingWithNote.EditNote(note);
            }

        }

        public async Task DeleteNote(T obj)
        {
            Note note = mapping.NoteMapping(obj);

            if (TestMod)
            {
                await workingWithNoteForTests.DeleteNote(note);
            }
            else
            {
                await workingWithNote.DeleteNote(note);
            }

        }

        public bool CheckAddNoteForTest(T obj, string login)
        {
            Note note = mapping.NoteMapping(obj);

            if (TestMod)
            {
                return workingWithNoteForTests.CheckAddNote(note, login);
            }

            return false;
        }    
        
        public bool CheckNoteForTest(T obj)
        {
            Note note = mapping.NoteMapping(obj);

            if (TestMod)
            {
                return workingWithNoteForTests.CheckNote(note);
            }

            return false;
        }
    }
}
