using System.ComponentModel.DataAnnotations;

namespace CinemaManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email!")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Mật khẩu phải có độ dài ít từ 1 đến 20 kí tự!")]
        public string Password { get; set; }

        [Required]
        public string Fullname { get; set; }

        public bool Gender { get; set; }
    }
}
