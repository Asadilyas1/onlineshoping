namespace shoping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Feedbooks = new HashSet<Feedbook>();
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Account_id { get; set; }

        [StringLength(100)]
        public string Account_Name { get; set; }

        [StringLength(200)]
        public string Account_adress { get; set; }

        [StringLength(50)]
        public string Account_phone { get; set; }

        [StringLength(100)]
        public string Account_Email { get; set; }

        [StringLength(200)]
        public string Account_password { get; set; }

        [StringLength(50)]
        public string Account_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedbook> Feedbooks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
