using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Gerivize.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("creation_date")]
        [DefaultValue(typeof(DateTime), "")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Column("super_user")]
        public bool SuperUser { get; set; }

        [Column("responsible")]
        public bool Responsible { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }
}
