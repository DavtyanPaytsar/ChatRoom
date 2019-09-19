using ChatRoom.DAL.Models.Chat;
using System.Linq;

namespace ChatRoom.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Rooms.Any())
            {
                var rooms = new Room[ ]
                {
                  new Room(){ Name="Master Room" },
                  new Room(){ Name="Test Room" }
                };

                foreach (var room in rooms)
                {
                    context.Rooms.Add(room);
                }
                context.SaveChanges();
            }
        }
    }
}