using AnimeNotes.Db.Context;
using AnimeNotes.Dtos;
using AnimeNotes.Services.Note;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeNotes.Controllers.Note;

[ApiController]
[Route("[controller]")]
public class NoteController(
    DataContext context,
    INoteService noteService): ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken=default)
    {
        var result = await noteService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNote(
        [FromHeader(Name = "Session")] string token,
        [FromBody]AnimeNoteDto animeNoteDto, 
        CancellationToken cancellationToken=default)
    {
        var session = await context.Sessions
            .FirstOrDefaultAsync(s => s.Token == token, cancellationToken);

        animeNoteDto.UserId = session!.UserId;

        var result = await noteService.CreateAsync(animeNoteDto, cancellationToken);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateNote(
        [FromHeader(Name = "Session")] string token,
        [FromBody] AnimeNoteDto animeNoteDto,
        CancellationToken cancellationToken = default)
    {
        var session = await context.Sessions
            .FirstOrDefaultAsync(s => s.Token == token, cancellationToken);

        animeNoteDto.UserId = session!.UserId;

        var result = await noteService.UpdateAsync(animeNoteDto, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
    Guid id,
    CancellationToken cancellationToken)
    {
        await noteService.DeleteAsync(id, cancellationToken);

        return Ok();
    }
}
