using CinemaManagement.Models;
using CinemaManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaManagement.Pages.Cinema
{
    public class SignUpModel : PageModel
    {
        private readonly CenimaDBContext _context;

        [BindProperty]
        public RegisterViewModel InputRegister { get; set; }

        public string Msg { get; set; }

        public SignUpModel(CenimaDBContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            InputRegister = new RegisterViewModel();
        }

        public IActionResult OnPost()
        {
            Msg = string.Empty;
            if (ModelState.IsValid)
            {
                var checkEmail = _context.Persons.SingleOrDefault(p => p.Email.Equals(InputRegister.Email));
                if (checkEmail == null)
                {

                }
                else
                {
                    Msg = "Email đã được đăng ký, vui lòng dùng email khác!";
                    return Page();
                }
            }
            return Page();
        }
    }
}