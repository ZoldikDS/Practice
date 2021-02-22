using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Services;
using System.Threading.Tasks;
using SelfHelperRE.Models;
using System;
using System.Linq;
using DbModels;
using Repository;

namespace SelfHelperRE.Controllers
{
    public class DiaryController : Controller
    {
        DiaryService<DiaryCatch> diaryService;
        DiaryService<DiaryData> diaryDataService;

        public DiaryController(IDiary<Diary> service)
        {
            this.diaryService = new DiaryService<DiaryCatch>(service);
            this.diaryDataService = new DiaryService<DiaryData>(service);
        }

        [HttpGet]
        public async Task<IEnumerable<DiaryData>> LoadDates()
        {
            IEnumerable<DiaryData> result = await diaryDataService.LoadDates(User.Identity.Name);
            return result.Distinct(new DiaryDateComparer());
        }

        [HttpPost]
        public async Task<List<DiaryData>> LoadEntries([FromBody] DiaryCatch info)
        {
            DiaryData diaryData = new DiaryData();

            diaryData.Id = Convert.ToInt32(info.Id);
            diaryData.DateTime = Convert.ToDateTime(info.Date);
            diaryData.Text = info.Text;

            return await diaryDataService.LoadEntries(diaryData, User.Identity.Name);
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
