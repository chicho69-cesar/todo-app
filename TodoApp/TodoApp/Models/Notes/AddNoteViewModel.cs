using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models {
    public class AddNoteViewModel {
        [Display(Name = "Nota: ")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [MaxLength(1000, ErrorMessage = "La nota no puede excederse de {1} caracteres")]
        public string Text { get; set; }
    }
}