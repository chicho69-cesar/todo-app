using Microsoft.AspNetCore.Mvc.Rendering;
using TodoApp.Enums;
using TodoApp.Helpers.Interfaces;

namespace TodoApp.Helpers {
    public class SelectListHelper : ISelectListHelper {
        public IEnumerable<SelectListItem> GetNotesStates() => new List<SelectListItem>() {
            new SelectListItem {
                Text = StateTask.Activa.ToString(),
                Value = $"{((int)(StateTask.Activa))}"
            },
            new SelectListItem {
                Text = StateTask.Terminada.ToString(),
                Value = $"{((int)(StateTask.Terminada))}"
            },
            new SelectListItem {
                Text = StateTask.Cancelada.ToString(),
                Value = $"{((int)(StateTask.Cancelada))}"
            }
        };
    }
}