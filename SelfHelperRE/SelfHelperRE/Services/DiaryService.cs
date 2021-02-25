using Repository;
using Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbModels;
using AutoMapper;

namespace Services
{
    public class DiaryService <T>
    {
        IDiary<Diary> service;
        IMapper diaryMapper;

        public DiaryService(IDiary<Diary> service, IMapper diaryMapper)
        {
            this.service = service;

            this.diaryMapper = diaryMapper;
        }

        public async Task<List<T>> LoadDates(string login)
        {
            List<Diary> diary = await service.GetDates(login);

            List<T> result = new List<T>();

            foreach (var item in diary)
            {
                result.Add(diaryMapper.Map<T>(item));
            }

            return result;
        }

        public async Task<List<T>> LoadEntries(T obj, string login)
        {
            Diary diary = diaryMapper.Map<Diary>(obj);

            List<Diary> diaryList = await service.GetEntries(diary, login);

            List<T> result = new List<T>();

            foreach (var item in diaryList)
            {
                result.Add(diaryMapper.Map<T>(item));
            }

            return result;
        }

        public async Task<int> AddEntry(T obj, string login)
        {
            Diary diary = diaryMapper.Map<Diary>(obj);

            return await service.AddEntry(diary, login);

        }

        public async Task<int> EditEntry(T obj)
        {
            Diary diary = diaryMapper.Map<Diary>(obj);

            return await service.EditEntry(diary);

        }

        public async Task<int> DeleteEntry(T obj)
        {
            Diary diary = diaryMapper.Map<Diary>(obj);

            return await service.DeleteEntry(diary);

        }
    }
}
