namespace Model.EF
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Contact")]
    public partial class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập tên"), Display(Name="Tên")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(15), Display(Name = "Điện thoại"), RegularExpression(@"^0\d{9,14}$", ErrorMessage ="Số điện thoại không hợp lệ!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email")]
        [StringLength(50), Display(Name = "Địa chỉ email"), EmailAddress(ErrorMessage ="Địa chỉ email không hợp lệ!")]
        public string Email { get; set; }

        [StringLength(500), Display(Name = "Nội dung yêu cầu"), Required(ErrorMessage ="Vui lòng nhập nội dung yêu cầu!")]
        public string Content { get; set; }

        [Column(TypeName = "date"), Display(Name = "Ngày gửi")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
