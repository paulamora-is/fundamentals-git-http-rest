using fundamentals.git.http.rest.api.models;

namespace fundamentals.git.http.rest.api.infra;

public interface IEntryDiaryData
{
    List<EntryModel> GetAll();
    EntryModel GetById(int id);
    void Add(EntryModel entryModel);
    public bool Delete(EntryModel entryModel);
}