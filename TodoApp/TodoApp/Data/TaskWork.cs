using System;
using System.Collections.Generic;

namespace TodoApp.Data
{
    public partial class TaskWork
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int State { get; set; }
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
