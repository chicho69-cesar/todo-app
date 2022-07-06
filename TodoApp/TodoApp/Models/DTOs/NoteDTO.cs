using System.ComponentModel.DataAnnotations;
using TodoApp.Enums;

namespace TodoApp.Models.DTOs {
    public class NoteDTO {
        public int Id { get; set; }

        [Display(Name = "Nota")]
        [MaxLength(1000, ErrorMessage = "La nota no puede excederse de {1} caracteres")]
        public string Text { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Estado")]
        public StateTask State { get; set; }

        [Display(Name = "Usuario")]
        public UserDTO User { get; set; }
    }
}