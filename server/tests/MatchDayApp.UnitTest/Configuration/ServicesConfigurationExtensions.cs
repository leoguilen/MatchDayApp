﻿using MatchDayApp.Domain.Common.Helpers;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Entities.Enum;
using MatchDayApp.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
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

            users[0].UserTeam = new UserTeam { UserId = users[0].Id, TeamId = Guid.NewGuid(), Accepted = true };
            users[1].UserTeam = new UserTeam { UserId = users[1].Id, TeamId = Guid.NewGuid(), Accepted = true };
            users[2].UserTeam = new UserTeam { UserId = users[2].Id, TeamId = Guid.NewGuid(), Accepted = true };

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
        public static MatchDayAppContext SeedFakeSoccerCourtData(this MatchDayAppContext testContext)
        {
            testContext.SeedFakeUserData();

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

            foreach (var entity in testContext.ChangeTracker.Entries())
                entity.State = EntityState.Detached;

            return testContext;
        }
        public static MatchDayAppContext SeedFakeScheduledMatchesData(this MatchDayAppContext testContext)
        {
            testContext
                .SeedFakeUserData()
                .SeedFakeTeamData()
                .SeedFakeSoccerCourtData();

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

            foreach (var entity in testContext.ChangeTracker.Entries())
                entity.State = EntityState.Detached;

            return testContext;
        }
    }
}
