using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models {
    public class EditTaskViewModel : AddTaskViewModel {
        public int Id { get; set; }

        [Display(Name = "Estado")]
        [Range(0, 3, ErrorMessage = "Debes seleccionar un estado")]
        public int State { get; set; }

        [Display(Name = "Estados")]
        public IEnumerable<SelectListItem> States { get; set; }
    }
}