using Gerivize.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerivize.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ImportManager _manager;

        public ImportController()
        {
            _manager = new ImportManager();
        }

        [HttpPost("instruments")]
        public string[] ImportInstruments([FromForm] IFormFile file)
        {
            return _manager.ImportInstruments(file);
        }
    }
}
