using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TeamService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> AddTeamAsync(TeamModel team)
        {
            var newTeam = _mapper.Map<Time>(team);

            var cmdResult = await _uow.Teams
                .AddRangeAsync(new[] { newTeam });

            return cmdResult.Any();
        }

        public async Task<bool> DeleteTeamAsync(Guid teamId)
        {
            var team = await _uow.Teams
                .GetByIdAsync(teamId);

            if (team != null)
            {
                await _uow.Teams.DeleteAsync(team);
                return true;
            }

            return false;
        }

        public async Task<TeamModel> GetTeamByIdAsync(Guid teamId)
        {
            var team = await _uow.Teams.GetByIdAsync(teamId);
            return _mapper.Map<TeamModel>(team);
        }

        public async Task<IReadOnlyList<TeamModel>> GetTeamsListAsync()
        {
            var teams = await _uow.Teams.ListAllAsync();
            return _mapper.Map<IReadOnlyList<TeamModel>>(teams);
        }

        public async Task<bool> UpdateTeamAsync(Guid teamId, TeamModel teamModel)
        {
            var team = await _uow.Teams
                .GetByIdAsync(teamId);

            if (team != null)
            {
                team.Name = teamModel.Name ?? team.Name;
                team.Image = teamModel.Image ?? team.Image;

                await _uow.Teams.SaveAsync(team);
                return true;
            }

            return false;
        }
    }
}
