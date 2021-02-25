using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Services;
using System.Threading.Tasks;
using System.Linq;
using DbModels;
using Repository;
using AutoMapper;

namespace SelfHelperRE.Controllers
{
    public class DiaryController : Controller
    {
        DiaryService<DiaryCatch> diaryService;

        public DiaryController(IDiary<Diary> service, IMapper diaryMapper)
        {
            this.diaryService = new DiaryService<DiaryCatch>(service, diaryMapper);
        }

        [HttpGet]
        public async Task<IEnumerable<DiaryCatch>> LoadDates()
        {
            IEnumerable<DiaryCatch> result = await diaryService.LoadDates(User.Identity.Name);
            return result.Distinct(new DiaryDateComparer());
        }

        [HttpPost]
        public async Task<List<DiaryCatch>> LoadEntries([FromBody] DiaryCatch info)
        {

            return await diaryService.LoadEntries(info, User.Identity.Name);
        }

        [HttpPost]
        public async Task AddEntry([FromBody] DiaryCatch info)
        {
            if (info.Text != null && info.Text != "")
            {
                await diaryService.AddEntry(info, User.Identity.Name);
            }
        }

        [HttpPost]
        public async Task EditEntry([FromBody] DiaryCatch info)
        {
            if (info.Text != null && info.Text != "")
            {
                await diaryService.EditEntry(info);
            }
        }

        [HttpPost]
        public async Task DeleteEntry([FromBody] DiaryCatch info)
        {
            await diaryService.DeleteEntry(info);
        }

    }
}
