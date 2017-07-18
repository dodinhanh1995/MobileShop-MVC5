namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập tên danh mục"), Display(Name="Tên danh mục")]
        [StringLength(50)]
        public string Name { get; set; }

       [Display(Name = "Tên danh mục cha")]
        public int? ParentID { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập thứ tự hiển thị"), Display(Name = "Thứ tự")]
        public int DisplayOrder { get; set; }

        [StringLength(50), Required(ErrorMessage ="Vui lòng nhập MetaTitle")]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        [DataType(DataType.Date), Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trạng thái"), Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
