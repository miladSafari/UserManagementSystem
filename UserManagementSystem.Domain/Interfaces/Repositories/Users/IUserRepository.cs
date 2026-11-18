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
        public Task<MessageResult<List<User>>> GetAll(PaginationModel paginationModel);
        public Task<MessageResult<User>> GetById(UserGetByIdInputDto userGetByIdInputDto);
        public Task<MessageResult<bool>> Create(UserCreateInputDto userCreateInputDto);
        public Task<MessageResult<bool>> Update(UserUpdateInputDto userUpdateInputDto);
        public Task<MessageResult<bool>> Delete(UserGetByIdInputDto userGetByIdInputDto);
    }
}
