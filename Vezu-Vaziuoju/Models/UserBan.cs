namespace Vezu_Vaziuoju
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using Vezu_Vaziuoju.Models;

    [Table("UserBan")]
    public partial class UserBan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime From { get; set; }

        [Column(TypeName = "date")]
        public DateTime To { get; set; }

        [Required]
        [StringLength(255)]
        public string UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string AdminId { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
