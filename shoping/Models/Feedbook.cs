namespace shoping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedbook")]
    public partial class Feedbook
    {
        [Key]
        public int Feed_id { get; set; }

        public int? Account_fid { get; set; }

        public int? Product_fid { get; set; }

        public DateTime? Date_time { get; set; }

        public int? Rating { get; set; }

        public virtual Account Account { get; set; }

        public virtual Product Product { get; set; }
    }
}
