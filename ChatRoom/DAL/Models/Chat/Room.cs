using System.ComponentModel.DataAnnotations.Schema;

namespace ChatRoom.DAL.Models.Chat
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
