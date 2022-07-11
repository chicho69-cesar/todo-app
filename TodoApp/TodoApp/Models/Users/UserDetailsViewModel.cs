using System.ComponentModel.DataAnnotations;
using TodoApp.Models.DTOs;

namespace TodoApp.Models {
    public class UserDetailsViewModel {
        [Display(Name = "Email")]
        [MaxLength(256, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [MaxLength(256, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserName { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(60, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(120, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Foto")]
        public Guid? ImageId { get; set; }

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

        public int NotesNumber =>
            Notes == null ? 0 : Notes.Count;

        public int GroupsNumber =>
            UserGroups == null ? 0 : UserGroups.Count;

        public int TasksNumbers => 
            UserGroups == null 
            ? 0 : UserGroups.Sum(ug => ug.Group.TaskWorks.Count);
    }
}