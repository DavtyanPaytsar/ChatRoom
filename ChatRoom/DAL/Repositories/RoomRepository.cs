using ChatRoom.DAL.Models.Chat;
using ChatRoom.DAL.Repositories.Interfaces;
using ChatRoom.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatRoom.DAL.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public RoomRepository(ApplicationDbContext context) : base(context)
        { }

        public override async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await entities.AsNoTracking().ToListAsync();
        }
    }
}
