using Gerivize.EnumTypes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gerivize.Models
{
    [Table("instrument")]
    public class Instrument
    {
        [Key]
        [Column("a_number")]
        public string ANumber { get; set; }

        [Column("user_id")]
        [DefaultValue(null)]
        public Guid? UserId { get; set; }

        [Column("test_template_id")]
        [DefaultValue(null)]
        public Guid? TestTemplateId { get; set; }

        [Column("instrument_name")]
        public string InstrumentName { get; set; }

        [Column("manufacturer")]
        public string Manufacturer { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("test")]
        [EnumDataType(typeof(TestType))]
        public TestType Test { get; set; }

        [Column("serial_number")]
        public string? SerialNumber { get; set; }

        [Column("last_calibrated_date")]
        public DateTime LastCalibratedDate { get; set; }

        [Column("next_calibration_date")]
        public DateTime NextCalibrationDate { get; set; }

        [Column("creation_date")]
        [DefaultValue(typeof(DateTime), "")]
        public DateTime CreationTime { get; set; } = DateTime.Now;

        [Column("comment")]
        public string? Comment { get; set; }

        [Column("reference_calibration_instruction")]
        public string? ReferenceCalibrationInstruction { get; set; }

        [Column("internal_calibration")]
        public string? InternalCalibration { get; set; }

        [Column("calibration_report_number")]
        public string? CalibrationReportNumber { get; set; }

        [Column("note")]
        public string? Note { get; set; }

        [Column("value")]
        [DefaultValue(0)]
        public int? Value { get; set; }

        [Column("external_calibration")]
        [DefaultValue(false)]
        public bool ExternalCalibration { get; set; }

        [Column("inactive")]
        [DefaultValue(false)]
        public bool Inactive { get; set; }

        [Column("image_source")]
        public string? Image { get; set; }

        [Column("needs_calibration")]
        [DefaultValue(true)]
        public bool NeedsCalibration { get; set; }


        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public virtual User? User { get; set; }

        [ForeignKey(nameof(TestTemplateId))]
        [JsonIgnore]
        public virtual TestTemplate? TestTemplate { get; set; }

        public override string ToString()
        {
            return $"A-Number: {ANumber}";
        }
    }
}
