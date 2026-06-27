using Microsoft.AspNetCore.Mvc;

namespace NotificationSystem
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _notificationService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _notificationService.Get(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDTO dto)
        {
            var result = _notificationService.Create(dto);

            return Ok(result);
        }
    }
}
