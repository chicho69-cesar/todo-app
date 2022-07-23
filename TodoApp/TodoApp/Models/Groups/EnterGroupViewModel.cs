using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models {
    public class EnterGroupViewModel {
        [Display(Name = "Codigo")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string Code { get; set; }
    }
}