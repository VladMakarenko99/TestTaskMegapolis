using TestTaskMegapolis.Contracts;
using TestTaskMegapolis.Data;
using Dapper;
using TestTaskMegapolis.DTOs.User;
using static TestTaskMegapolis.Data.SqlQueries;

namespace TestTaskMegapolis.Repository;

public class UserRepository(SqlConnectionFactory dbConnectionFactory) : IUserRepository
{
    public async Task CreateUser(CreateUserDto userDto)
    {
        using var sqlConnection = dbConnectionFactory.CreateDbConnection();

        var insertUserQuery = InsertUser;
        
        var userId = await sqlConnection.QuerySingleAsync<int>(insertUserQuery, new
        {
            userDto.FirstName,
            userDto.LastName
        });

        var insertUserGroupQuery = InsertUserGroup;

        foreach (var groupId in userDto.GroupIds)
        {
            await sqlConnection.ExecuteAsync(insertUserGroupQuery, new
            {
                UserId = userId,
                GroupId = groupId
            });
        }
    }

    public async Task<List<UserGroupsDto>> GetUsersWithGroups()
    {
        using var sqlConnection = dbConnectionFactory.CreateDbConnection();

        var query = SelectUserWithTheirGroups;
        
        var result = await sqlConnection.QueryAsync<UserGroupsDto>(query);

        return result.ToList();
    }
}