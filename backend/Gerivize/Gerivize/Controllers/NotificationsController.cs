using Gerivize.Interfaces;
using Gerivize.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gerivize.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private INotificationRepository _notificationRepository;
        private INotificationManager _notificationManager;

        public NotificationsController(INotificationRepository notificationRepository, INotificationManager notificationManager)
        {
            _notificationRepository = notificationRepository;
            _notificationManager = notificationManager;
        }

        // GET: api/<NotificationsController>
        [HttpGet]
        public ActionResult<Notification> Get()
        {
            List<Notification> notifications = _notificationRepository.getAll();
            if(notifications.Count == 0) { NotFound(); }
            return Ok(notifications);
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
        [HttpPost("create/{aNumber}")]
        public ActionResult Post(string aNumber)
        {
            _notificationManager.CreateNotifications(aNumber);
            return Ok();
        }

        // PUT api/<NotificationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotificationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
