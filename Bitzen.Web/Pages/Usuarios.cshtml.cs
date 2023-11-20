using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bitzen.Web.Pages
{
    public class UsuariosModel : PageModel
    {
        private readonly ILogger<UsuariosModel> _logger;

        public UsuariosModel(ILogger<UsuariosModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}