namespace Vezu_Vaziuoju
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Comments = new HashSet<Comment>();
            Tickets = new HashSet<Ticket>();
            Trips = new HashSet<Trip>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string AddressFrom { get; set; }

        [Required]
        [StringLength(255)]
        public string AddressTo { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartTime { get; set; }

        public int TotalSeats { get; set; }

        public int AvailableSeats { get; set; }

        public double TicketPrice { get; set; }

        public bool IsValid { get; set; }

        [Required]
        [StringLength(255)]
        public string DriverId { get; set; }

        [Required]
        [StringLength(255)]
        public string AdminId { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Driver Driver { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
