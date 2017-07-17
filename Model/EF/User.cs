namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập"), Display(Name = "Tên đăng nhập")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu"), Display(Name = "Mật khẩu")]
        [StringLength(32), MinLength(6, ErrorMessage = "Mật khẩu tối thiểu phải là 6 ký tự")]
        public string Password { get; set; }

        [StringLength(70), Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [StringLength(250), Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email"), EmailAddress(ErrorMessage = "Định đạng email không hợp lệ")]
        [StringLength(70), Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

        [StringLength(15), Display(Name = "Điện thoại"), Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^0\d{9,14}", ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string Phone { get; set; }

        [Display(Name = "Ngày tạo"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [StringLength(50), Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }

        [Display(Name = "Trạng thái"), Required(ErrorMessage = "Vui lòng chọn trạng thái")]
        public int Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
