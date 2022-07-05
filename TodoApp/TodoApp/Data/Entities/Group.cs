using System.ComponentModel.DataAnnotations;

namespace TodoApp.Data.Entities {
    public class Group {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        public ICollection<UserGroup> UserGroups { get; set; }

        public ICollection<TaskWork> TaskWorks { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7193/images/noimage.png"
            : $"https://todolistapp.blob.core.windows.net/groups/{ImageId}";
    }
}