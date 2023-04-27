using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Gerivize.Models
{
    [Table("notification")]
    public class Notification
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("a_number")]
        public string ANumber { get; set; }

        [Column("creation_date")]
        [DefaultValue(typeof(DateTime), "")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Column("has_reacted")]
        public bool HasReacted { get; set; }


        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public virtual User? User { get; set; }

        [ForeignKey(nameof(ANumber))]
        [JsonIgnore]
        public virtual Instrument? Instrument { get; set; }
    }
}
