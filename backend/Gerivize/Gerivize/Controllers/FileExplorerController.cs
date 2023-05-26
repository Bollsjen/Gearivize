using Gerivize.Attributes;
using Gerivize.Managers;
using Gerivize.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gerivize.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileExplorerController : ControllerBase
    {
        private readonly FileExplorerManager _fileManager;
        public static readonly string _rootPath = @"A:\Datamatiker\BollsRoot";

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

        // POST api/<FileExplorerController>
        [HttpPost("file")]
        [Authorize]
        public ActionResult Post([FromForm] FileExplorerFileUpload file, [AuthenticatedUser] User user)
        {
            if(!user.Responsible) return Unauthorized();
            if (_fileManager.UploadFile(file)) { return Ok(); }
            return StatusCode(500);
        }

        // PUT api/<FileExplorerController>/5
        [HttpPut("move/file")]
        [Authorize]
        public ActionResult Put(int id, [FromBody] FileExplorerMoveFile move, [AuthenticatedUser] User user)
        {
            if(!user.Responsible) return Unauthorized();
            if(_fileManager.MoveFile(move)) return Ok();
            return StatusCode(500);
        }

        // DELETE api/<FileExplorerController>/5
        [HttpDelete("file-dir")]
        [Authorize]
        public ActionResult Delete([FromForm] FileExplorerDeleteFileOrDirectory dataToSend, [AuthenticatedUser] User user)
        {
            if(!user.Responsible) return Unauthorized();
            if (_fileManager.DeleteFileOrDirectory(dataToSend.Name, dataToSend.Path.ToList(), dataToSend.Type)) return Ok();
            return StatusCode(500);
        }
    }
}
