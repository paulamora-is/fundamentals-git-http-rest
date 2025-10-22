namespace fundamentals.git.http.rest.api.models;

public class EntryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdate { get; set; }
}