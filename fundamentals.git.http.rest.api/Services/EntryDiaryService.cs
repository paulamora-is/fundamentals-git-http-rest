using fundamentals.git.http.rest.api.infra;
using fundamentals.git.http.rest.api.models;

namespace fundamentals.git.http.rest.api.services;

public class EntryDiaryService : IEntryDiaryService
{
    private readonly IEntryDiaryData _entryDiaryData;

    public EntryDiaryService(IEntryDiaryData entryDiaryData)
    {
        _entryDiaryData = entryDiaryData;
    }

    public EntryModel GetById(int id)
    {
        EntryModel entryById = _entryDiaryData.GetById(id);

        if (entryById is null)
        {
            Console.WriteLine($"Id does not exist: {id}.");
        }

        return entryById;
    }

    public List<EntryModel> GetAll()
    {
        List<EntryModel> entriesList = _entryDiaryData.GetAll();

        if (entriesList.Count == 0)
        {
            Console.WriteLine("There are no entries in the list.");
        }

        return entriesList;
    }

    public void AddEntry(EntryModel entryModel)
    {
        if (entryModel.DateCreated == default)
        {
            entryModel.DateCreated = DateTime.Now;
        }
        
        entryModel.DateUpdate = default;
        _entryDiaryData.Add(entryModel);
    }

    public EntryModel Update(int id, EntryModel entryModel)
    {
        EntryModel entryById = GetById(id);

        if (entryById is null)
        {
            Console.WriteLine($"Id does not exist: {id}.");
            return null;
        }

        entryById.DateUpdate = DateTime.Now;
        entryById.DateCreated = entryById.DateCreated;
        entryById.Content = entryModel.Content;
        entryById.Title = entryModel.Title;
       
        return entryById;
    }

    public bool Delete(int id)
    {
        EntryModel entryModel = GetById(id);
        
        if (entryModel is null)
        {
            Console.WriteLine($"Id does not exist: {entryModel?.Id}.");
            return false;
        }
        
        return _entryDiaryData.Delete(entryModel);
    }
}