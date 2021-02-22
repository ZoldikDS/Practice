using Repository;
using Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbModels;

namespace Services
{
    public class DiaryService <T>
    {
        MappingForDiary<T> mapping;
        IDiary<Diary> service;

        public DiaryService(IDiary<Diary> service)
        {
            this.service = service;

            mapping = new MappingForDiary<T>();
        }

        public async Task<List<T>> LoadDates(string login)
        {
            List<Diary> diary = await service.GetDates(login);

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

            List<Diary> diaryList = await service.GetEntries(diary, login);

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

            await service.AddEntry(diary, login);

        }

        public async Task EditEntry(T obj)
        {
            Diary diary = mapping.DiaryMapping(obj);

            await service.EditEntry(diary);

        }

        public async Task DeleteEntry(T obj)
        {
            Diary diary = mapping.DiaryMapping(obj);

            await service.DeleteEntry(diary);

        }

        public bool CheckAddEntry(T obj, string login)
        {
            Diary diary = mapping.DiaryMapping(obj);

            return service.CheckAddEntry(diary, login);
        }

        public bool CheckEntry(T obj)
        {
            Diary diary = mapping.DiaryMapping(obj);

            return service.CheckEntry(diary);
        }
    }
}
