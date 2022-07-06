using System;
using System.Collections.Generic;

namespace TodoApp.Data
{
    public partial class Group
    {
        public Group()
        {
            TaskWorks = new HashSet<TaskWork>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ImageId { get; set; }

        public virtual ICollection<TaskWork> TaskWorks { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
