using NUnit.Framework;
using RepositoryForTests;
using SelfHelperRE.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InMemory
{
    [TestFixture]
    public class NoteServiceTests
    {
        NoteService<NoteData> noteDataService;

        WorkingWithNoteForTests workingWithNoteForTests = new WorkingWithNoteForTests();

        [SetUp]
        public void Setup()
        {
            noteDataService = new NoteService<NoteData>(workingWithNoteForTests);
        }

        [Test]
        public async Task Load_Note_Categories_Test()
        {
            string login = "zoldik";

            IEnumerable<NoteData> result = await noteDataService.LoadCategories(login);

            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase("Все")]
        [TestCase("Важные")]
        [TestCase("Test topic")]
        public async Task Load_Notes_By_Date_Test(string topic)
        {
            NoteData data = new NoteData() { DateTime = Convert.ToDateTime("01.01.2021 00:00:00"), Topic = topic };

            string login = "zoldik";

            List<NoteData> result = await noteDataService.LoadNotes(data, login);

            Assert.IsNotNull(result);
        }
        
        [Test]
        [TestCase("Все")]
        [TestCase("Важные")]
        [TestCase("Test topic")]
        public async Task Load_Notes_Test(string topic)
        {
            NoteData data = new NoteData() { Topic = topic };

            string login = "zoldik";

            List<NoteData> result = await noteDataService.LoadNotes(data,login);

            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task Add_Note_Test(bool important)
        {
            NoteData data = new NoteData() { Topic = "Topic in test", Title = "Title in test", Text = "Text in test", Important = important };

            string login = "zoldik";

            await noteDataService.AddNote(data, login);

            Assert.IsTrue(noteDataService.CheckAddNoteForTest(data, login));
        }
        
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task Edit_Note_Test(bool important)
        {
            NoteData data = new NoteData() {Id = 2, Topic = "Topic in test for edit", Title = "Title in test for edit", Text = "Text in test for edit", Important = important };

            await noteDataService.EditNote(data);

            Assert.IsTrue(noteDataService.CheckNoteForTest(data));
        }
        
        [Test]
        public async Task Delete_Note_Test()
        {
            NoteData data = new NoteData() { Id = 3 };

            await noteDataService.DeleteNote(data);

            Assert.IsFalse(noteDataService.CheckNoteForTest(data));
        }

    }
}
