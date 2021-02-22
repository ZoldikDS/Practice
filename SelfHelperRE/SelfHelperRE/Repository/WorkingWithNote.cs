using DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class WorkingWithNote : INote<Note>
    {
        ApplicationContext db;
        public WorkingWithNote()
        {
            db = new ApplicationContext();
        }

        public async Task AddNote(Note obj, string login)
        {
            DateTime dateTime = DateTime.Now;

            User user = db.Users.FirstOrDefault(e => e.Login == login);

            if (user != null)
            {
                db.Notes.Add(new Note
                {
                    Text = obj.Text,
                    DateTime = dateTime,
                    User = user,
                    Title = obj.Title,
                    Topic = obj.Topic,
                    Important = obj.Important
                });

                await db.SaveChangesAsync();
            };
        }

        public bool CheckAddNote(Note obj, string login)
        {
            throw new NotImplementedException();
        }

        public bool CheckNote(Note obj)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteNote(Note obj)
        {
            Note note = db.Notes.FirstOrDefault(e => e.Id == obj.Id);

            if (note != null)
            {
                db.Notes.Remove(note);

                await db.SaveChangesAsync();
            }
        }

        public async Task EditNote(Note obj)
        {
            Note note = db.Notes.FirstOrDefault(e => e.Id == obj.Id);

            if (note != null)
            {
                note.Text = obj.Text;
                note.Topic = obj.Topic;
                note.Title = obj.Title;
                note.Important = obj.Important;

                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Note>> GetCategories(string login)
        {
            List<Note> result = await db.Notes.AsNoTracking().
                Where(e => e.User.Login == login).Select(e => new Note { Topic = e.Topic }).ToListAsync();

            return result;
        }

        public async Task<List<Note>> GetNotes(Note obj, string login)
        {
            List<Note> result;
            if (obj.DateTime == DateTime.MinValue)
            {
                if (obj.Topic == "Все")
                {
                    result = await db.Notes.AsNoTracking()
                    .Where(e => e.User.Login == login)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToListAsync();
                }
                else if (obj.Topic == "Важные")
                {
                    result = await db.Notes.AsNoTracking()
                    .Where(e => e.User.Login == login && e.Important == true)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToListAsync();
                }
                else
                {
                    result = await db.Notes.AsNoTracking()
                    .Where(e => e.User.Login == login && e.Topic == obj.Topic)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToListAsync();
                }
            }
            else
            {
                if (obj.Topic == "Все")
                {
                    result = result = await db.Notes.AsNoTracking()
                    .Where(e => e.User.Login == login && e.DateTime.Date == obj.DateTime.Date)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToListAsync();
                }
                else if (obj.Topic == "Важные")
                {
                    result = await db.Notes.AsNoTracking()
                    .Where(e => e.User.Login == login && e.Important == true && e.DateTime.Date == obj.DateTime.Date)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToListAsync();
                }
                else
                {
                    result = await db.Notes.AsNoTracking()
                    .Where(e => e.User.Login == login && e.Topic == obj.Topic && e.DateTime.Date == obj.DateTime.Date)
                    .Select(e => new Note { Text = e.Text, Title = e.Title, Id = e.Id, Topic = e.Topic, Important = e.Important }).ToListAsync();
                }
            }

            return result;
        }
    }
}
