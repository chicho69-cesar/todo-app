using System.ComponentModel.DataAnnotations;
using TodoApp.Enums;

namespace TodoApp.Models.DTOs {
    public class UserGroupDTO {
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        public UserDTO User { get; set; }

        [Display(Name = "Grupo")]
        public GroupDTO Group { get; set; }

        [Display(Name = "Estado")]
        public StateInGroup State { get; set; }
    }
}