using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagementSystem.Common.Enums;
using UserManagementSystem.Common.Logging;
using UserManagementSystem.Common.Pagination;
using UserManagementSystem.Domain.Common;
using UserManagementSystem.Domain.Dtos;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Domain.Interfaces.Repositories.Users;
using UserManagementSystem.Infrastructure.Data;

namespace UserManagementSystem.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementContext _context;
        public UserRepository(UserManagementContext context)
        {
            _context = context;
        }

        public async Task<MessageResult<List<User>>> GetAllAsync(PaginationModel paginationModel)
        {
            try
            {
                var users = await _context.Users.Where(u=>u.IsDeleted == false)
                .Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize)
                .Take(paginationModel.PageSize).ToListAsync();

                var result = new MessageResult<List<User>>
                {
                    Result = users,
                    Message = "لیست کاربران با موفقیت فراخوانی شد",
                    Status = StatusResult.Success
                };

                return result;
            }
            catch (Exception ex)
            {
                LogIntoFile.LogIntoFileMethos("GetAll User Has Error : " + ex.Message);

                var result = new MessageResult<List<User>>
                {
                    Result = null,
                    Message = "فراخوانی لیست کاربران با خطا مواجه شد",
                    Status = StatusResult.Error
                };
                return result;
            }

        }
        public async Task<MessageResult<User>> GetByIdAsync(UserGetByIdInputDto userGetByIdInputDto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userGetByIdInputDto.Id);

                var result = new MessageResult<User>
                {
                    Result = user,
                    Message = "کاربر با موفقیت فراخوانی شد",
                    Status = StatusResult.Success
                };

                return result;
            }
            catch (Exception ex)
            {
                LogIntoFile.LogIntoFileMethos($"Get User By Id With Name :{userGetByIdInputDto.Id} Has Error : " + ex.Message);

                var result = new MessageResult<User>
                {
                    Result = null,
                    Message = "فراخوانی کاربر مورد نظر با خطا مواجه شد",
                    Status = StatusResult.Error
                };
                return result;
            }

        }
        public async Task<MessageResult<bool>> CreateAsync(UserCreateInputDto userCreateInputDto)
        {
            try
            {
                var user = new User(Guid.NewGuid(),
               userCreateInputDto.Name,
               userCreateInputDto.Family,
               userCreateInputDto.NationalCode,
               userCreateInputDto.BirthDate);

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return new MessageResult<bool>
                {
                    Result = true,
                    Message = "کاربر با موفقیت ایجاد گردید.",
                    Status = StatusResult.Success
                };
            }
            catch (Exception ex)
            {
                LogIntoFile.LogIntoFileMethos($"Update User With Name :{userCreateInputDto.Name} Has Error : " + ex.Message);

                return new MessageResult<bool>
                {
                    Result = false,
                    Message = "ایجاد کاربر با خطا مواجه شد.",
                    Status = StatusResult.Error
                };
            }

        }
        public async Task<MessageResult<bool>> UpdateAsync(UserUpdateInputDto userUpdateInputDto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userUpdateInputDto.Id);
                if (user is not null)
                {
                    user.ChangeName(userUpdateInputDto.Name);
                    user.ChangeFamily(userUpdateInputDto.Family);
                    user.ChangeNationalCode(userUpdateInputDto.NationalCode);
                    user.ChangeBirthDate(userUpdateInputDto.BirthDate);

                    await _context.SaveChangesAsync();

                    return new MessageResult<bool>
                    {
                        Result = true,
                        Message = "کاربر با موفقیت ایجاد گردید.",
                        Status = StatusResult.Success
                    };
                }
                else
                {
                    return new MessageResult<bool>
                    {
                        Result = false,
                        Message = "کاربر یافت نشد.",
                        Status = StatusResult.Error
                    };
                }

            }
            catch (Exception ex)
            {
                LogIntoFile.LogIntoFileMethos($"Update User With Id :{userUpdateInputDto.Id} Has Error : " + ex.Message);


                return new MessageResult<bool>
                {
                    Result = false,
                    Message = "ویرایش کاربر با خطا مواجه شد.",
                    Status = StatusResult.Error
                };
            }
        }
        public async Task<MessageResult<bool>> DeleteAsync(UserGetByIdInputDto userGetByIdInputDto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userGetByIdInputDto.Id);

                if (user is not null)
                {
                    user.DeleteUser();

                    await _context.SaveChangesAsync();

                    return new MessageResult<bool>
                    {
                        Result = true,
                        Message = "کاربر با موفقیت حذف شد.",
                        Status = StatusResult.Success
                    };
                }
                else
                {
                    return new MessageResult<bool>
                    {
                        Result = false,
                        Message = "کاربر یافت نشد.",
                        Status = StatusResult.Error
                    };
                }
            }
            catch (Exception ex)
            {
                LogIntoFile.LogIntoFileMethos($"Delete User With Id :{userGetByIdInputDto.Id} Has Error : " + ex.Message);

                return new MessageResult<bool>
                {
                    Result = false,
                    Message = "حذف کاربر با خطا مواجه شد.",
                    Status = StatusResult.Error
                };
            }

        }
    }
}
