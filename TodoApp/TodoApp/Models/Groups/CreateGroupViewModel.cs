using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models {
    public class CreateGroupViewModel {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} del grupo es requerido")]
        public string Name { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: Cambiar la ruta para el despliegue
        [Display(Name = "Imagen")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7193/images/noimage.png"
            : $"https://shoppingcar.blob.core.windows.net/groups/{ImageId}";

        [Display(Name = "Foto")]
        public IFormFile ImageFile { get; set; }
    }
}