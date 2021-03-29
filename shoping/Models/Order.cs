namespace shoping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_details = new HashSet<Order_details>();
        }

        [Key]
        public int Order_id { get; set; }

        [StringLength(150)]
        public string Order_Name { get; set; }

        [StringLength(150)]
        public string Order_adress { get; set; }

        [StringLength(150)]
        public string Order_Email { get; set; }

        [StringLength(50)]
        public string Oder_phone_No { get; set; }

        [StringLength(100)]
        public string Order_type { get; set; }

        public DateTime? Oder_date_time { get; set; }

        [StringLength(150)]
        public string Order_status { get; set; }

        [StringLength(50)]
        public string Order_delivery_status { get; set; }

        public int Account_Fid { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_details> Order_details { get; set; }
    }
}
