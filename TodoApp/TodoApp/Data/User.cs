using System;
using System.Collections.Generic;

namespace TodoApp.Data
{
    public partial class User
    {
        public User()
        {
            Notes = new HashSet<Note>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? ImageId { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
