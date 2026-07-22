namespace AnimeNotes.Dtos;

public class AnimeNoteDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; }

    public string Hero { get; set; }

    public string Source { get; set; }

    public int WatchedEpisode { get; set; }

    public int ExpectedEpisode { get; set; }

    public int CreatedAtYear { get; set; }
}
