using System.ComponentModel.DataAnnotations;
using TodoApp.Data;

namespace TodoApp.Models {
    public class GroupTasksViewModel {
        public int GroupId { get; set; }

        [Display(Name = "Grupo")]
        public string Name { get; set; }

        [Display(Name = "Tareas")]
        public IEnumerable<TaskWork> Tasks { get; set; }
    }
}