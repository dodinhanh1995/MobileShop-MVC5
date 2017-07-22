namespace Model.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Map")]
    public partial class Map
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50), Required]
        public string APIKey { get; set; }

        [StringLength(50), Display(Name ="Tên"), Required]
        public string Name { get; set; }

        [StringLength(20), Display(Name = "Vĩ độ"), Required]
        public string Latitude { get; set; }

        [StringLength(20), Display(Name = "Kinh độ"), Required]
        public string Longitude { get; set; }

        [StringLength(500), Display(Name ="Thông tin mô tả")]
        public string Description { get; set; }

        [Display(Name ="Trạng thái"), Required]
        public bool Status { get; set; }
    }
}
