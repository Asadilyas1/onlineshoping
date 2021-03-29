namespace shoping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contact_us
    {
        [Key]
        public int Customer_id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Message { get; set; }

        public DateTime? Date_time { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
    }
}
