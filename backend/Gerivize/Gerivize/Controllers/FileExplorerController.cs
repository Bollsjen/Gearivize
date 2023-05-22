using Gerivize.Managers;
using Gerivize.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gerivize.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileExplorerController : ControllerBase
    {
        private readonly FileExplorerManager _fileManager;
        private readonly string _rootPath = @"A:\Datamatiker\BollsRoot";

        public FileExplorerController()
        {
            _fileManager = new FileExplorerManager();
        }

        // GET: api/<FileExplorerController>
        [HttpGet]
        public List<DirectoryData> Get()
        {
            List<DirectoryData> files = _fileManager.GetDirectoryTree(_rootPath);
            return files;
        }

        // GET api/<FileExplorerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FileExplorerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FileExplorerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FileExplorerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
