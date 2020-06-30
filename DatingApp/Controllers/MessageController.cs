using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.ActionFilters;
using DatingApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DatingApp.DTOS;
using DatingApp.Models;
using DatingApp.Helpers;
using DatingApp.DTOS.Messages;

namespace DatingApp.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public MessageController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var messageFromRepo = await _repo.GetMessage(id);
            if (messageFromRepo == null)
            {
                return NotFound();

            }
            return Ok(messageFromRepo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreationDto messageForCreationDto)
        {

            var sender = await _repo.GetUser(userId);
            if (sender.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            messageForCreationDto.SenderId = userId;
            var recipientFromRepo = await _repo.GetUser(messageForCreationDto.RecipientId);
            if (recipientFromRepo == null)
            {
                return BadRequest("Recipient User not found");
            }

            var message = _mapper.Map<Message>(messageForCreationDto);
            _repo.Add(message);
            if (await _repo.SaveAll())
            {
                var messageToReturn = _mapper.Map<MessageToReturnDto>(message);

                return CreatedAtRoute("GetMessage", new { userId, Id = message.Id }, messageToReturn);
            }

            return BadRequest("yo necesito cigarrillos");
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userId, [FromQuery]MessageParams messageParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            messageParams.UserId = userId;

            var messagesFormRepo = await _repo.GetMessagesForUser(messageParams);
            var messages = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFormRepo);
            Response.AddPaginationInfo(messagesFormRepo.CurrentPage, messagesFormRepo.PageSize, messagesFormRepo.TotalCount, messagesFormRepo.TotalPages);
            return Ok(messages);



        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessagesThread(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var messagesFromRepo = await _repo.GetMessagesThread(userId, recipientId);

            var messagesThresd = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            return Ok(messagesThresd);


        }

        [HttpPost("{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var messagesFromRepo = await _repo.GetMessage(messageId);
            if (messagesFromRepo.SenderId == userId)
                messagesFromRepo.SenderDeleted = true;

            if (messagesFromRepo.RecipientId == userId)
                messagesFromRepo.RecipientDeleted = true;

            if (messagesFromRepo.RecipientDeleted == true &&  messagesFromRepo.SenderDeleted == true)
            {
                _repo.Delete(messagesFromRepo);
            }

            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Error deleting message");

        }


        [HttpPost("{messageId}/read")]
        public async Task<IActionResult> MarkAsRead(int messageId, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var messagesFromRepo = await _repo.GetMessage(messageId);


            if (messagesFromRepo.RecipientId != userId)
                return Unauthorized();

            messagesFromRepo.IsRead = true;
            messagesFromRepo.DateRead = DateTime.Now;
            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("aa");

        }


    }
   }
