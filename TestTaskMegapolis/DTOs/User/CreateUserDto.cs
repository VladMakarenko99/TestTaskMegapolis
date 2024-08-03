namespace TestTaskMegapolis.DTOs.User;

public record CreateUserDto(string FirstName, string LastName, List<int> GroupIds);