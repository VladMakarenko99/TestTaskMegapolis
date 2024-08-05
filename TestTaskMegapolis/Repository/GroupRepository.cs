using Dapper;
using TestTaskMegapolis.Contracts;
using TestTaskMegapolis.Data;
using TestTaskMegapolis.DTOs.Group;
using static TestTaskMegapolis.Data.SqlQueries;

namespace TestTaskMegapolis.Repository;

public class GroupRepository(SqlConnectionFactory dbConnectionFactory) : IGroupRepository
{
    public async Task CreateGroup(CreateGroupDto groupDto)
    {
        await using var sqlConnection = dbConnectionFactory.CreateDbConnection();

        await sqlConnection.ExecuteAsync(InsertGroup, new
        {
            Name = groupDto.Name
        });
    }

    public async Task<List<GetGroupDto>> GetGroups()
    {
        await using var sqlConnection = dbConnectionFactory.CreateDbConnection();

        var result = await sqlConnection.QueryAsync<GetGroupDto>(SelectGroups);

        return result.ToList();
    }
}