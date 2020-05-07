namespace Vezu_Vaziuoju
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Vezu_Vaziuoju.Models;

    [Table("Comment")]
    public partial class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        public int PostId { get; set; }

        [Required]
        [StringLength(255)]
        public string UserId { get; set; }

        public virtual Post Post { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class CommentViewModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Comment")]
        public string Text { get; set; }

        public Post Post { get; set; }
    }
}
