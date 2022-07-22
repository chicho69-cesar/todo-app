using System.ComponentModel.DataAnnotations;
using TodoApp.Data;

namespace TodoApp.Models {
    public class GroupTasksViewModel {
        [Display(Name = "Grupo")]
        public string Name { get; set; }

        [Display(Name = "Tareas")]
        public IEnumerable<TaskWork> Tasks { get; set; }
    }
}