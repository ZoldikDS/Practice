using System;

namespace Repository
{

    public class Target
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTimeFirst { get; set; }
        public DateTime DateTimeSecond { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
