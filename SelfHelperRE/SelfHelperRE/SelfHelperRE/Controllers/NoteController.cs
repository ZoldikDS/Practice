using DbModels;
using Microsoft.AspNetCore.Mvc;
using Repository;
using SelfHelper.Comparers;
using SelfHelperRE.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfHelperRE.Controllers
{
    public class NoteController : Controller
    {

        NoteService<NoteData> noteDataService;

        public NoteController(INote<Note> service)
        {
            this.noteDataService = new NoteService<NoteData>(service);
        }

        [HttpGet]
        public async Task<IEnumerable<NoteData>> LoadCategories()
        {
            IEnumerable<NoteData> result = await noteDataService.LoadCategories(User.Identity.Name);

            return result.Distinct(new NoteTopicComparer());
        }

        [HttpPost]
        public async Task<List<NoteData>> LoadNotes([FromBody] NoteCatch noteCatch)
        {
            List<NoteData> result;

            NoteData data = new NoteData();
            if (noteCatch.Date != "Все")
            {
                data.DateTime = Convert.ToDateTime(noteCatch.Date);
            }
            data.Topic = noteCatch.Category;

            result = await noteDataService.LoadNotes(data, User.Identity.Name);

            return result;
        }

        [HttpPost]
        public async Task AddNote([FromBody] NoteCatch noteCatch)
        {
            if (noteCatch.Text != null && noteCatch.Text != "" && noteCatch.Category != null && noteCatch.Category != "" && noteCatch.Title != null && noteCatch.Title != "" && noteCatch.Important != null && noteCatch.Important != "")
            {
                NoteData data = new NoteData();
                data.Topic = noteCatch.Category;
                data.Text = noteCatch.Text;
                data.Title = noteCatch.Title;
                if (noteCatch.Important == "true")
                {
                    data.Important = true;
                }
                else
                {
                    data.Important = false;
                }

                await noteDataService.AddNote(data, User.Identity.Name);
            }
        }

        [HttpPost]
        public async Task EditNote([FromBody] NoteCatch noteCatch)
        {
            if (noteCatch.Text != null && noteCatch.Text != "" && noteCatch.Category != null && noteCatch.Category != "" && noteCatch.Title != null && noteCatch.Title != "" && noteCatch.Important != null && noteCatch.Important != "")
            {
                NoteData data = new NoteData();
                data.Topic = noteCatch.Category;
                data.Text = noteCatch.Text;
                data.Title = noteCatch.Title;
                data.Id = Convert.ToInt32(noteCatch.Id);
                if (noteCatch.Important == "true")
                {
                    data.Important = true;
                }
                else
                {
                    data.Important = false;
                }

                await noteDataService.EditNote(data);
            }
        }

        [HttpPost]
        public async Task DeleteNote([FromBody] NoteCatch noteCatch)
        {
            NoteData data = new NoteData();

            data.Id = Convert.ToInt32(noteCatch.Id);

            await noteDataService.DeleteNote(data);
        }
    }
}
