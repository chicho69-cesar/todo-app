using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models {
    public class AddTaskViewModel {
        public int GroupId { get; set; }

        [Display(Name = "Tarea")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [MaxLength(1000, ErrorMessage = "La {0} no puede excederse de {1} caracteres")]
        public string Text { get; set; }

        [Display(Name = "Fecha limite")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "La fecha de fin de la tarea es requerida")]
        public DateTime FinishDate { get; set; } = DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
    }
}