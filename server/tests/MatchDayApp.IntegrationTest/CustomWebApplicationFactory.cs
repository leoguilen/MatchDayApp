using MatchDayApp.Api;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Domain.Common.Helpers;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Entities.Enum;
using MatchDayApp.Infra.CrossCutting.InversionOfControl;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchDayApp.IntegrationTest
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseEnvironment("Development")
                .ConfigureServices(services =>
                {
                    var configuration = services.BuildServiceProvider()
                        .GetRequiredService<IConfiguration>();

                    var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<MatchDayAppContext>));

                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSqlServerDependency(configuration: null, isTest: true);

                    var providerDbContext = services.BuildServiceProvider()
                        .GetRequiredService<MatchDayAppContext>();
                    providerDbContext.SeedFakeData();

                    services.AddJwtDependency(configuration);
                    services.AddSqlServerRepositoryDependency();
                    services.AddServiceDependency(configuration);
                    services.AddSingleton<IUnitOfWork>(new UnitOfWork(providerDbContext));
                });
        }
    }

    public static class CustomWebApplicationFactoryExtensions
    {
        public static MatchDayAppContext SeedFakeData(this MatchDayAppContext testContext)
        {
            #region User

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
                    UserType = UserType.SoccerCourtOwner,
                    Deleted = true
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

            #endregion

            #region Team

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

            users[0].UserTeam = new UserTeam { UserId = users[0].Id, TeamId = teams[0].Id, Accepted = true };
            users[1].UserTeam = new UserTeam { UserId = users[1].Id, TeamId = teams[1].Id, Accepted = true };
            users[2].UserTeam = new UserTeam { UserId = users[2].Id, TeamId = teams[2].Id, Accepted = true };

            testContext.Users.UpdateRange(users);
            testContext.Teams.AddRange(teams);
            testContext.SaveChanges();

            #endregion

            #region Soccer Court

            var soccerCourt = new List<SoccerCourt>
            {
                new SoccerCourt
                {
                    Name = "Soccer Court 1",
                    Image = "soccerCourt1.png",
                    HourPrice = 100M,
                    Phone = "(11) 1234-5678",
                    Address = "Av. teste 10, teste",
                    Cep = "12345-789",
                    Latitude = -23.1278154,
                    Longitude = -46.5552845,
                    OwnerUserId = testContext.Users.ToList()[0].Id
                },
                new SoccerCourt
                {
                    Name = "Soccer Court 2",
                    Image = "soccerCourt2.png",
                    HourPrice = 110M,
                    Phone = "(11) 0000-9999",
                    Address = "Av. teste 123, teste",
                    Cep = "98745-036",
                    Latitude = -22.3254,
                    Longitude = -43.7595,
                    OwnerUserId = testContext.Users.ToList()[1].Id
                },
                new SoccerCourt
                {
                    Name = "Soccer Court 3",
                    Image = "soccerCourt3.png",
                    HourPrice = 90M,
                    Phone = "(11) 3692-1472",
                    Address = "Av. teste 321, teste",
                    Cep = "01012-345",
                    Latitude = -23.1096504,
                    Longitude = -46.533172,
                    OwnerUserId = testContext.Users.ToList()[2].Id
                }
            };

            testContext.SoccerCourts.AddRange(soccerCourt);
            testContext.SaveChanges();

            #endregion

            #region Schedule Match

            var matches = new List<ScheduleMatch>
            {
                new ScheduleMatch
                {
                    FirstTeamId = testContext.Teams.ToList()[0].Id,
                    FirstTeamConfirmed = true,
                    SecondTeamId = testContext.Teams.ToList()[2].Id,
                    SecondTeamConfirmed = true,
                    SoccerCourtId = testContext.SoccerCourts.ToList()[2].Id,
                    TotalHours = 1,
                    Date = new DateTime(2020,10,20,21,0,0,DateTimeKind.Local),
                    MatchStatus = MatchStatus.Confirmed
                },
                new ScheduleMatch
                {
                    FirstTeamId = testContext.Teams.ToList()[1].Id,
                    FirstTeamConfirmed = true,
                    SecondTeamId = testContext.Teams.ToList()[0].Id,
                    SecondTeamConfirmed = false,
                    SoccerCourtId = testContext.SoccerCourts.ToList()[1].Id,
                    TotalHours = 1,
                    Date = new DateTime(2020,10,19,18,0,0,DateTimeKind.Local),
                    MatchStatus = MatchStatus.WaitingForConfirmation
                },
                new ScheduleMatch
                {
                    FirstTeamId = testContext.Teams.ToList()[1].Id,
                    FirstTeamConfirmed = true,
                    SecondTeamId = testContext.Teams.ToList()[2].Id,
                    SecondTeamConfirmed = true,
                    SoccerCourtId = testContext.SoccerCourts.ToList()[1].Id,
                    TotalHours = 1,
                    Date = new DateTime(2020,10,21,19,0,0,DateTimeKind.Local),
                    MatchStatus = MatchStatus.Confirmed
                },
                new ScheduleMatch
                {
                    FirstTeamId = testContext.Teams.ToList()[0].Id,
                    FirstTeamConfirmed = true,
                    SecondTeamId = testContext.Teams.ToList()[1].Id,
                    SecondTeamConfirmed = true,
                    SoccerCourtId = testContext.SoccerCourts.ToList()[0].Id,
                    TotalHours = 1,
                    Date = new DateTime(2020,10,16,20,0,0,DateTimeKind.Local),
                    MatchStatus = MatchStatus.Finished
                },
                new ScheduleMatch
                {
                    FirstTeamId = testContext.Teams.ToList()[2].Id,
                    FirstTeamConfirmed = false,
                    SecondTeamId = testContext.Teams.ToList()[1].Id,
                    SecondTeamConfirmed = true,
                    SoccerCourtId = testContext.SoccerCourts.ToList()[2].Id,
                    TotalHours = 1,
                    Date = new DateTime(2020,10,18,17,0,0,DateTimeKind.Local),
                    MatchStatus = MatchStatus.Canceled
                }
            };

            testContext.ScheduleMatches.Add(matches[0]);
            testContext.SaveChanges();
            testContext.ScheduleMatches.Add(matches[1]);
            testContext.SaveChanges();
            testContext.ScheduleMatches.Add(matches[2]);
            testContext.SaveChanges();
            testContext.ScheduleMatches.Add(matches[3]);
            testContext.SaveChanges();
            testContext.ScheduleMatches.Add(matches[4]);
            testContext.SaveChanges();

            #endregion

            foreach (var entity in testContext.ChangeTracker.Entries())
                entity.State = EntityState.Detached;

            return testContext;
        }
    }
}
