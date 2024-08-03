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
        using var sqlConnection = dbConnectionFactory.CreateDbConnection();
        
        var insertGroupQuery = InsertGroup;
        
        await sqlConnection.ExecuteAsync(insertGroupQuery, new
        {
            Name = groupDto.Name
        });
    }

    public async Task<List<GetGroupDto>> GetGroups()
    {
        using var sqlConnection = dbConnectionFactory.CreateDbConnection();

        var query = SelectGroups;
        var result = await sqlConnection.QueryAsync<GetGroupDto>(query);

        return result.ToList();
    }
}