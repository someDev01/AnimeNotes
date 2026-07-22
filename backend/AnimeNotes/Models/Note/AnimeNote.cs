namespace AnimeNotes.Models.Note;

public class AnimeNote
{
    public Guid Id { get; set; } = Guid.NewGuid();   

    public Guid UserId { get; set; }

    public string Title { get; set; }

    public string Hero {  get; set; }

    public string Source { get; set; }

    public int WatchedEpisode {  get; set; }

    public int ExpectedEpisode { get; set; }

    public int CreatedAtYear { get; set; }
}
