namespace Model.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("About")]
    public partial class About
    {
        public int Id { get; set; }

        [StringLength(50), Display(Name="Tên")]
        public string Name { get; set; }

        [StringLength(250), Display(Name = "Ảnh đại diện")]
        public string Image { get; set; }

        [Column(TypeName = "ntext"), Display(Name = "Chi tiết")]
        public string Detail { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
