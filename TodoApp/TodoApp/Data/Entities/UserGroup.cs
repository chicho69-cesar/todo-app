using TodoApp.Enums;

namespace TodoApp.Data.Entities {
    public class UserGroup {
        public int Id { get; set; }

        public User User { get; set; }

        public Group Group { get; set; }

        public StateInGroup State { get; set; }
    }
}