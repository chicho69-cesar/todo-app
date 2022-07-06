using System;
using System.Collections.Generic;

namespace TodoApp.Data
{
    public partial class Note
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int State { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
