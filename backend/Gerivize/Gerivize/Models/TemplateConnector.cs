using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerivize.Models
{
    [Table("template_connector")]
    public class TemplateConnector
    {
        [Key]
        [Column("templateId")]
        public Guid TestTemplateId { get; set; }

        [Column("instrumentId")]
        public string InstrumentId { get; set; }

        [ForeignKey(nameof(TestTemplateId))]
        public virtual TestTemplate? TestTemplate { get; set; }

        [ForeignKey(nameof(InstrumentId))]
        public virtual Instrument? Instrument { get; set; }
    }
}
