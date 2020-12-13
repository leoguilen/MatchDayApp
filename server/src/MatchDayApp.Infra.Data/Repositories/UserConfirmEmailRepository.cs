using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Data.Repositories
{
    public class UserConfirmEmailRepository : IUserConfirmEmailRepository
    {
        private readonly MatchDayAppContext _context;

        public UserConfirmEmailRepository(MatchDayAppContext context) 
            => _context = context;

        public async Task<bool> AddRequestAsync(Guid userId)
        {
            var userConfirmEmail = new UserConfirmEmail
            {
                Id = Guid.NewGuid(),
                RequestedAt = DateTime.Now,
                UserId = userId,
                ConfirmKey = Guid.NewGuid()
            };

            var result = await _context.UserConfirmEmails
                .AddAsync(userConfirmEmail);

            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public async Task<UserConfirmEmail> GetRequestByKeyAsync(Guid key)
        {
            return await _context.UserConfirmEmails
                .SingleOrDefaultAsync(x => x.ConfirmKey == key && x.Confirmed == false);
        }

        public bool UpdateRequest(UserConfirmEmail confirmEmail)
        {
            confirmEmail.Confirmed = true;

            var result = _context.UserConfirmEmails
                .Update(confirmEmail);

            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
