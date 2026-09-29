namespace Identity.Applications.Services;

public interface IPasswordHasher
{
    string HashPassword(string password); //for reg
    bool VerifyHashedPassword(string hashedPassword, string providedPassword); //for lg in
}