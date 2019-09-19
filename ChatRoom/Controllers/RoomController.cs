using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChatRoom.DAL.Models.Chat;
using ChatRoom.DAL.Models.Chat.ViewModels;
using ChatRoom.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChatRoom.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        public IUnitOfWork _unitOfWork { get; set; }
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RoomController(
            ILogger<RoomController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RoomViewModel>> GetAll()
        {
            IEnumerable<RoomViewModel> result = null;
            try
            {
                var rooms = await _unitOfWork.Rooms.GetAllAsync();
                result = _mapper.Map<IEnumerable<RoomViewModel>>(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on getting rooms");
            }

            return result;
        }

        [HttpPost]
        public async Task<RoomViewModel> Create([FromBody]NewRoomViewModel newRoom)
        {
            RoomViewModel result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _unitOfWork.Rooms.GetAsync(r => r.Name == newRoom.Name);
                    var room = data.FirstOrDefault();
                    if (room == null)
                    {
                        room = _mapper.Map< Room >(newRoom);
                        room = await _unitOfWork.Rooms.AddAsync(room);
                    }

                    result = _mapper.Map< RoomViewModel >(room);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when creating the room");
            }

            return result;
        }

        [HttpDelete]
        public async Task Delete(int roomId)
        {
            try
            {
                await _unitOfWork.Rooms.DeleteAsync(new Room() { Id = roomId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when deeleting the room");
            }
        }
    }
}