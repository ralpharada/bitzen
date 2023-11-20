using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bitzen.Web.Pages
{
    public class MotoristasModel : PageModel
    {
        private readonly ILogger<MotoristasModel> _logger;

        public MotoristasModel(ILogger<MotoristasModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}