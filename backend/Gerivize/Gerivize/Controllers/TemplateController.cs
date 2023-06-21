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
    }
}
