using System;


namespace Repository
{
    public class Diary
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
