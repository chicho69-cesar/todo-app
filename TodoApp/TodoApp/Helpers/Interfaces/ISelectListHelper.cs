using Microsoft.AspNetCore.Mvc.Rendering;

namespace TodoApp.Helpers.Interfaces {
    public interface ISelectListHelper {
        IEnumerable<SelectListItem> GetStates();
    }
}