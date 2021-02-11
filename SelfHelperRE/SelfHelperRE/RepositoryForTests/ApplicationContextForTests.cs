using Repository;
using System.Collections.Generic;

namespace RepositoryForTests
{
    public class ApplicationContextForTests
    {
        public List<User> users;
        public List<Diary> diaries;
        public List<Note> notes;
        public List<Target> targets;

        public ApplicationContextForTests()
        {
            users = new List<User>{
                (new User { Id = 0, Login = "zoldik", Email = "zoldikds@mail.ru", Password = "1111" }),
                (new User { Id = 1, Login = "zoldik1", Email = "zoldikds1@mail.ru", Password = "2222" }),
                (new User { Id = 2, Login = "zoldik2", Email = "zoldikds2@mail.ru", Password = "3333" })
            };

            diaries = new List<Diary>
            {
                new Diary { Id = 0, DateTime = System.Convert.ToDateTime("01.01.2021 00:00:00"), Text = "Test", User = users[0]},
                new Diary { Id = 1, DateTime = System.Convert.ToDateTime("02.01.2021 00:01:00"), Text = "Test2", User = users[1]},
                new Diary { Id = 2, DateTime = System.Convert.ToDateTime("01.01.2021 00:02:00"), Text = "Test3", User = users[2]},
                new Diary { Id = 3, DateTime = System.Convert.ToDateTime("02.01.2021 00:03:00"), Text = "Test4", User = users[0]}
            };

            notes = new List<Note>
            {
                new Note { Id = 0, Text = "Test note", Important = true, Title = "Test title", Topic = "Test topic", DateTime = System.Convert.ToDateTime("01.01.2021 00:00:00"),  User = users[0]},
                new Note { Id = 1, Text = "Test note 2", Important = true, Title = "Test title 2", Topic = "Test topic 2", DateTime = System.Convert.ToDateTime("02.01.2021 00:01:00"), User = users[1]},
                new Note { Id = 2, Text = "Test note 3", Important = true, Title = "Test title 3", Topic = "Test topic 3", DateTime = System.Convert.ToDateTime("01.01.2021 00:02:00"), User = users[2]},
                new Note { Id = 3, Text = "Test note 4", Important = true, Title = "Test title 4", Topic = "Test topic 4",DateTime = System.Convert.ToDateTime("02.01.2021 00:03:00"),  User = users[0]},
            };  
            
            targets = new List<Target>
            {
                new Target { Id = 0, Text = "Test target", DateTimeFirst = System.Convert.ToDateTime("01.01.2021 00:00:00"), DateTimeSecond = System.Convert.ToDateTime("03.01.2021 00:00:00"), Status = "Completed", User = users[0]},
                new Target { Id = 1, Text = "Test target 2", DateTimeFirst = System.Convert.ToDateTime("02.01.2021 00:01:00"), DateTimeSecond = System.Convert.ToDateTime("04.01.2021 00:01:00"), Status = "Failed", User = users[1]},
                new Target { Id = 2, Text = "Test target 3", DateTimeFirst = System.Convert.ToDateTime("01.01.2021 00:00:00"), DateTimeSecond = System.Convert.ToDateTime("04.01.2021 00:00:00"), Status = "Performed", User = users[2]},
                new Target { Id = 3, Text = "Test target 3", DateTimeFirst = System.Convert.ToDateTime("01.01.2021 00:01:00"), DateTimeSecond = System.Convert.ToDateTime("02.01.2021 00:01:00"), Status = "Failed", User = users[0]},
            };
        }
    }
}
