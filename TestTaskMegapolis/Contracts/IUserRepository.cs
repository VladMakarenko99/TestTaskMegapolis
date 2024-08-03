using TestTaskMegapolis.DTOs;
using TestTaskMegapolis.DTOs.User;

namespace TestTaskMegapolis.Contracts;

public interface IUserRepository
{
    Task CreateUser(CreateUserDto userDto);

    Task<List<UserGroupsDto>> GetUsersWithGroups();
}