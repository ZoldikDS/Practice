using DbModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryForTests
{
    public class WorkingWithNoteForTests : INote<Note>
    {
        ApplicationContextForTests context;
        public WorkingWithNoteForTests()
        {
            context = new ApplicationContextForTests();
        }

        public async Task<int> AddNote(Note obj, string login)
        {
            DateTime dateTime = DateTime.Now;

            User user = context.users.FirstOrDefault(e => e.Login == login);

            if (user != null)
            {
                try
                {
                    obj.Id = context.notes.Count();

                    obj.User = user;

                    context.notes.Add(obj);

                    return 1;
                }
                catch (Exception)
                {
                    return -1;
                }

            }

            return -1;
        }

        public async Task<int> DeleteNote(Note obj)
        {
            Note note = context.notes.FirstOrDefault(e => e.Id == obj.Id);

            if (note != null)
            {
                try
                {
                    context.notes.Remove(note);

                    return 1;
                }
                catch (Exception)
                {
                    return -1;
                }
            }

            return -1;
        }

        public async Task<int> EditNote(Note obj)
        {
            Note note = context.notes.FirstOrDefault(e => e.Id == obj.Id);

            if (note != null)
            {
                try
                {
                    note.Text = obj.Text;
                    note.Topic = obj.Topic;
                    note.Title = obj.Title;
                    note.Important = obj.Important;

                    return 1;
                }
                catch (Exception)
                {
                    return -1;
                }

            }

            return -1;
        }

        public async Task<List<Note>> GetCategories(string login)
        {
            List<Note> result = context.notes.
                        Where(e => e.User.Login == login).Select(e => new Note { Topic = e.Topic }).ToList();

            return result;
        }

        public async Task<List<Note>> GetNotes(Note obj, string login)
        {
            List<Note> result;
            if (obj.DateTime == DateTime.MinValue)
            {
                if (obj.Topic == "Все")
                {
                    result = context.notes
                    .Where(e => e.User.Login == login)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToList();
                }
                else if (obj.Topic == "Важные")
                {
                    result = context.notes
                    .Where(e => e.User.Login == login && e.Important == true)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToList();
                }
                else
                {
                    result = context.notes
                    .Where(e => e.User.Login == login && e.Topic == obj.Topic)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToList();
                }
            }
            else
            {
                if (obj.Topic == "Все")
                {
                    result = result = context.notes
                    .Where(e => e.User.Login == login && e.DateTime.Date == obj.DateTime.Date)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToList();
                }
                else if (obj.Topic == "Важные")
                {
                    result = context.notes
                    .Where(e => e.User.Login == login && e.Important == true && e.DateTime.Date == obj.DateTime.Date)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToList();
                }
                else
                {
                    result = context.notes
                    .Where(e => e.User.Login == login && e.Topic == obj.Topic && e.DateTime.Date == obj.DateTime.Date)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToList();
                }
            }

            return result;
        }
    }
}
