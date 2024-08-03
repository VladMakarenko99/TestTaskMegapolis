namespace TestTaskMegapolis.Entities;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<UserGroup> UserGroups { get; set; }
}