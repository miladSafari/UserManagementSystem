using System;
using System.Collections.Generic;
using System.Text;
using UserManagementSystem.Common.Pagination;
using UserManagementSystem.Domain.Common;
using UserManagementSystem.Domain.Dtos;
using UserManagementSystem.Domain.Interfaces.Repositories.Users;
using UserManagementSystem.Domain.Interfaces.Services.Users;

namespace UserManagementSystem.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<MessageResult<List<UserGetAllOutputDto>>> GetAllAsync(PaginationModel paginationModel)
        {
            var finalResult = new MessageResult<List<UserGetAllOutputDto>>();
            var users = await _repository.GetAllAsync(paginationModel);

            var resultOfUser = users.Result.Select(u => new UserGetAllOutputDto()
            {
                Id = u.Id,
                Name = u.Name,
                Family = u.Family,
                BirthDate = u.BirthDate,
                NationalCode = u.NationalCode
            }).ToList();

            finalResult.Result = resultOfUser;
            finalResult.Message = users.Message;
            finalResult.Status = users.Status;

            return finalResult;
        }

        public async Task<MessageResult<UserGetAllOutputDto>> GetByIdAsync(UserGetByIdInputDto userGetByIdInputDto)
        {
            var finalResult = new MessageResult<UserGetAllOutputDto>();
            var user = await _repository.GetByIdAsync(userGetByIdInputDto);

            var resultOfUser = new UserGetAllOutputDto
            {
                Id = user.Result.Id,
                Name = user.Result.Name,
                Family = user.Result.Family,
                BirthDate = user.Result.BirthDate,
                NationalCode = user.Result.NationalCode
            };

            finalResult.Result = resultOfUser;
            finalResult.Message = user.Message;
            finalResult.Status = user.Status;

            return finalResult;
        }
        public async Task<MessageResult<bool>> CreateAsync(UserCreateInputDto userCreateInputDto)
        {
            return await _repository.CreateAsync(userCreateInputDto);
        }

        public async Task<MessageResult<bool>> UpdateAsync(UserUpdateInputDto userUpdateInputDto)
        {
            return await _repository.UpdateAsync(userUpdateInputDto);
        }

        public async Task<MessageResult<bool>> DeleteAsync(UserGetByIdInputDto userGetByIdInputDto)
        {
            return await _repository.DeleteAsync(userGetByIdInputDto);
        }

    }
}
