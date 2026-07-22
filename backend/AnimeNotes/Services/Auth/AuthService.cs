using AnimeNotes.Db.Context;
using AnimeNotes.Dtos;
using AnimeNotes.Models.Session;
using AnimeNotes.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AnimeNotes.Services.Auth;

public class AuthService(DataContext context) : IAuthService
{
    public async Task<Result> Login(string login, string password, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Where(u => u.Login == login)
            .FirstOrDefaultAsync(cancellationToken);

        if(user is null)
        {
            return new Result() { Error = "Пользователь не найден", IsSuccess = false};
        }

        var hasher = new PasswordHasher<User>();
        var verify = hasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            password);

        if(verify == PasswordVerificationResult.Failed)
        {
            return new Result() { Error = "Пароль неверный", IsSuccess = false};
        }

        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));

        var session = new Session()
        {
            UserId = user.Id,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddYears(1)
        };

        await context.Sessions.AddAsync(session, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return new Result() { IsSuccess = true, Data = token};
    }

    public async Task<Result> Register(string login, string password, CancellationToken cancellationToken)
    {
        var existingUser = await context.Users
            .AnyAsync(u => u.Login == login, cancellationToken);

        if (existingUser)
        {
            return new Result() { Error = "Такой логин уже есть", IsSuccess = false};
        }

        var user = new User();
        var hasher = new PasswordHasher<User>();
        var hash = hasher.HashPassword(user, password);

        user.Login = login;
        user.PasswordHash = hash;

        await context.Users.AddAsync(user, cancellationToken); 
        await context.SaveChangesAsync(cancellationToken);

        return new Result() {IsSuccess = true};
    }
}
