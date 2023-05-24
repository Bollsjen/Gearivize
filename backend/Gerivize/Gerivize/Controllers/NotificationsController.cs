using Gerivize.Attributes;
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
    public class NotificationsController : ControllerBase
    {
        private NotificationRepository _notificationRepository;
        private NotificationsManager _notificationManager;

        public NotificationsController()
        {
            _notificationRepository = new NotificationRepository();
            _notificationManager = new NotificationsManager();
        }

        // GET: api/<NotificationsController>
        [HttpGet]
        public ActionResult<Notification> Get()
        {
            List<Notification> notifications = _notificationRepository.getAll();
            if(notifications.Count == 0) { NotFound(); }
            return Ok(notifications);
        }

        [HttpGet("by-user")]
        [Authorize]
        public ActionResult<Notification> GetByUser([AuthenticatedUser] User user) 
        {
            List<Notification> list = _notificationRepository.getByUserId(user.Id);
            if(list.Count == 0) { NotFound(); }
            return Ok(list);
        }

        // GET api/<NotificationsController>/5
        [HttpGet("{id}")]
        public ActionResult<Notification> Get(Guid id)
        {
            Notification notification = _notificationRepository.getById(id);
            if(notification == null) { NotFound(); }
            return Ok(notification);
        }

        // POST api/<NotificationsController>
        [Authorize]
        [HttpPost("create")]
        public ActionResult Post()
        {
            _notificationManager.findIstrumenmtsToCreateNotificationsFor();
            return Ok();
        }

        // PUT api/<NotificationsController>/5
        [Authorize]
        [HttpPut]
        public ActionResult Put([FromBody] Notification notification)
        {
            _notificationRepository.updateNotification(notification);
            return Ok();
        }

        // DELETE api/<NotificationsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
