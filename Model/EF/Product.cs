namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập tên sản phẩm"), Display(Name="Tên sản phẩm")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(10), Display(Name = "Mã code"), Required(ErrorMessage ="Vui lòng nhập mã sản phẩm")]
        public string Code { get; set; }

        [StringLength(500), Display(Name = "Mô tả")]
        public string Description { get; set; }

        [StringLength(250), Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm"), Display(Name = "Giá bán"), Range(100000, 100000000, ErrorMessage ="Giá phải từ 100.000đ đến 100.000.000đ")]
        public decimal Price { get; set; }

        [Display(Name = "Giá khuyến mại"), Range(100000, 100000000, ErrorMessage = "Giá khuyến mại phải từ 100.000đ đến 100.000.000đ")]
        public decimal? PromotionPrice { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn VAT"), Display(Name = "VAT")]
        public bool IncludeVAT { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm"), Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục sản phẩm"), Display(Name = "Danh mục")]
        public int CategoryID { get; set; }

        [Column(TypeName = "ntext"), Display(Name = "Chi tiết")]
        public string Detail { get; set; }

        [DataType(DataType.Date), Display(Name = "Ngày nhập")]
        public DateTime CreatedDate { get; set; }

        [StringLength(50), Display(Name = "Người nhập")]
        public string CreateBy { get; set; }

        [StringLength(100), Required(ErrorMessage = "Vui lòng nhập MetaTitle sản phẩm")]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái sản phẩm"), Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Nổi bật"), DataType(DataType.Date)]
        public DateTime? TopHot { get; set; }

        [System.ComponentModel.DefaultValue(1), Display(Name = "Số lượt xem")]
        public int ViewCount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
