using WorkManagerDal.Models;

namespace RiskerWorkManager.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GetEmailFromToken(string token);
        bool ValidateToken(string token);
    }
}
