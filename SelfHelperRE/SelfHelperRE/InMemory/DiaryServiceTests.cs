using AutoMapper;
using Mapping;
using NUnit.Framework;
using RepositoryForTests;
using SelfHelperRE;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InMemory
{
    [TestFixture]
    public class DiaryServiceTests
    {
        DiaryService<DiaryCatch> diaryService;

        WorkingWithDiaryForTest WorkingWithDiaryForTest = new WorkingWithDiaryForTest();


        [SetUp]
        public void Setup()
        {

            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingDiary<DiaryCatch>());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            diaryService = new DiaryService<DiaryCatch>(WorkingWithDiaryForTest, mapper);

        }

        [Test]
        public async Task Load_Diary_Dates_Test()
        {
            string login = "zoldik";

            IEnumerable<DiaryCatch> result = await diaryService.LoadDates(login);

            Assert.IsNotNull(result);
        }    
        
        [Test]
        public async Task Load_Diary_Entries_Test()
        {
            string login = "zoldik";

            DiaryCatch diaryData = new DiaryCatch { DateTime = "01.01.2021 00:00:00" };

            List<DiaryCatch> result = await diaryService.LoadEntries(diaryData, login);

            Assert.IsNotNull(result);
        }     
        
        [Test]
        public async Task Add_Diary_Entries_Test()
        {
            string login = "zoldik";

            DiaryCatch diaryData = new DiaryCatch { Text = "Test from test" };

            Assert.AreEqual(1, await diaryService.AddEntry(diaryData, login));
        }
             
        [Test]
        public async Task Edit_Diary_Entries_Test()
        {
            DiaryCatch diaryData = new DiaryCatch { Id = "3", Text = "12313", DateTime = "01.01.2021 00:00:00" };
           
            Assert.AreEqual(1, await diaryService.EditEntry(diaryData));
        }
                
        [Test]
        public async Task Delete_Diary_Entries_Test()
        {
            DiaryCatch diaryData = new DiaryCatch { Id = "2", Text = "12313", DateTime = "01.01.2021 00:00:00" };

            Assert.AreEqual(1, await diaryService.DeleteEntry(diaryData));
        }

    }
}
