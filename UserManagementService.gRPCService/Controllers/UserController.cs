using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Common.Enums;
using UserManagementSystem.Domain.Common;
using UserManagementSystem.Domain.Dtos;
using UserManagementSystem.Domain.Interfaces.Services.Users;

namespace UserManagementService.gRPCService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: api/User/GetAllAsync
        [HttpGet("GetAllAsync")]
        public async Task<MessageResult<List<UserGetAllOutputDto>>> GetAllAsync(
            [FromQuery] PaginationModel paginationModel)
        {
            var pagination = new UserManagementSystem.Common.Pagination.PaginationModel
            {
                PageNumber = paginationModel.PageNumber,
                PageSize = paginationModel.PageSize
            };

            var users = await _userService.GetAllAsync(pagination);

            return new MessageResult<List<UserGetAllOutputDto>>
            {
                Result = users.Result?.Select(u => new UserGetAllOutputDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Family = u.Family,
                    NationalCode = u.NationalCode,
                    BirthDate = u.BirthDate
                }).ToList() ?? new List<UserGetAllOutputDto>(),
                Message = users.Message ?? string.Empty,
                Status = users.Status
            };
        }

        // GET: api/User/GetByIdAsync?id=GUID
        [HttpGet("GetByIdAsync")]
        public async Task<MessageResult<UserGetAllOutputDto>> GetByIdAsync(Guid id)
        {
            var input = new UserManagementSystem.Domain.Dtos.UserGetByIdInputDto { Id = id };

            var user = await _userService.GetByIdAsync(input);

            if (user.Result == null)
            {
                return new MessageResult<UserGetAllOutputDto>
                {
                    Result = null!,
                    Message = user.Message ?? "User not found",
                    Status = user.Status
                };
            }

            return new MessageResult<UserGetAllOutputDto>
            {
                Result = new UserGetAllOutputDto
                {
                    Id = user.Result.Id,
                    Name = user.Result.Name,
                    Family = user.Result.Family,
                    NationalCode = user.Result.NationalCode,
                    BirthDate = user.Result.BirthDate
                },
                Message = user.Message ?? string.Empty,
                Status = user.Status
            };
        }

        // POST: api/User/CreateAsync
        [HttpPost("CreateAsync")]
        public async Task<MessageResult<bool>> CreateAsync(UserManagementSystem.Domain.Dtos.UserCreateInputDto dto)
        {
            _logger.LogInformation("REST Create called. NationalCode={NationalCode}", dto.NationalCode);

            var result = await _userService.CreateAsync(dto);

            return new MessageResult<bool>
            {
                Result = result.Result,
                Message = result.Message ?? string.Empty,
                Status = result.Status
            };
        }

        // POST: api/User/UpdateAsync
        [HttpPost("UpdateAsync")]
        public async Task<MessageResult<bool>> UpdateAsync(UserManagementSystem.Domain.Dtos.UserUpdateInputDto dto)
        {
            var result = await _userService.UpdateAsync(dto);

            return new MessageResult<bool>
            {
                Result = result.Result,
                Message = result.Message ?? string.Empty,
                Status = result.Status
            };
        }

        // POST: api/User/DeleteAsync
        [HttpPost("DeleteAsync")]
        public async Task<MessageResult<bool>> DeleteAsync(UserManagementSystem.Domain.Dtos.UserGetByIdInputDto dto)
        {
            var result = await _userService.DeleteAsync(dto);

            return new MessageResult<bool>
            {
                Result = result.Result,
                Message = result.Message ?? string.Empty,
                Status = result.Status
            };
        }
        [HttpPost("TestGrpcCreate")]
        public async Task<IActionResult> TestGrpcCreate()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using var channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { HttpHandler = handler });
            var client = new UserManagementService.gRPCService.UserService.UserServiceClient(channel);

            var response = await client.CreateAsync(
                new UserManagementService.gRPCService.UserCreateInputDto
                {
                    Name = "Test",
                    Family = "Test",
                    NationalCode = "1234567890",
                    BirthDate = "1990-01-01T00:00:00.0000000"
                });

            return Ok(response);
        }
    }
}