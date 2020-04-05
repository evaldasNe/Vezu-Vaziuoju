namespace Vezu_Vaziuoju
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public double Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidTill { get; set; }

        public bool IsUsed { get; set; }

        [Required]
        [StringLength(255)]
        public string PassengerId { get; set; }

        public int AdvertId { get; set; }

        public virtual Advert Advert { get; set; }

        public virtual Passenger Passenger { get; set; }
    }
}
