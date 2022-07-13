using TodoApp.Data;

namespace TodoApp.Models {
    public class GroupsNotesViewModel {
        public string Charge { get; set; }

        public IEnumerable<Note> Notes { get; set; }
        
        public IEnumerable<Group> Groups { get; set; }
        
        public IEnumerable<UserGroup> UserGroups { get; set; }
    }
}