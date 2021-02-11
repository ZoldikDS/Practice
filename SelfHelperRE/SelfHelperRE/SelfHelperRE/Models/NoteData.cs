using System;

namespace SelfHelperRE.Models
{
    public class NoteData
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public bool Important { get; set; }
    }
}
