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

        public async Task AddNote(Note obj, string login)
        {
            DateTime dateTime = DateTime.Now;

            User user = context.users.FirstOrDefault(e => e.Login == login);

            if (user != null)
            {
                obj.Id = context.notes.Count();

                obj.User = user;

                context.notes.Add(obj);
            };
        }

        public async Task DeleteNote(Note obj)
        {
            Note note = context.notes.FirstOrDefault(e => e.Id == obj.Id);

            if (note != null)
            {
                context.notes.Remove(note);
            }
        }

        public async Task EditNote(Note obj)
        {
            Note note = context.notes.FirstOrDefault(e => e.Id == obj.Id);

            if (note != null)
            {
                note.Text = obj.Text;
                note.Topic = obj.Topic;
                note.Title = obj.Title;
                note.Important = obj.Important;
            }
        }

        public bool CheckAddNote(Note obj, string login)
        {
            User user = context.users.FirstOrDefault(e => e.Login == login);

            Note note = context.notes.FirstOrDefault(e => e.Text == obj.Text && e.Title == obj.Title && e.Topic == obj.Topic && e.Important == obj.Important && e.User == user);

            return note != null;
        }

        public bool CheckNote(Note obj)
        {
            Note note = context.notes.FirstOrDefault(e => e.Id == obj.Id);

            return note != null;
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
