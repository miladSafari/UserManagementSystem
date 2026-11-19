using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Domain.Common;
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

        [HttpGet("GetAllAsync")]
        public async Task<MessageResult<List<UserManagementSystem.Domain.Dtos.UserGetAllOutputDto>>> GetAllAsync(
            [FromQuery] PaginationModel paginationModel)
        {
            var pagination = new UserManagementSystem.Common.Pagination.PaginationModel
            {
                PageNumber = paginationModel.PageNumber,
                PageSize = paginationModel.PageSize
            };

            var users = await _userService.GetAllAsync(pagination);

            return new MessageResult<List<UserManagementSystem.Domain.Dtos.UserGetAllOutputDto>>
            {
                Result = users.Result?.Select(u => new UserManagementSystem.Domain.Dtos.UserGetAllOutputDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Family = u.Family,
                    NationalCode = u.NationalCode,
                    BirthDate = u.BirthDate
                }).ToList() ?? new List<UserManagementSystem.Domain.Dtos.UserGetAllOutputDto>(),
                Message = users.Message ?? string.Empty,
                Status = users.Status
            };
        }

        [HttpGet("GetByIdAsync")]
        public async Task<MessageResult<UserManagementSystem.Domain.Dtos.UserGetAllOutputDto>> GetByIdAsync(Guid id)
        {
            var input = new UserManagementSystem.Domain.Dtos.UserGetByIdInputDto { Id = id };

            var user = await _userService.GetByIdAsync(input);

            if (user.Result == null)
            {
                return new MessageResult<UserManagementSystem.Domain.Dtos.UserGetAllOutputDto>
                {
                    Result = null!,
                    Message = user.Message ?? "User not found",
                    Status = user.Status
                };
            }

            return new MessageResult<UserManagementSystem.Domain.Dtos.UserGetAllOutputDto>
            {
                Result = new UserManagementSystem.Domain.Dtos.UserGetAllOutputDto
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
        [HttpPost("GrpcGetAll")]
        public async Task<IActionResult> GrpcGetAll(PaginationModel inputDto)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using var channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { HttpHandler = handler });
            var client = new UserManagementService.gRPCService.UserService.UserServiceClient(channel);

            var response = await client.GetAllAsync(
                new PaginationModel
                {
                    PageNumber = inputDto.PageNumber,
                    PageSize = inputDto.PageSize
                });

            return Ok(response);
        }
        [HttpPost("GrpcGetById")]
        public async Task<IActionResult> GrpcGetById(UserManagementService.gRPCService.UserGetByIdInputDto inputDto)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using var channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { HttpHandler = handler });
            var client = new UserManagementService.gRPCService.UserService.UserServiceClient(channel);

            var response = await client.GetByIdAsync(
                new UserGetByIdInputDto
                {
                    Id = inputDto.Id
                });

            return Ok(response);
        }
        [HttpPost("GrpcCreate")]
        public async Task<IActionResult> GrpcCreate(UserManagementService.gRPCService.UserCreateInputDto inputDto)
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
                new UserCreateInputDto
                {
                    Name = inputDto.Name,
                    Family = inputDto.Family,
                    NationalCode = inputDto.NationalCode,
                    BirthDate = inputDto.BirthDate
                });

            return Ok(response);
        }
        [HttpPost("GrpcUpdate")]
        public async Task<IActionResult> GrpcUpdate(UserManagementService.gRPCService.UserUpdateInputDto inputDto)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using var channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { HttpHandler = handler });
            var client = new UserManagementService.gRPCService.UserService.UserServiceClient(channel);

            var response = await client.UpdateAsync(
                new UserUpdateInputDto
                {
                    Id = inputDto.Id,
                    Name = inputDto.Name,
                    Family = inputDto.Family,
                    NationalCode = inputDto.NationalCode,
                    BirthDate = inputDto.BirthDate
                });

            return Ok(response);
        }
        [HttpPost("GrpcDelete")]
        public async Task<IActionResult> GrpcDelete(UserManagementService.gRPCService.UserGetByIdInputDto inputDto)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using var channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { HttpHandler = handler });
            var client = new UserManagementService.gRPCService.UserService.UserServiceClient(channel);

            var response = await client.DeleteAsync(
                new UserManagementService.gRPCService.UserGetByIdInputDto
                {
                    Id = inputDto.Id
                });

            return Ok(response);
        }
    }
}