using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly MatchDayAppContext _context;

        public UserRepository(MatchDayAppContext context)
            : base(context) => _context = context;

        public override async Task<IReadOnlyList<User>> ListAllAsync()
        {
            return await Entities
                .Include(u => u.UserTeam.Team)
                .ToListAsync();
        }

        public override async Task<User> GetByIdAsync(Guid id)
        {
            return await Entities
                .Include(u => u.UserTeam.Team)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Entities
                .Include(u => u.UserTeam.Team)
                .SingleOrDefaultAsync(u => u.Email.Contains(email));
        }

        public async Task<bool> AddRequestConfirmEmailAsync(Guid userId)
        {
            var userConfirmEmail = new UserConfirmEmail
            {
                Id = Guid.NewGuid(),
                RequestedAt = DateTime.Now,
                UserId = userId,
                ConfirmKey = Guid.NewGuid()
            };

            var result = await _context.ConfirmEmails
                .AddAsync(userConfirmEmail);

            return result.State == EntityState.Added;
        }
    }
}
