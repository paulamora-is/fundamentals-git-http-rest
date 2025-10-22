using fundamentals.git.http.rest.api.models;

namespace fundamentals.git.http.rest.api.services;

public interface IEntryDiaryService
{
    EntryModel GetById(int id);
    List<EntryModel> GetAll();
    void AddEntry(EntryModel entryModel);
    EntryModel Update(int id, EntryModel entryModel);
    bool Delete(int id);
}