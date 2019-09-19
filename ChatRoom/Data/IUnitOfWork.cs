using ChatRoom.DAL.Repositories.Interfaces;

namespace ChatRoom.Data
{
    public interface IUnitOfWork
    {
        IMessageRepository Messages { get; }
        IRoomRepository Rooms { get; }

        int SaveChanges();
    }
}
