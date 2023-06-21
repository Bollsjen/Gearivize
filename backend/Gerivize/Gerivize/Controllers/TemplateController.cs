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
    public class TemplateController : ControllerBase
    {
        private readonly TemplateRepository _templateRepository;
        private readonly TemplateManager _templateManager;

        public TemplateController()
        {
            _templateRepository = new TemplateRepository();
            _templateManager = new TemplateManager();
        }

        [HttpGet]
        public ActionResult<List<TestTemplate>> Get()
        {
            return Ok(_templateRepository.getAll());
        }

        [HttpGet("{Id}")]
        public ActionResult<TestTemplate> GetByTemplateId(Guid id)
        {
            return _templateRepository.getById(id);
        }

        [HttpGet("Instruments")]
        public ActionResult<TemplateConnector> GetAllInstruments()
        {
              return Ok(_templateRepository.getAllInstruments());
        }

        [HttpPost("Template")]
        public ActionResult<TestTemplate> AddTemplate([FromBody] TestTemplate testTemplate)
        {
            _templateRepository.AddTemplate(testTemplate);
            return Ok();
        }

        [HttpPost("Instrument/{templateId}/{instrumentId}")]
        public ActionResult<TemplateConnector> AddInstrumentToTemplate(Guid templateId, string instrumentId)
        {
            _templateRepository.AddInstrumentToTemplate(templateId, instrumentId);
            return Ok();
        }

        [HttpPut("Template")]
        public ActionResult<TestTemplate> UpdateTemplate([FromBody] TestTemplate testTemplate)
        {
            _templateRepository.UpdateTemplate(testTemplate);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public ActionResult<TestTemplate> DeleteTemplate(Guid templateId)
        {
            _templateRepository.DeleteTemplate(templateId);
            return Ok();
        }

        [HttpDelete("Instrument/{templateId}/{instrumentId}")]
        public ActionResult<TemplateConnector> DeleteInstrumentFromTemplate(Guid templateId, string instrumentId)
        {
            _templateRepository.DeleteInstrumentFromTemplate(templateId, instrumentId);
            return Ok();
        }
    }
}
