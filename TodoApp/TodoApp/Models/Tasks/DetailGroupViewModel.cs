using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models {
    public class DetailGroupViewModel {
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Imagen")]
        public string ImageFullPath { get; set; }
    }
}