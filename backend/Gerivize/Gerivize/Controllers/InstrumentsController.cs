using Gerivize.Managers;
using Gerivize.Models;
using Gerivize.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gerivize.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentsController : ControllerBase
    {
        private readonly InstrumentRepository _instrumentRepository;
        private readonly InstrumentsManager _instrumentsManager;
        private readonly FileExplorerManager _fileExplorerManager;

        public InstrumentsController(FileExplorerManager manager, InstrumentsManager instrumentManager)
        {
            _instrumentRepository = new InstrumentRepository();
            _instrumentsManager = instrumentManager;
            _fileExplorerManager = manager;
        }



        // GET: api/<InstrumentsController>
        [HttpGet]
        public ActionResult<List<Instrument>> Get()
        {
            return Ok(_instrumentRepository.getAll());
        }

        // GET api/<InstrumentsController>/5
        [HttpGet("{aNumber}")]
        public ActionResult<Instrument> GetByANumber(string aNumber)
        {
            return _instrumentRepository.getById(aNumber);
        }

        [HttpGet("image/{imageFileName}")]
        public ActionResult GetImage(string imageFileName)
        {
            if (!System.IO.File.Exists(Environment.CurrentDirectory + @"\Public\Images\Instrument\" + imageFileName)) return NotFound();
            var image = System.IO.File.OpenRead(Environment.CurrentDirectory + @"\Public\Images\Instrument\" + imageFileName);
            return File(image, "image/jpeg");
        }

        [HttpGet("list/x-months/{months}/{external}")]
        public ActionResult<List<Instrument>> GetInstrumentByXMonths(int months, bool external)
        {
            return Ok(_instrumentRepository.getCalibrationNextXMonths(months, external));
        }

        [HttpGet("list/overdue/{external}")]
        public ActionResult<List<Instrument>> GetInstrumentOverdue(bool external)
        {
            return Ok(_instrumentRepository.getCalibrationOverdue(external));
        }

        // POST api/<InstrumentsController>
        [Authorize]
        [HttpPost]
        public ActionResult<Instrument> Post([FromBody] Instrument instrument)
        {
            Instrument newInstrument = _instrumentRepository.createInstrument(instrument);
            if(newInstrument != null && _fileExplorerManager.CreateINstrumentDIrectories(newInstrument.ANumber)) return Ok();
            return BadRequest();
        }

        [Authorize]
        [HttpPost("{aNumber}")]
        public ActionResult PostImage([FromForm] IFormFile file, string aNumber)
        {
            return Ok(_instrumentsManager.ImageUpload(file, aNumber));
        }

        // PUT api/<InstrumentsController>/5
        [Authorize]
        [HttpPut]
        public ActionResult<Instrument> Put([FromBody] Instrument instrument)
        {
            return Ok(_instrumentRepository.updateInstrument(instrument));
        }

        [Authorize]
        [HttpPut("inactive")]
        public ActionResult<Instrument> Activate([FromBody] Instrument instrument)
        {
            return Ok(_instrumentRepository.updateInstrument(instrument));
        }

        // DELETE api/<InstrumentsController>/5
        [Authorize]
        [HttpDelete("inactive/{aNumber}")]
        public ActionResult<Instrument> Delete(string aNumber)
        {
            return Ok(_instrumentRepository.deleteInstrument(aNumber));
        }

        [Authorize]
        [HttpDelete("{aNumber}")]
        public ActionResult<Instrument> ActualDelete(string aNumber)
        {
            return Ok(_instrumentRepository.actualDeleteInstrument(aNumber));
        }
    }
}
