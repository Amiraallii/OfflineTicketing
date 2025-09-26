using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Application.Features.User.Commands.CreateUser;
using OfflineTicketing.Application.Features.User.Commands.DeleteUser;
using OfflineTicketing.Application.Features.User.Commands.UpdateUser;
using OfflineTicketing.Application.Features.User.Queries.GetAllAdmins;
using OfflineTicketing.Application.Features.User.Queries.GetAllUsers;
using OfflineTicketing.Application.Features.User.Queries.GetById;
using OfflineTicketing.Web.ViewModel;

namespace OfflineTicketing.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IMediator mediatR) : ControllerBase
    {
        [HttpGet("GetAllEmployees")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> GetAllEmployees(CancellationToken ct)
        {
            var result = await mediatR.Send(new GetAllEmployeesQuery(), ct);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("GetAllAdmins")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> GetAllAdmins(CancellationToken ct)
        {
            var result = await mediatR.Send(new GetAllAdminsQuery(), ct);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("GetUserById")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken ct)
        {
            var result = await mediatR.Send(new GetUserByIdQuery { Id = id }, ct);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("CreateUser")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var result = await mediatR.Send(new CreateUserCommand
            {
                Email = dto.Email,
                FullName = dto.FullName,
                Password = dto.Password,
                Role = dto.Role,
            });
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPut("UpdateUser")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
        {
            var result = await mediatR.Send(new UpdateUserCommand
            {
                Id = dto.Id,
                Email = dto.Email,
                FullName = dto.FullName,
                Password = dto.Password,
                Role = dto.Role,
            });
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpDelete("DeleteUser")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await mediatR.Send(new DeleteUserCommand { Id = id });
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

    }
}
