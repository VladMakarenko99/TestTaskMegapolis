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
        await using var sqlConnection = dbConnectionFactory.CreateDbConnection();
        await sqlConnection.OpenAsync();
        await using var transaction = await sqlConnection.BeginTransactionAsync();
        
        try
        {
            var userId = await sqlConnection.QuerySingleAsync<int>(InsertUser, new
            {
                userDto.FirstName,
                userDto.LastName
            }, transaction);
            
            foreach (var groupId in userDto.GroupIds)
            {
                await sqlConnection.ExecuteAsync(InsertUserGroup, new
                {
                    UserId = userId,
                    GroupId = groupId
                }, transaction);
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<UserGroupsDto>> GetUsersWithGroups()
    {
        await using var sqlConnection = dbConnectionFactory.CreateDbConnection();

        var result = await sqlConnection.QueryAsync<UserGroupsDto>(SelectUserWithTheirGroups);

        return result.ToList();
    }
}