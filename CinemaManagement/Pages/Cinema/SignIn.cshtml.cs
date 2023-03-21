using CinemaManagement.Models;
using CinemaManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaManagement.Pages.Cinema
{
    public class SignInModel : PageModel
    {

        private readonly CenimaDBContext _context;

        [BindProperty]
        public LoginViewModel InputLogin { get; set; }

        public string Msg { get; set; }

        public SignInModel(CenimaDBContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            Msg = string.Empty;
            if (ModelState.IsValid)
            {
                var checkEmail = _context.Persons.SingleOrDefault(p => p.Email.Equals(InputLogin.Email));
                if (checkEmail != null)
                {
                    if (checkEmail.Password.Equals(InputLogin.Password.Trim()))
                    {
                        HttpContext.Session.SetString("Fullname", checkEmail.Fullname);
                        HttpContext.Session.SetInt32("Role", (int)checkEmail.Type);
                        return RedirectToPage("/Main");
                    }
                    else
                    {
                        Msg = "Mật khẩu không chính xác!";
                        return Page();
                    }
                }
                else
                {
                    Msg = "Email không tồn tại, vui lòng đăng ký!";
                    return Page();
                }
            }
            return Page();
        }

        public IActionResult OnGetLogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}