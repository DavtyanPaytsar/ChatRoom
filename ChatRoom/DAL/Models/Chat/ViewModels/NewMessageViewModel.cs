using System.ComponentModel.DataAnnotations;

namespace ChatRoom.DAL.Models.Chat.ViewModels
{
    public class NewMessageViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Content { get; set; }

        [Required]
        public long RoomId { get; set; }
    }
}
