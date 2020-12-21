using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Servicos
{
    public class TimeServico : ITimeServico
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TimeServico(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<TimeModel> AdicionarTimeAsync(TimeModel time)
        {
            var novoTime = _mapper.Map<Time>(time);

            var timeAdicionado = await _uow.TimeRepositorio
                .SaveAsync(novoTime);

            return _mapper.Map<TimeModel>(timeAdicionado);
        }

        public async Task<TimeModel> AtualizarTimeAsync(Guid timeId, TimeModel timeModel)
        {
            var time = await _uow.TimeRepositorio
                .GetByIdAsync(timeId);

            if (time != null)
            {
                time.Nome = timeModel.Nome ?? time.Nome;
                time.Imagem = timeModel.Imagem ?? time.Imagem;

                var timeAtualizado = await _uow.TimeRepositorio
                    .SaveAsync(time);

                return _mapper.Map<TimeModel>(timeAtualizado);
            }

            return null;
        }

        public async Task<bool> DeletarTimeAsync(Guid timeId)
        {
            var time = await _uow.TimeRepositorio
                .GetByIdAsync(timeId);

            if (time != null)
            {
                await _uow.TimeRepositorio
                    .DeleteAsync(time);

                return true;
            }

            return false;
        }

        public async Task<TimeModel> ObterTimePorIdAsync(Guid timeId)
        {
            var time = await _uow.TimeRepositorio
                .GetByIdAsync(timeId);

            return _mapper.Map<TimeModel>(time);
        }

        public async Task<IReadOnlyList<TimeModel>> ObterTimesAsync()
        {
            var times = await _uow.TimeRepositorio
                .ListAllAsync();

            return _mapper.Map<IReadOnlyList<TimeModel>>(times);
        }
    }
}
