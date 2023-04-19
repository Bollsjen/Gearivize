using Gerivize.EnumTypes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerivize.Models
{
    public class Instrument
    {
        [Key]
        [Column("a_number")]
        public string ANumber { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

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
        public string SerialNumber { get; set; }

        [Column("last_calibrated_date")]
        public DateTime LastCalibratedDate { get; set; }

        [Column("next_calibration_date")]
        public DateTime NextCalibrationDate { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("reference_calibration_instruction")]
        public string ReferenceCalibrationInstruction { get; set; }

        [Column("internal_calibration")]
        public string InternalCalibration { get; set; }

        [Column("calibration_report_number")]
        public string CalibrationReportNumber { get; set; }

        [Column("note")]
        public string Note { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("external_calibration")]
        public bool ExternalCalibration { get; set; }


        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
