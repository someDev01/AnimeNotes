using AnimeNotes.Db.Context;
using AnimeNotes.Dtos;
using AnimeNotes.Models.Note;
using Microsoft.EntityFrameworkCore;

namespace AnimeNotes.Services.Note;

public class NoteService(DataContext context) : INoteService
{
    public async Task<AnimeNoteDto> CreateAsync(AnimeNoteDto animeNoteDto, CancellationToken cancellationToken)
    {
        var note = new AnimeNote()
        {
            UserId = animeNoteDto.UserId,
            Title = animeNoteDto.Title,
            Hero = animeNoteDto.Hero,
            Source = animeNoteDto.Source,
            WatchedEpisode = animeNoteDto.WatchedEpisode,
            ExpectedEpisode = animeNoteDto.ExpectedEpisode,
            CreatedAtYear = animeNoteDto.CreatedAtYear,
        };

        await context.Notes.AddAsync(note, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return animeNoteDto;
    }

    public async Task<Result> DeleteAsync(Guid noteId, CancellationToken cancellationToken)
    {
        var note = await context.Notes
            .FirstOrDefaultAsync(x => x.Id == noteId, cancellationToken);

        if (note is null)
            return new Result() { Error = "Такой заметки нет" };

        context.Notes.Remove(note);
        await context.SaveChangesAsync(cancellationToken);

        return new Result() { IsSuccess=true};

    }

    public async Task<List<AnimeNoteDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var notes = await context.Notes
            .OrderByDescending(n => n.CreatedAtYear)
            .Select(n => new AnimeNoteDto()
            {
                Id = n.Id,
                UserId = n.UserId,
                Title = n.Title,
                Hero = n.Hero,
                Source = n.Source,
                WatchedEpisode = n.WatchedEpisode,
                ExpectedEpisode = n.ExpectedEpisode,
                CreatedAtYear = n.CreatedAtYear,
            }).ToListAsync(cancellationToken);

        return notes;
    }

    public async Task<AnimeNoteDto> UpdateAsync(AnimeNoteDto animeNoteDto, CancellationToken cancellationToken)
    {
        var note = await context.Notes
            .FirstOrDefaultAsync(x => x.Id == animeNoteDto.Id, cancellationToken);

        if (note is null)
            return new AnimeNoteDto();

        if(animeNoteDto.Title is not null)
            note.Title = animeNoteDto.Title;
        if(animeNoteDto.Hero is not null)
            note.Hero = animeNoteDto.Hero;
        if(animeNoteDto.Source is not null)
            note.Source = animeNoteDto.Source;
        if(animeNoteDto.WatchedEpisode != 0)
            note.WatchedEpisode = animeNoteDto.WatchedEpisode;
        if(animeNoteDto.ExpectedEpisode != 0)
            note.ExpectedEpisode = animeNoteDto.ExpectedEpisode;
        if(animeNoteDto.CreatedAtYear != 0)
            note.CreatedAtYear = animeNoteDto.CreatedAtYear;

        await context.SaveChangesAsync(cancellationToken);

        return animeNoteDto;
    }
}
