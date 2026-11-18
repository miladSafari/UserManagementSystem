using System;
using System.Collections.Generic;
using System.Text;
using UserManagementSystem.Domain.Common;
using UserManagementSystem.Domain.Dtos;

namespace UserManagementSystem.Domain.Interfaces.Services.Users
{
    public interface IUserService
    {
        public Task<MessageResult<List<UserGetAllOutputDto>>> GetAll();
        public Task<MessageResult<UserGetAllOutputDto>> GetById(UserGetByIdInputDto userGetByIdInputDto);
        public Task<MessageResult<bool>> Create(UserCreateInputDto userCreateInputDto);
        public Task<MessageResult<bool>> Update(UserUpdateInputDto userUpdateInputDto);
        public Task<MessageResult<bool>> Delete(UserGetByIdInputDto userGetByIdInputDto);
    }
}
