using TestTaskMegapolis.DTOs.Group;

namespace TestTaskMegapolis.Contracts;

public interface IGroupRepository
{
    Task CreateGroup(CreateGroupDto groupDto);
}