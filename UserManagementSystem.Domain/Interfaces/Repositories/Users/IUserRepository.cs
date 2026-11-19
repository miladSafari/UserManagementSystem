using System;
using System.Collections.Generic;
using System.Text;
using UserManagementSystem.Common.Pagination;
using UserManagementSystem.Domain.Common;
using UserManagementSystem.Domain.Dtos;
using UserManagementSystem.Domain.Entities;

namespace UserManagementSystem.Domain.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        public Task<MessageResult<List<User>>> GetAllAsync(PaginationModel paginationModel);
        public Task<MessageResult<User>> GetByIdAsync(UserGetByIdInputDto userGetByIdInputDto);
        public Task<MessageResult<bool>> CreateAsync(UserCreateInputDto userCreateInputDto);
        public Task<MessageResult<bool>> UpdateAsync(UserUpdateInputDto userUpdateInputDto);
        public Task<MessageResult<bool>> DeleteAsync(UserGetByIdInputDto userGetByIdInputDto);
    }
}
