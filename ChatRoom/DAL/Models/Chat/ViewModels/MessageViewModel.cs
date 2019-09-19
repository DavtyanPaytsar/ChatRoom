using System;

namespace ChatRoom.DAL.Models.Chat.ViewModels
{
    public class MessageViewModel
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public long RoomId { get; set; }
        public string UserId { get; set; }
    }
}
