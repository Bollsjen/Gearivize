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
            List<DirectoryData> files = new List<DirectoryData>() { 
                new DirectoryData(){ 
                    DirectoryName = _rootPath,
                    Directories =  _fileManager.GetDirectoryTree(_rootPath),
                    Files = new List<FileData>(),
                    Size = 0,
                    LastModified = new DirectoryInfo(_rootPath).LastWriteTime,
        }
            };
            files[0].Files = _fileManager.GetFilesInDirectory(new DirectoryInfo(_rootPath));

            foreach (var fileNode in files[0].Files)
            {
                files[0].Size += fileNode.Size;
            }

            foreach (var subDirectoryNode in files[0].Directories)
            {
                files[0].Size += subDirectoryNode.Size;
            }

            return files;
        }

        // GET api/<FileExplorerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FileExplorerController>
        [HttpPost("file")]
        public ActionResult Post([FromForm] FileExplorerFileUpload file)
        {
            bool success = _fileManager.UploadFile(file);
            if (success) { return Ok(); }
            return StatusCode(500);
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
