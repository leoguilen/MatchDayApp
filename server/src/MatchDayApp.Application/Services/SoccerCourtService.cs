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
    public class SoccerCourtService : ISoccerCourtService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SoccerCourtService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> AddSoccerCourtAsync(SoccerCourtModel soccerCourt)
        {
            var newSoccerCourt = _mapper.Map<SoccerCourt>(soccerCourt);

            var cmdResult = await _uow.SoccerCourts
                .AddRangeAsync(new[] { newSoccerCourt });

            return cmdResult.Any();
        }

        public async Task<bool> DeleteSoccerCourtAsync(Guid soccerCourtId)
        {
            var soccerCourt = await _uow.SoccerCourts
                .GetByIdAsync(soccerCourtId);

            if (soccerCourt != null)
            {
                await _uow.SoccerCourts.DeleteAsync(soccerCourt);
                return true;
            }

            return false;
        }

        public async Task<SoccerCourtModel> GetSoccerCourtByIdAsync(Guid soccerCourtId)
        {
            var soccerCourt = await _uow.SoccerCourts.GetByIdAsync(soccerCourtId);
            return _mapper.Map<SoccerCourtModel>(soccerCourt);
        }

        public async Task<IReadOnlyList<SoccerCourtModel>> GetSoccerCourtsListAsync()
        {
            var soccerCourts = await _uow.SoccerCourts.ListAllAsync();
            return _mapper.Map<IReadOnlyList<SoccerCourtModel>>(soccerCourts);
        }

        public async Task<bool> UpdateSoccerCourtAsync(Guid soccerCourtId, SoccerCourtModel soccerCourtModel)
        {
            var soccerCourt = await _uow.SoccerCourts
                .GetByIdAsync(soccerCourtId);

            if (soccerCourt != null)
            {
                soccerCourt.Name = soccerCourtModel.Name ?? soccerCourt.Name;
                soccerCourt.Image = soccerCourtModel.Image ?? soccerCourt.Image;
                soccerCourt.Phone = soccerCourtModel.Phone ?? soccerCourt.Phone;
                soccerCourt.Address = soccerCourtModel.Address ?? soccerCourt.Address;
                soccerCourt.Cep = soccerCourtModel.Cep ?? soccerCourt.Cep;

                await _uow.SoccerCourts.SaveAsync(soccerCourt);
                return true;
            }

            return false;
        }
    }
}
