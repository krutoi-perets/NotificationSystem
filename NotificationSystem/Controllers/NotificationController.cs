using Microsoft.AspNetCore.Mvc;

namespace NotificationSystem
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private List<Notification> _notifications = new List<Notification>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_notifications);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var notification = _notifications.Find(x => x.Id == id);
            if (notification == null)
                return NotFound();
            return Ok(notification);
        }

        [HttpPost]
        public IActionResult CreateNotification(Notification notification)
        {
            notification.Id = _notifications.Count() == 0 ? 1 : _notifications[_notifications.Count() - 1].Id + 1;
            _notifications.Add(notification);

            return Created();
        }
    }
}
