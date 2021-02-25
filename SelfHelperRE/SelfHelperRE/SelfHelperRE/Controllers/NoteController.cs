using AutoMapper;
using DbModels;
using Microsoft.AspNetCore.Mvc;
using Repository;
using SelfHelper.Comparers;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfHelperRE.Controllers
{
    public class NoteController : Controller
    {

        NoteService<NoteCatch> noteService;

        public NoteController(INote<Note> service, IMapper noteMapper)
        {
            this.noteService = new NoteService<NoteCatch>(service, noteMapper);
        }

        [HttpGet]
        public async Task<IEnumerable<NoteCatch>> LoadCategories()
        {
            IEnumerable<NoteCatch> result = await noteService.LoadCategories(User.Identity.Name);

            return result.Distinct(new NoteTopicComparer());
        }

        [HttpPost]
        public async Task<List<NoteCatch>> LoadNotes([FromBody] NoteCatch noteCatch)
        {
            return await noteService.LoadNotes(noteCatch, User.Identity.Name);
        }

        [HttpPost]
        public async Task AddNote([FromBody] NoteCatch noteCatch)
        {
            if (noteCatch.Text != null && noteCatch.Text != "" && noteCatch.Topic != null && noteCatch.Topic != "" && noteCatch.Title != null && noteCatch.Title != "" && noteCatch.Important != null && noteCatch.Important != "")
            {
                await noteService.AddNote(noteCatch, User.Identity.Name);
            }
        }

        [HttpPost]
        public async Task EditNote([FromBody] NoteCatch noteCatch)
        {
            if (noteCatch.Text != null && noteCatch.Text != "" && noteCatch.Topic != null && noteCatch.Topic != "" && noteCatch.Title != null && noteCatch.Title != "" && noteCatch.Important != null && noteCatch.Important != "")
            {
                await noteService.EditNote(noteCatch);
            }
        }

        [HttpPost]
        public async Task DeleteNote([FromBody] NoteCatch noteCatch)
        {
            await noteService.DeleteNote(noteCatch);
        }
    }
}
