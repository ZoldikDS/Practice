using NUnit.Framework;
using RepositoryForTests;
using SelfHelperRE;
using SelfHelperRE.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InMemory
{
    [TestFixture]
    public class DiaryServiceTests
    {
        DiaryService<DiaryCatch> diaryService;
        DiaryService<DiaryData> diaryDataService;

        WorkingWithDiaryForTest WorkingWithDiaryForTest = new WorkingWithDiaryForTest();

        [SetUp]
        public void Setup()
        {
            diaryService = new DiaryService<DiaryCatch>(WorkingWithDiaryForTest);
            diaryDataService = new DiaryService<DiaryData>(WorkingWithDiaryForTest);
;
        }

        [Test]
        public async Task Load_Diary_Dates_Test()
        {
            string login = "zoldik";

            IEnumerable<DiaryData> result = await diaryDataService.LoadDates(login);

            Assert.IsNotNull(result);
        }    
        
        [Test]
        public async Task Load_Diary_Entries_Test()
        {
            string login = "zoldik";

            DiaryData diaryData = new DiaryData { DateTime = Convert.ToDateTime("01.01.2021 00:00:00") };

            List<DiaryData> result = await diaryDataService.LoadEntries(diaryData, login);

            Assert.IsNotNull(result);
        }     
        
        [Test]
        public async Task Add_Diary_Entries_Test()
        {
            string login = "zoldik";

            DiaryCatch diaryData = new DiaryCatch { Text = "Test from test" };

            await diaryService.AddEntry(diaryData, login);

            Assert.IsTrue(diaryService.CheckAddEntry(diaryData, login));
        }
             
        [Test]
        public async Task Edit_Diary_Entries_Test()
        {
            DiaryCatch diaryData = new DiaryCatch { Id = "3", Text = "12313", Date = "01.01.2021 00:00:00" };
            
            await diaryService.EditEntry(diaryData);

            Assert.IsTrue(diaryService.CheckEntry(diaryData));
        }
                
        [Test]
        public async Task Delete_Diary_Entries_Test()
        {
            DiaryCatch diaryData = new DiaryCatch { Id = "2", Text = "12313", Date = "01.01.2021 00:00:00" };

            await diaryService.DeleteEntry(diaryData);

            Assert.IsFalse(diaryService.CheckEntry(diaryData));
        }

    }
}
