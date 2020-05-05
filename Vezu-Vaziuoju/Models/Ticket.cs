namespace Vezu_Vaziuoju
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public double Price { get; set; }

        [Column(TypeName = "datetime")]
        [DisplayName("Valid till")]
        public DateTime ValidTill { get; set; }

        [DisplayName("Is Used")]
        public bool IsUsed { get; set; }

        [Required]
        [StringLength(255)]
        public string PassengerId { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public virtual Passenger Passenger { get; set; }
    }
}
