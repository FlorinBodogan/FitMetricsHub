using AutoMapper;
using BACKEND.DTOs.Admin;
using BACKEND.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<int> GetUsersCountForPeriod(int numberOfDays)
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-numberOfDays);
            DateTime currentDate = DateTime.UtcNow;

            var usersCount = await this.context.Users
                .Where(user => user.Created >= startDate && user.Created <= currentDate)
                .CountAsync();

            return usersCount;
        }

        public async Task<UsersCountDto> GetNumbersOfUsers()
        {
            DateTime currentDate = DateTime.UtcNow;

            var usersLastDay = await GetUsersCountForPeriod(1);
            var usersLastWeek = await GetUsersCountForPeriod(7);
            var usersLastMonth = await GetUsersCountForPeriod(30);
            var usersLast3Months = await GetUsersCountForPeriod(90);
            var usersLast6Months = await GetUsersCountForPeriod(180);
            var usersLastYear = await GetUsersCountForPeriod(365);

            return new UsersCountDto
            {
                UsersLastDay = usersLastDay,
                UsersLastWeek = usersLastWeek,
                UsersLastMonth = usersLastMonth,
                UsersLast3Months = usersLast3Months,
                UsersLast6Months = usersLast6Months,
                UsersLastYear = usersLastYear
            };
        }
    }
}