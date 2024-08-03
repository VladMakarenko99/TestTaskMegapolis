namespace TestTaskMegapolis.Data;

public static class SqlQueries
{
    public const string InsertUser = """
                                     INSERT INTO "Users" ("FirstName", "LastName")
                                     VALUES (@FirstName, @LastName)
                                     RETURNING "Id";
                                     """;

    public const string InsertUserGroup = """
                                          INSERT INTO "UserGroups" ("UserId", "GroupId")
                                          VALUES (@UserId, @GroupId)
                                          """;
    
    public const string InsertGroup = """
                                          INSERT INTO "Groups" ("Name")
                                          VALUES (@Name)
                                          """;

    public const string SelectUserWithTheirGroups = """
                                                    SELECT
                                                        CONCAT(u."FirstName", ' ', u."LastName") AS "UserName",
                                                        STRING_AGG(g."Name", ', ') AS "GroupNames"
                                                    FROM
                                                        "Users" u
                                                    JOIN
                                                        "UserGroups" ug ON u."Id" = ug."UserId"
                                                    JOIN
                                                        "Groups" g ON ug."GroupId" = g."Id"
                                                    GROUP BY
                                                        u."Id", u."FirstName", u."LastName"
                                                    ORDER BY
                                                        u."Id";
                                                    """;

    public const string SelectGroups = """
                                       SELECT * FROM "Groups" ORDER BY "Id";
                                       """;
}