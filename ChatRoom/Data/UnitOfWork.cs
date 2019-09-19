using ChatRoom.DAL.Repositories;
using ChatRoom.DAL.Repositories.Interfaces;

namespace ChatRoom.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;

        IMessageRepository _messages;
        IRoomRepository _rooms;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IMessageRepository Messages
        {
            get
            {
                if (_messages == null)
                    _messages = new MessageRepository(_context);

                return _messages;
            }
        }

        public IRoomRepository Rooms
        {
            get
            {
                if (_rooms == null)
                    _rooms = new RoomRepository(_context);

                return _rooms;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
