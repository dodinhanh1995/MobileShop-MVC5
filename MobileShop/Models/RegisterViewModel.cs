using System.ComponentModel.DataAnnotations;

namespace MobileShop.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Vui lòng nhập tên đăng nhập!")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        

        [Required(ErrorMessage ="Vui lòng nhập mật khẩu!")]
        [StringLength(32, ErrorMessage = "Mật khẩu tối thiểu phải 6 ký tự", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email!")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ!")]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

    }
}