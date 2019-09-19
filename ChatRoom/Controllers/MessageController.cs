using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChatRoom.DAL.Models.Auth;
using ChatRoom.DAL.Models.Chat;
using ChatRoom.DAL.Models.Chat.ViewModels;
using ChatRoom.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChatRoom.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        public IUnitOfWork _unitOfWork { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public MessageController(
            UserManager<ApplicationUser> userManager,
            ILogger<MessageController> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<MessageViewModel>> GetByRoomId(int roomId)
        {
            IEnumerable<MessageViewModel> result = null;

            try
            {
                var messages = await _unitOfWork.Messages.GetAsync(m => m.RoomId == roomId);
                result = _mapper.Map<IEnumerable<MessageViewModel>>(messages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting messages by roomId");
            }

            return result;
        }


        [HttpPost]
        public async Task<MessageViewModel> Create([FromBody]NewMessageViewModel newMessage)
        {
            MessageViewModel result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    var message = _mapper.Map<Message>(newMessage);
                    message.UserId = _userManager.GetUserId(User);
                    message.Date = DateTime.Now;

                    message = await _unitOfWork.Messages.AddAsync(message);

                    result = _mapper.Map<MessageViewModel>(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when adding message");
            }

            return result;
        }

    }
}