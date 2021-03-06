﻿using DbModels;
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

        public async Task<int> AddNote(Note obj, string login)
        {
            DateTime dateTime = DateTime.Now;

            User user = db.Users.FirstOrDefault(e => e.Login == login);

            if (user != null)
            {
                try
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
            Note note = db.Notes.FirstOrDefault(e => e.Id == obj.Id);

            if (note != null)
            {
                try
                {
                    db.Notes.Remove(note);

                    await db.SaveChangesAsync();

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
            Note note = db.Notes.FirstOrDefault(e => e.Id == obj.Id);

            if (note != null)
            {
                try
                {
                    note.Text = obj.Text;
                    note.Topic = obj.Topic;
                    note.Title = obj.Title;
                    note.Important = obj.Important;

                    await db.SaveChangesAsync();

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
