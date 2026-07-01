using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Threading.Channels;

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
        public async Task<IActionResult> GetAll()
        {
            var result = await _notificationService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _notificationService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("receiver/{receiver}")]
        public async Task<IActionResult> GetByReceiver(string receiver)
        {
            var result = await _notificationService.GetByReceiverAsync(receiver);
            if (!result.Any())
                return NotFound();
            return Ok(result);
        }

        [HttpGet("channel/{channel}")]
        public async Task<IActionResult> GetByChannel(NotificationChannel channel)
        {
            var result = await _notificationService.GetByChannelAsync(channel);
            if (!result.Any())
                return NotFound();
            return Ok(result);
        }

        [HttpGet("sendingDate/{date}")]
        public async Task<IActionResult> GetBySendingDate(DateTime dateTime)
        {
            var result = await _notificationService.GetByDateAsync(dateTime.Date);
            if (!result.Any())
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(CreateNotificationDTO dto)
        {
            var result = await _notificationService.CreateAsync(dto);

            return Ok(result);
        }
    }
}
