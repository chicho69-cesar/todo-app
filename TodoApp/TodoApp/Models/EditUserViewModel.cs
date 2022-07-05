using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models {
    public class EditUserViewModel {
        public string Id { get; set; }

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7193/images/noimage.png"
            : $"https://shoppingcar.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}