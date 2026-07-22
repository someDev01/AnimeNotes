namespace AnimeNotes.Models.User;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Login { get; set; }

    public string PasswordHash { get; set; }  

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
