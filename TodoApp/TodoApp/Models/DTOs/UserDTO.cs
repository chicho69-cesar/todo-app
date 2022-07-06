using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.DTOs {
    public class UserDTO {
        public int Id { get; set; }

        [Display(Name = "Email")]
        [MaxLength(256, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Email { get; set; }

        [MaxLength(256, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string NormalizedEmail { get; set; }

        [Display(Name = "Username")]
        [MaxLength(256, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserName { get; set; }

        [MaxLength(256, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string NormalizedUserName { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(60, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(120, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<NoteDTO> Notes { get; set; }

        public ICollection<UserGroupDTO> UserGroups { get; set; }

        //TODO: Cambiar la ruta para el despliegue
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7193/images/noimage.png"
            : $"https://todolistapp.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Usuario")]
        public string FullName =>
            $"{FirstName} {LastName}";
    }
}