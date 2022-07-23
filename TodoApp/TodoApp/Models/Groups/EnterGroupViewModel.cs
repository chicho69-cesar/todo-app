using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models {
    public class EnterGroupViewModel {
        [Display(Name = "Codigo")]
        public string Code { get; set; }
    }
}