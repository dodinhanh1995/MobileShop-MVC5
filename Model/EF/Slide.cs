namespace Model.EF
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Slide")]
    public partial class Slide
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50), Required(ErrorMessage ="Vui lòng nhập tên quảng cáo"), Display(Name="Tên quảng cáo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui chọn hình ảnh quảng cáo"), Display(Name = "Hình ảnh")]
        [StringLength(200)]
        public string Image { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thứ tự hiển thị"), Display(Name = "Thứ tự")]
        public int DisplayOrder { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        [StringLength(20)]
        public string Target { get; set; }

        [StringLength(500), Display(Name = "Chi tiết")]
        public string Description { get; set; }

        [DataType(DataType.Date), Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái"), Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
