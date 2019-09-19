using AutoMapper;
using ChatRoom.DAL.Models.Chat;
using ChatRoom.DAL.Models.Chat.ViewModels;

namespace ChatRoom.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Room, RoomViewModel>();
            CreateMap<NewRoomViewModel, Room>();

            CreateMap<Message, MessageViewModel>()
                .ForMember(d => d.UserId, map => map.MapFrom(s => s.User.UserName));
            CreateMap<NewMessageViewModel, Message>();
        }
    }
}
