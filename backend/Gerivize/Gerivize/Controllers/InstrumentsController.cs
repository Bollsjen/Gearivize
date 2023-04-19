using Gerivize.Interfaces;
using Gerivize.Managers;
using Gerivize.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gerivize.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentsController : ControllerBase
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IInstrumentsManager _instrumentsManager;

        public InstrumentsController(IInstrumentRepository instrumentRepository, IInstrumentsManager instrumentsManager)
        {
            _instrumentRepository = instrumentRepository;
            _instrumentsManager = instrumentsManager;
        }



        // GET: api/<InstrumentsController>
        [HttpGet]
        public ActionResult<List<Instrument>> Get()
        {
            return Ok(_instrumentRepository.getAll());
        }

        // GET api/<InstrumentsController>/5
        [HttpGet("{aNumber}")]
        public ActionResult<Instrument> Get(string aNumber)
        {
            return _instrumentRepository.getById(aNumber);
        }

        // POST api/<InstrumentsController>
        [HttpPost]
        public ActionResult<Instrument> Post([FromBody] Instrument instrument)
        {
            return Ok(_instrumentRepository.createInstrument(instrument));
        }

        // PUT api/<InstrumentsController>/5
        [HttpPut]
        public ActionResult<Instrument> Put([FromBody] Instrument instrument)
        {
            return Ok(_instrumentRepository.updateInstrument(instrument));
        }

        // DELETE api/<InstrumentsController>/5
        [HttpDelete]
        public ActionResult<Instrument> Delete(string aNumber)
        {
            return Ok(_instrumentRepository.deleteInstrument(aNumber));
        }
    }
}
