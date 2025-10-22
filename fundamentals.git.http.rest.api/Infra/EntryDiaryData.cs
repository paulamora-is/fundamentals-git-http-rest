using fundamentals.git.http.rest.api.models;

namespace fundamentals.git.http.rest.api.infra;

public class EntryDiaryData : IEntryDiaryData
{
    private static readonly List<EntryModel> _entries = new List<EntryModel>
    {
        new EntryModel { Id = 1, Title = "First Day", Content = "Starting the diary!", DateCreated = DateTime.Now.AddDays(-4) },
        new EntryModel { Id = 2, Title = "Learning day", Content = "Learned about HTTP and REST", DateCreated = DateTime.Now.AddDays(-3) },
        new EntryModel { Id = 3, Title = "Learning day", Content = "I studied layers in .NET", DateCreated = DateTime.Now.AddDays(-3) },
        new EntryModel { Id = 4, Title = "Productive day", Content = "I learned even more about git", DateCreated = DateTime.Now.AddDays(-2) },
        new EntryModel { Id = 5, Title = "Productive day", Content = "I learned even more about github", DateCreated = DateTime.Now.AddDays(-2) },
        new EntryModel { Id = 6, Title = "Testing the complete API", Content = "Adding the first entry via postman", DateCreated = DateTime.Now.AddDays(-1) },
    };
    
    public List<EntryModel> GetAll()
    {
        return _entries;
    }

    public EntryModel GetById(int id)
    {
        return _entries.FirstOrDefault(i => i.Id == id);
    }

    public void Add(EntryModel entryModel)
    {
        if (_entries.Any(i => i.Id == entryModel.Id && entryModel.Id != 0))
        {
            return;
        }

        int nextId = _entries.Count > 0 ? _entries.Max(e => e.Id) + 1 : 1;
        entryModel.Id = nextId;

        _entries.Add(entryModel);
    }

    public bool Delete(EntryModel entryModel)
    {
        return _entries.Remove(entryModel);
    }
}