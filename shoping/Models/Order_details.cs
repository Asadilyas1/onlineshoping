namespace shoping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_details
    {
        [Key]
        public int Od_id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Od_Sale_price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Od_Prachase_price { get; set; }

        public int? Od_quantity { get; set; }

        public int Order_Fid { get; set; }

        public int Product_Fid { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
