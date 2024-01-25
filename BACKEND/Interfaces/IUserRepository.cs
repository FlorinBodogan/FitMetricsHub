using BACKEND.DTOs.Admin;

namespace BACKEND.Interfaces
{
    public interface IUserRepository
    {
        Task<UsersCountDto> GetNumbersOfUsers();
    }
}