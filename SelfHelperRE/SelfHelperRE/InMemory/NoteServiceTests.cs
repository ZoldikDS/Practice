using AutoMapper;
using Mapping;
using NUnit.Framework;
using RepositoryForTests;
using SelfHelperRE;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InMemory
{
    [TestFixture]
    public class NoteServiceTests
    {
        NoteService<NoteCatch> noteService;

        WorkingWithNoteForTests workingWithNoteForTests = new WorkingWithNoteForTests();

        [SetUp]
        public void Setup()
        {
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingNote<NoteCatch>());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            noteService = new NoteService<NoteCatch>(workingWithNoteForTests, mapper);

        }

        [Test]
        public async Task Load_Note_Categories_Test()
        {
            string login = "zoldik";

            IEnumerable<NoteCatch> result = await noteService.LoadCategories(login);

            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase("Все")]
        [TestCase("Важные")]
        [TestCase("Test topic")]
        public async Task Load_Notes_By_Date_Test(string topic)
        {
            NoteCatch data = new NoteCatch() { DateTime = "01.01.2021 00:00:00", Topic = topic };

            string login = "zoldik";

            List<NoteCatch> result = await noteService.LoadNotes(data, login);

            Assert.IsNotNull(result);
        }
        
        [Test]
        [TestCase("Все")]
        [TestCase("Важные")]
        [TestCase("Test topic")]
        public async Task Load_Notes_Test(string topic)
        {
            NoteCatch data = new NoteCatch() { Topic = topic };

            string login = "zoldik";

            List<NoteCatch> result = await noteService.LoadNotes(data,login);

            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase("true")]
        [TestCase("false")]
        public async Task Add_Note_Test(string important)
        {
            NoteCatch data = new NoteCatch() { Topic = "Topic in test", Title = "Title in test", Text = "Text in test", Important = important };

            string login = "zoldik";

            Assert.AreEqual(1, await noteService.AddNote(data, login));
        }
        
        [Test]
        [TestCase("true")]
        [TestCase("false")]
        public async Task Edit_Note_Test(string important)
        {
            NoteCatch data = new NoteCatch() {Id = "2", Topic = "Topic in test for edit", Title = "Title in test for edit", Text = "Text in test for edit", Important = important };

            Assert.AreEqual(1, await noteService.EditNote(data));
        }
        
        [Test]
        public async Task Delete_Note_Test()
        {
            NoteCatch data = new NoteCatch() { Id = "3" };

            Assert.AreEqual(1, await noteService.DeleteNote(data));
        }

    }
}
