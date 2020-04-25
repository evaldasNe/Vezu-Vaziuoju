namespace Vezu_Vaziuoju
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        public int PostId { get; set; }

        [Required]
        [StringLength(255)]
        public string DriverId { get; set; }

        [Required]
        [StringLength(255)]
        public string PassengerId { get; set; }

        public virtual Post Post { get; set; }

        public virtual Driver Driver { get; set; }
    }
}
