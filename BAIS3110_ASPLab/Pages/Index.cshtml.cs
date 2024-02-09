using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3110_ASPLab.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string Message { get; set; }

        [BindProperty]
        public string InputTextBox { get; set; } = string.Empty;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Message = "Set in OnGet()";
        }

        public void OnPost()
        {
            Message = "Posted in OnPost()   " +  DateTime.Now.ToString(); 
        }

    }
}