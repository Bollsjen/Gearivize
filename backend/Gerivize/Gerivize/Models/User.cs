using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerivize.Models
{
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

        [Column("super_user")]
        public bool SuperUser { get; set; }

        [Column("responsible")]
        public bool Responsible { get; set; }
    }
}
