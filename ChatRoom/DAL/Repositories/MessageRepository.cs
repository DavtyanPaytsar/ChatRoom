using ChatRoom.DAL.Models.Chat;
using ChatRoom.DAL.Repositories.Interfaces;
using ChatRoom.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChatRoom.DAL.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public MessageRepository(ApplicationDbContext context) : base(context)
        { }


        public override async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await entities.Include(m => m.Room).
                AsNoTracking().
                ToListAsync();
        }


        public override async Task<IEnumerable<Message>> GetAsync(Expression<Func<Message, bool>> predicate)
        {
            return await entities.Where(predicate).
                Include(m => m.Room).
                Include(m => m.User).
                AsNoTracking().
                ToListAsync();
        }
    }
}
