using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bitzen.Web.Pages
{
    public class VeiculosModel : PageModel
    {
        private readonly ILogger<VeiculosModel> _logger;

        public VeiculosModel(ILogger<VeiculosModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}