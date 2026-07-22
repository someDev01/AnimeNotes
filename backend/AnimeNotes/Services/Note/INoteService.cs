using AnimeNotes.Dtos;

namespace AnimeNotes.Services.Note;

public interface INoteService
{
    Task<List<AnimeNoteDto>> GetAllAsync(CancellationToken cancellationToken);

    Task<AnimeNoteDto> CreateAsync(AnimeNoteDto animeNoteDto, CancellationToken cancellationToken);

    Task<AnimeNoteDto> UpdateAsync(AnimeNoteDto animeNoteDto, CancellationToken cancellationToken);

    Task<Result> DeleteAsync(Guid noteId, CancellationToken cancellationToken);
}
