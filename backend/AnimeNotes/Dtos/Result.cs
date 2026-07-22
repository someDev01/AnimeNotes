namespace AnimeNotes.Dtos;

public class Result
{
    public string Error { get; set; }

    public bool IsSuccess { get; set; }

    public string? Data { get; set; }
}
