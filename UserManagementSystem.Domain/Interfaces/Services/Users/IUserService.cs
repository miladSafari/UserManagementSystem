using System;
using System.Collections.Generic;
using System.Text;
using UserManagementSystem.Common.Pagination;
using UserManagementSystem.Domain.Common;
using UserManagementSystem.Domain.Dtos;
using UserManagementSystem.Domain.Entities;

namespace UserManagementSystem.Domain.Interfaces.Services.Users
{
    public interface IUserService
    {
        public Task<MessageResult<List<UserGetAllOutputDto>>> GetAllAsync(PaginationModel paginationModel);
        public Task<MessageResult<UserGetAllOutputDto>> GetByIdAsync(UserGetByIdInputDto userGetByIdInputDto);
        public Task<MessageResult<bool>> CreateAsync(UserCreateInputDto userCreateInputDto);
        public Task<MessageResult<bool>> UpdateAsync(UserUpdateInputDto userUpdateInputDto);
        public Task<MessageResult<bool>> DeleteAsync(UserGetByIdInputDto userGetByIdInputDto);
    }
}
