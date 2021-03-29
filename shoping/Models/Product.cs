namespace shoping.Models
{
    using System.Web;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Feedbooks = new HashSet<Feedbook>();
            Order_details = new HashSet<Order_details>();
        }

        [Key]
        public int Product_id { get; set; }

        [StringLength(100)]
        public string Product_Name { get; set; }

        public string Product_Pic { get; set; }
        [NotMapped]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public HttpPostedFileBase picture { get; set; }
       
        [NotMapped]

        public int product_quantity { get; set; }


        [Column(TypeName = "numeric")]
        public decimal? Product_Prachase_price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Product_Sale_price { get; set; }

        [StringLength(150)]
        public string Product_description { get; set; }

        [StringLength(50)]
        public string Product_Status { get; set; }

        public int? Sub_Category_Fid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedbook> Feedbooks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_details> Order_details { get; set; }

        public virtual Sub_category Sub_category { get; set; }
    }
}
