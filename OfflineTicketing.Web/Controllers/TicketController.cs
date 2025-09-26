using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfflineTicketing.Application.Features.Ticket.Commands.CreateTicket;
using OfflineTicketing.Application.Features.Ticket.Commands.DeleteTicket;
using OfflineTicketing.Application.Features.Ticket.Commands.UpdateStatus;
using OfflineTicketing.Application.Features.Ticket.Commands.UpdateTicket;
using OfflineTicketing.Application.Features.Ticket.Queries.GetAllTicket;
using OfflineTicketing.Application.Features.Ticket.Queries.GetAllTicketByAsignedUserId;
using OfflineTicketing.Application.Features.Ticket.Queries.GetAllTicketByCreatedUserId;
using OfflineTicketing.Application.Features.Ticket.Queries.GetTicketById;
using OfflineTicketing.Application.Features.Ticket.Queries.GetTicketsByUserId;
using OfflineTicketing.Application.Features.User.Queries.GetAllAdmins;
using OfflineTicketing.Application.Features.User.Queries.GetAllUsers;
using OfflineTicketing.Application.Features.User.Queries.GetById;
using OfflineTicketing.Core.Enums;
using OfflineTicketing.Web.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OfflineTicketing.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController(IMediator mediatR) : ControllerBase
    {
        [HttpPost("CreateTicket")]
        [Authorize("Employee")]
        public async Task<IActionResult> CreateTicket(CreateTicketDto dto) 
        {
            var result = await mediatR.Send(new CreateTicketCommand
            {
                AssignedToUserId = dto.AssignedToUserId,
                CreatedByUserId = CurrentUser,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = dto.Status,
                Title = dto.Title
            });
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPut("UpdateTicket")]
        [Authorize("Administrator")]
        public async Task<IActionResult> UpdateTicket(UpdateTicketDto dto)
        {
            var result = await mediatR.Send(new UpdateTicketCommand
            {
                Id = dto.Id,
                AssignedTo = dto.AssignedToUserId,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = dto.Status,
                Title = dto.Title,
                
                UpdateAt = DateTime.UtcNow,
            });
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPut("UpdateTicketStatus")]
        [Authorize("Administrator")]
        public async Task<IActionResult> UpdateTicketStatus(UpdateTicketStatusDto dto)
        {
            var result = await mediatR.Send(new UpdateStatusCommand
            {
                TicketId = dto.TicketId,
                Status = dto.Status
            });
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpDelete("DeleteTicket")]
        [Authorize("Administrator")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            var result = await mediatR.Send(new DeleteTicketCommand
            {
                Id = id
            });
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("GetAllTickets")]
        [Authorize("Administrator")]
        public async Task<IActionResult> GetAllTickets([FromQuery] Paginations paginations, CancellationToken ct)
        {
            var result = await mediatR.Send(new GetAllTicketsQuery { Count = paginations.Count, Page = paginations.Page}, ct);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("GetAllTicketByAsignedUserId")]
        [Authorize("Administrator")]
        public async Task<IActionResult> GetAllTicketByAsignedUserId(Guid asignedToId, CancellationToken ct)
        {
            var result = await mediatR.Send(new GetAllTicketByAsignedUserIdQuery { AsignedToId = asignedToId}, ct);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("GetAllTicketByCreatedUserId")]
        [Authorize("Administrator")]
        public async Task<IActionResult> GetAllTicketByCreatedUserId(Guid createdUserId, CancellationToken ct)
        {
            var result = await mediatR.Send(new GetAllTicketByCreatedUserIdQuery { CreatedUserId = createdUserId }, ct);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("GetTicketById")]
        [Authorize("Administrator")]
        public async Task<IActionResult> GetTicketById(Guid id, CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var guid = Guid.Parse(userId);
            var result = await mediatR.Send(new GetTicketByIdQuery { Id = id, CurrentUserId = guid }, ct);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("my")]
        [Authorize("Employee")]
        public async Task<IActionResult> GetMyTickets(CancellationToken ct)
        {
            
            var result = await mediatR.Send(new GetTicketsByUserIdQuery { UserId = CurrentUser }, ct);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        

        private Guid CurrentUser
        {
            get
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId is null) return new();

                return Guid.Parse(userId);
            }
        }
    }
}
