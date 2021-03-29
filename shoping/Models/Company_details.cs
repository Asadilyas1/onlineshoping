namespace shoping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Company_details
    {
        [Key]
        public int Company_id { get; set; }

        [StringLength(200)]
        public string Company_name { get; set; }

        [StringLength(200)]
        public string Company_contact { get; set; }

        [StringLength(200)]
        public string Company_adress { get; set; }

        [StringLength(200)]
        public string Company_email { get; set; }

        [StringLength(200)]
        public string Company_logo { get; set; }

        [NotMapped]
        public HttpPostedFileBase picture { get; set; }
    }
}
