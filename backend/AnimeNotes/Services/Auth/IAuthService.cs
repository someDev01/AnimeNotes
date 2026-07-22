using AnimeNotes.Dtos;

namespace AnimeNotes.Services.Auth;

public interface IAuthService
{
    Task<Result> Login(string login, string password, CancellationToken cancellationToken);

    Task<Result> Register(string login, string password, CancellationToken cancellationToken);
}
