using MatchDayApp.Domain.Common.Helpers;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Entities.Enum;
using MatchDayApp.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MatchDayApp.UnitTest.Configuration
{
    public static class ServicesConfigurationExtensions
    {
        public static MatchDayAppContext SeedFakeUserData(this MatchDayAppContext testContext)
        {
            var salt = SecurePasswordHasher.CreateSalt(8);

            var users = new List<User>
            {
                new User
                {
                    FirstName = "Test",
                    LastName = "One",
                    Username = "test1",
                    Email = "test1@email.com",
                    ConfirmedEmail = true,
                    Password = SecurePasswordHasher.GenerateHash("test123", salt),
                    Salt = salt,
                    UserType = UserType.SoccerCourtOwner
                },
                new User
                {
                    FirstName = "Test",
                    LastName = "Two",
                    Username = "test2",
                    Email = "test2@email.com",
                    ConfirmedEmail = true,
                    Password = SecurePasswordHasher.GenerateHash("test321", salt),
                    Salt = salt,
                    UserType = UserType.TeamOwner
                },

                new User
                {
                    FirstName = "Test",
                    LastName = "Three",
                    Username = "test3",
                    Email = "test3@email.com",
                    ConfirmedEmail = true,
                    Password = SecurePasswordHasher.GenerateHash("test231", salt),
                    Salt = salt,
                    UserType = UserType.SoccerCourtOwner
                }
            };

            testContext.Users.AddRange(users);
            testContext.SaveChanges();

            foreach (var entity in testContext.ChangeTracker.Entries())
                entity.State = EntityState.Detached;

            return testContext;
        }
        public static MatchDayAppContext SeedFakeTeamData(this MatchDayAppContext testContext)
        {
            testContext.SeedFakeUserData();

            var teams = new List<Team>
            {
                new Team
                {
                    Name = "Team 1",
                    Image = "team1.png",
                    TotalPlayers = 15,
                    OwnerUserId = testContext.Users.ToList()[0].Id
                },
                new Team
                {
                    Name = "Team 2",
                    Image = "team2.png",
                    TotalPlayers = 13,
                    OwnerUserId = testContext.Users.ToList()[1].Id
                },
                new Team
                {
                    Name = "Team 3",
                    Image = "team3.png",
                    TotalPlayers = 11,
                    OwnerUserId = testContext.Users.ToList()[2].Id
                }
            };

            testContext.Teams.AddRange(teams);
            testContext.SaveChanges();

            foreach (var entity in testContext.ChangeTracker.Entries())
                entity.State = EntityState.Detached;

            return testContext;
        }
    }
}
