using Repository;
using Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepositoryForTests;

namespace Services
{
    public class DiaryService <T>
    {
        WorkingWithDiary workingWithDiary;
        WorkingWithDiaryForTest workingWithDiaryForTest;
        MappingForDiary<T> mapping;

        bool TestMod;

        public DiaryService(bool TestMod = false)
        {
            workingWithDiary = new WorkingWithDiary();
            mapping = new MappingForDiary<T>();
            workingWithDiaryForTest = new WorkingWithDiaryForTest();

            this.TestMod = TestMod;
        }

        public async Task<List<T>> LoadDates(string login)
        {
            List<Diary> diary;

            if (TestMod)
            {
                diary =await workingWithDiaryForTest.GetDates(login);
            }
            else
            {
                diary = await workingWithDiary.GetDates(login);
            }

            List<T> result = new List<T>();

            foreach (var item in diary)
            {
                result.Add(mapping.BackDiaryMapping(item));
            }

            return result;

        }

        public async Task<List<T>> LoadEntries(T obj, string login)
        {
            Diary diary = mapping.DiaryMapping(obj);

            List<Diary> diaryList;

            if (TestMod)
            {
                diaryList = await workingWithDiaryForTest.GetEntries(diary, login);
            }
            else
            {
                diaryList = await workingWithDiary.GetEntries(diary, login);
            }

            List<T> result = new List<T>();

            foreach (var item in diaryList)
            {
                result.Add(mapping.BackDiaryMapping(item));
            }

            return result;

        }

        public async Task AddEntry(T obj, string login)
        {
            Diary diary = mapping.DiaryMapping(obj);

            if (TestMod)
            {
                await workingWithDiaryForTest.AddEntry(diary, login);
            }
            else
            {
                await workingWithDiary.AddEntry(diary, login);
            }

        }

        public async Task EditEntry(T obj)
        {
            Diary diary = mapping.DiaryMapping(obj);

            if (TestMod)
            {
                await workingWithDiaryForTest.EditEntry(diary);
            }
            else
            {
                await workingWithDiary.EditEntry(diary);
            }

        }

        public async Task DeleteEntry(T obj)
        {
            Diary diary = mapping.DiaryMapping(obj);

            if (TestMod)
            {
                await workingWithDiaryForTest.DeleteEntry(diary); ;
            }
            else
            {
                await workingWithDiary.DeleteEntry(diary);
            }

        }

        public bool CheckAddEntry(T obj, string login)
        {
            Diary diary = mapping.DiaryMapping(obj);

            if (TestMod)
            {
                return workingWithDiaryForTest.CheckAddEntry(diary, login); ;
            }

            return false;
        }

        public bool CheckEntry(T obj)
        {
            Diary diary = mapping.DiaryMapping(obj);

            if (TestMod)
            {
                return workingWithDiaryForTest.CheckEntry(diary);
            }

            return false;
        }
    }
}
