using System.ComponentModel.DataAnnotations;
using TodoApp.Enums;

namespace TodoApp.Data.Entities {
    public class TaskWork {
        public int Id { get; set; }

        [Display(Name = "Nota")]
        [MaxLength(1000, ErrorMessage = "La nota no puede excederse de {1} caracteres")]
        public string Text { get; set; }

        [Display(Name = "Fecha de inicio")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha de fin")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Estado")]
        public StateTask State { get; set; }

        [Display(Name = "Grupo")]
        public Group Group { get; set; }
    }
}