﻿using Gerivize.Managers;
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

        public InstrumentsController()
        {
            _instrumentRepository = new InstrumentRepository();
            _instrumentsManager = new InstrumentsManager();
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

        // POST api/<InstrumentsController>
        [Authorize]
        [HttpPost]
        public ActionResult<Instrument> Post([FromBody] Instrument instrument)
        {
            return Ok(_instrumentRepository.createInstrument(instrument));
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
