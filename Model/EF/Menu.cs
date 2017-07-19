namespace Model.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Menu")]
    public partial class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập tên menu")]
        [StringLength(50), Display(Name ="Tên menu")]
        public string Text { get; set; }

        [StringLength(250), Required(ErrorMessage = "Vui lòng nhập đường dẫn liên kết"), Display(Name ="Đường dẫn")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thứ tự hiển thị"), Display(Name = "Thứ tự")]
        public int DisplayOrder { get; set; }

        [StringLength(10), Required(ErrorMessage = "Vui lòng chọn loại Target")]
        public string Target { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái"), Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Loại menu")]
        public int? TypeID { get; set; }

        public virtual MenuType MenuType { get; set; }
    }
}
