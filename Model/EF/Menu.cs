namespace Model.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Menu")]
    public partial class Menu
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập tên menu")]
        [StringLength(50), Display(Name ="Tên menu")]
        public string Text { get; set; }

        [StringLength(250), Required(ErrorMessage = "Vui lòng nhập đường dẫn liên kết"), Display(Name ="Đường dẫn")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thứ tự hiển thị"), Display(Name = "Thứ tự")]
        public int DisplayOrder { get; set; }

        [StringLength(10)]
        public string Target { get; set; }

        public bool? Status { get; set; }

        public int? TypeID { get; set; }

        public virtual MenuType MenuType { get; set; }
    }
}
