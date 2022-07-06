using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.DTOs {
    public class GroupDTO {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        public ICollection<UserGroupDTO> UserGroups { get; set; }

        public ICollection<TaskWorkDTO> TaskWorks { get; set; }

        //TODO: Cambiar la ruta para el despliegue
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7193/images/noimage.png"
            : $"https://todolistapp.blob.core.windows.net/groups/{ImageId}";
    }
}