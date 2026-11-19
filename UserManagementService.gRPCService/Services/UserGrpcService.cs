using Grpc.Core;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UserManagementSystem.Common.Pagination;
using UserManagementSystem.Domain.Interfaces.Services.Users;
using GrpcUserService = UserManagementService.gRPCService.UserService;

namespace UserManagementService.gRPCService.Services
{
    public class UserGrpcService : GrpcUserService.UserServiceBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserGrpcService> _logger;

        private const string DateFormat = "O"; // ISO 8601 RoundTrip

        public UserGrpcService(
            IUserService userService,
            ILogger<UserGrpcService> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public override async Task<UserGetAllOutputDtos> GetAll(
            PaginationModel request,
            ServerCallContext context)
        {
            _logger.LogInformation(
                "gRPC GetAll called. PageNumber={PageNumber}, PageSize={PageSize}",
                request.PageNumber, request.PageSize);

            var pagination = new UserManagementSystem.Common.Pagination.PaginationModel
            {
                PageSize = request.PageSize,
                PageNumber = request.PageNumber
            };

            var users = await _userService.GetAllAsync(pagination);

            var response = new UserGetAllOutputDtos
            {
                Message = users.Message ?? string.Empty,
                Status = Convert.ToInt32(users.Status)
            };

            if (users.Result != null)
            {
                response.Users.AddRange(
                    users.Result.Select(u => new UserDto
                    {
                        Id = u.Id.ToString(),
                        Name = u.Name ?? string.Empty,
                        Family = u.Family ?? string.Empty,
                        NationalCode = u.NationalCode ?? string.Empty,
                        BirthDate = u.BirthDate.ToString(DateFormat, CultureInfo.InvariantCulture)
                    }));
            }

            return response;
        }

        public override async Task<UserGetByIdResponse> GetById(
            UserGetByIdInputDto request,
            ServerCallContext context)
        {
            var userId = ParseGuidOrThrow(request.Id, "user ID");

            _logger.LogInformation("gRPC GetById called. Id={Id}", userId);

            var inputDto = new UserManagementSystem.Domain.Dtos.UserGetByIdInputDto
            {
                Id = userId
            };

            var user = await _userService.GetByIdAsync(inputDto);

            var response = new UserGetByIdResponse
            {
                Message = user.Message ?? string.Empty,
                Status = Convert.ToInt32(user.Status)
            };

            if (user.Result != null)
            {
                response.User = new UserDto
                {
                    Id = user.Result.Id.ToString(),
                    Name = user.Result.Name ?? string.Empty,
                    Family = user.Result.Family ?? string.Empty,
                    NationalCode = user.Result.NationalCode ?? string.Empty,
                    BirthDate = user.Result.BirthDate.ToString(DateFormat, CultureInfo.InvariantCulture)
                };
            }

            return response;
        }

        public override async Task<OperationResponse> Create(
            UserCreateInputDto request,
            ServerCallContext context)
        {
            var birthDate = ParseBirthDateOrThrow(request.BirthDate);

            _logger.LogInformation(
                "gRPC Create called. NationalCode={NationalCode}",
                request.NationalCode);

            var inputDto = new UserManagementSystem.Domain.Dtos.UserCreateInputDto
            {
                Name = request.Name,
                Family = request.Family,
                NationalCode = request.NationalCode,
                BirthDate = birthDate
            };

            var result = await _userService.CreateAsync(inputDto);

            _logger.LogInformation(
                "gRPC Create finished. Status={Status}, Message={Message}",
                result.Status, result.Message);

            return MapToOperationResponse(result);
        }

        public override async Task<OperationResponse> Update(
            UserUpdateInputDto request,
            ServerCallContext context)
        {
            var userId = ParseGuidOrThrow(request.Id, "user ID");
            var birthDate = ParseBirthDateOrThrow(request.BirthDate);

            _logger.LogInformation("gRPC Update called. Id={Id}", userId);

            var inputDto = new UserManagementSystem.Domain.Dtos.UserUpdateInputDto
            {
                Id = userId,
                Name = request.Name,
                Family = request.Family,
                NationalCode = request.NationalCode,
                BirthDate = birthDate
            };

            var result = await _userService.UpdateAsync(inputDto);

            _logger.LogInformation(
                "gRPC Update finished. Status={Status}, Message={Message}",
                result.Status, result.Message);

            return MapToOperationResponse(result);
        }

        public override async Task<OperationResponse> Delete(
            UserGetByIdInputDto request,
            ServerCallContext context)
        {
            var userId = ParseGuidOrThrow(request.Id, "user ID");

            _logger.LogInformation("gRPC Delete called. Id={Id}", userId);

            var inputDto = new UserManagementSystem.Domain.Dtos.UserGetByIdInputDto
            {
                Id = userId
            };

            var result = await _userService.DeleteAsync(inputDto);

            _logger.LogInformation(
                "gRPC Delete finished. Status={Status}, Message={Message}",
                result.Status, result.Message);

            return MapToOperationResponse(result);
        }

        private static Guid ParseGuidOrThrow(string raw, string fieldName)
        {
            if (!Guid.TryParse(raw, out var id))
            {
                throw new RpcException(new Status(
                    StatusCode.InvalidArgument,
                    $"Invalid {fieldName} format: '{raw}'"));
            }
            return id;
        }

        private static DateTime ParseBirthDateOrThrow(string raw)
        {
            if (!DateTime.TryParse(
                    raw,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind,
                    out var birthDate))
            {
                throw new RpcException(new Status(
                    StatusCode.InvalidArgument,
                    $"Invalid BirthDate format: '{raw}'. Expected ISO 8601 (e.g. 1990-01-01T00:00:00.0000000)."));
            }
            return birthDate;
        }

        private static OperationResponse MapToOperationResponse(
            UserManagementSystem.Domain.Common.MessageResult<bool> result)
        {
            return new OperationResponse
            {
                Result = result.Result,
                Message = result.Message ?? string.Empty,
                Status = Convert.ToInt32(result.Status)
            };
        }
    }
}