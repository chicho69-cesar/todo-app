using System.Security.Claims;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services {
    public class UserService : IUserService {
        private readonly HttpContext httpContext;

        public UserService(IHttpContextAccessor httpContextAccesor) {
            httpContext = httpContextAccesor.HttpContext;
        }

        public int GetUserById() {
            if (httpContext.User.Identity.IsAuthenticated) {
                var idClaim = httpContext.User.Claims
                    .Where(x => x.Type == ClaimTypes.NameIdentifier)
                    .FirstOrDefault();

                var id = int.Parse(idClaim.Value);

                return id;
            } else {
                throw new ApplicationException("El usuario no esta autenticado");
            }
        }
    }
}