using System;
using System.Collections.Generic;

namespace TodoApp.Data
{
    public partial class UserGroup
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? GroupId { get; set; }
        public int State { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
