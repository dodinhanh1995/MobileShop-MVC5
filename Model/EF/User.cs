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

        [Required, Display(Name ="Tên đăng nhập")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required, Display(Name = "Mật khẩu")]
        [StringLength(32)]
        public string Password { get; set; }

        [StringLength(70), Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [StringLength(250), Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Required]
        [StringLength(70), Display(Name ="Địa chỉ email")]
        public string Email { get; set; }

        [StringLength(15), Display(Name = "Điện thoại")]
        public string Phone { get; set; }

        [Column(TypeName = "date"), Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50), Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }

        [Display(Name = "Trạng thái")]
        public int Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
