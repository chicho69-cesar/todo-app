using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models {
    public class EditGroupViewModel {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El {0} debe tener maximo {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Foto")]
        public Guid? ImageId { get; set; }

        [Display(Name = "Foto")]
        public IFormFile ImageFile { get; set; }

        //TODO: Cambiar la ruta para el despliegue
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7193/images/noimage.png"
            : $"https://todolistapp.blob.core.windows.net/groups/{ImageId}";
    }
}