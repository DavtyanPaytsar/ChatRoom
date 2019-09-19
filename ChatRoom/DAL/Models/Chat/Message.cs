using ChatRoom.DAL.Models.Auth;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatRoom.DAL.Models.Chat
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public long RoomId { get; set; }
        public string UserId { get; set; }
        public Room Room { get; set; }
        public ApplicationUser User { get; set; }
    }
}
