using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bitzen.Web.Pages
{
    public class AbastecimentosModel : PageModel
    {
        private readonly ILogger<AbastecimentosModel> _logger;

        public AbastecimentosModel(ILogger<AbastecimentosModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}