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
        [StringLength(15), Display(Name = "Điện thoại")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email")]
        [StringLength(50), Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

        [StringLength(500), Display(Name = "Nội dung yêu cầu")]
        public string Content { get; set; }

        [Column(TypeName = "date"), Display(Name = "Ngày gửi")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage ="Vui lòng chọn trạng thái"), Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
