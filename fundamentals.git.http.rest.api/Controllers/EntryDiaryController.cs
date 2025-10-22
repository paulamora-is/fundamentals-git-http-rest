using fundamentals.git.http.rest.api.models;
using fundamentals.git.http.rest.api.services;
using Microsoft.AspNetCore.Mvc;

namespace fundamentals.git.http.rest.api;

[ApiController]
[Route("api/entries")]
public class EntryDiaryController : ControllerBase
{
    private readonly IEntryDiaryService _entryDiaryService;

    public EntryDiaryController(IEntryDiaryService entryDiaryService)
    {
        _entryDiaryService = entryDiaryService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetById(int id)
    {
        EntryModel entryById = _entryDiaryService.GetById(id);
        if (entryById is null)
        {
            return NotFound($"Entry with ID {id} not found.");
        }

        return Ok(entryById);
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string? title)
    {
        List<EntryModel> entriesList = _entryDiaryService.GetAll();

        if (!string.IsNullOrEmpty(title))
        {
           entriesList = entriesList.Where(t => t.Title
                            .Contains(title, StringComparison.OrdinalIgnoreCase))
                            .ToList();
        }

        if (entriesList.Count == 0)
        {
            return NoContent();
        }
        
        return Ok(entriesList);
        
    }

    [HttpPost]
    public IActionResult Post([FromBody] EntryModel entryModel)
    {
        if (entryModel == null)
        {
            return BadRequest("EntryModel cannot be null.");
        }
       
       _entryDiaryService.AddEntry(entryModel);
        return CreatedAtAction(nameof(GetById), new { id = entryModel.Id }, entryModel);
    }

    [HttpPut]
    [Route("{id:int}")]
    public IActionResult Put(int id, [FromBody]EntryModel entryModel)
    {
        if (entryModel == null)
        {
            return BadRequest("EntryModel cannot be null.");
        }

        EntryModel updated = _entryDiaryService.Update(id, entryModel);
        
        if (updated is null)
        {
            return NotFound($"Entry with ID {id} not found.");
        }
        return Ok(updated);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (!_entryDiaryService.Delete(id))
        {
            return NotFound($"Entry with ID {id} not found.");
        }
        return Ok($"Entry {id} deleted.");
    }
}