using BACKEND.Entities;

namespace BACKEND.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}