using System.ComponentModel.DataAnnotations;
using TodoApp.Enums;

namespace TodoApp.Data.Entities {
    public class Note {
        public int Id { get; set; }

        [Display(Name = "Nota")]
        [MaxLength(1000, ErrorMessage = "La nota no puede excederse de {1} caracteres")]
        public string Text { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Estado")]
        public StateTask State { get; set; }

        [Display(Name = "Usuario")]
        public User User { get; set; }
    }
}