using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Servicos
{
    public class PartidaServico : IPartidaServico
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PartidaServico(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> ConfirmarPartidaAsync(Guid timeId, Guid partidaId)
        {
            var partida = await _uow.PartidaRepositorio
                .ObterPartidaPorIdAsync(partidaId);

            if (partida.PrimeiroTimeId == timeId)
                if (!partida.PrimeiroTimeConfirmado)
                {
                    partida.PrimeiroTimeConfirmado = true;
                    partida.StatusPartida = StatusPartida.AguardandoConfirmacao;
                }

            if (partida.SegundoTimeId == timeId)
                if (!partida.SegundoTimeConfirmado)
                {
                    partida.SegundoTimeConfirmado = true;
                    partida.StatusPartida = StatusPartida.AguardandoConfirmacao;
                }

            if (partida.PrimeiroTimeConfirmado && partida.SegundoTimeConfirmado)
                partida.StatusPartida = StatusPartida.Confirmada;

            await _uow.PartidaRepositorio.AtualizarPartidaAsync(partida);
            var commited = await _uow.CommitAsync();

            return commited > 0;
        }

        public async Task<PartidaModel> DesmarcarPartidaAsync(Guid partidaId)
        {
            var partida = await _uow.PartidaRepositorio
                .ObterPartidaPorIdAsync(partidaId);

            if (partida == null)
                return null;

            partida.StatusPartida = StatusPartida.Cancelada;

            await _uow.PartidaRepositorio.AtualizarPartidaAsync(partida);
            var commited = await _uow.CommitAsync();

            return commited > 0 ? _mapper.Map<PartidaModel>(partida) : null;
        }

        public async Task<PartidaModel> MarcarPartidaAsync(PartidaModel partidaModel)
        {
            var temPartida = await _uow.PartidaRepositorio
                .ObterAsync(p => p.QuadraFutebolId == partidaModel.QuadraFutebolId
                    && p.DataPartida == partidaModel.DataPartida);

            if (temPartida.Any())
                return null;

            var partida = _mapper.Map<Partida>(partidaModel);
            partida.StatusPartida = StatusPartida.AguardandoConfirmacao;
            partida.DataPartida = partida.DataPartida;

            await _uow.PartidaRepositorio.AdicionarPartidaAsync(partida);
            var commited = await _uow.CommitAsync();

            return commited > 0 ? _mapper.Map<PartidaModel>(partida) : null;
        }

        public async Task<PartidaModel> ObterPartidaPorIdAsync(Guid partidaId)
        {
            var partida = await _uow.PartidaRepositorio
                .ObterPartidaPorIdAsync(partidaId);

            return _mapper.Map<PartidaModel>(partida);
        }

        public async Task<IEnumerable<PartidaModel>> ObterPartidaPorQuadraIdAsync(Guid quadraId)
        {
            var partidas = await _uow.PartidaRepositorio
                .ObterAsync(p => p.QuadraFutebolId == quadraId);

            return _mapper.Map<IEnumerable<PartidaModel>>(partidas);
        }

        public async Task<IEnumerable<PartidaModel>> ObterPartidaPorTimeIdAsync(Guid timeId)
        {
            var partidas = await _uow.PartidaRepositorio
                .ObterAsync(p => p.PrimeiroTimeId == timeId
                              || p.SegundoTimeId == timeId);

            return _mapper.Map<IEnumerable<PartidaModel>>(partidas);
        }

        public async Task<IReadOnlyList<PartidaModel>> ObterPartidasAsync()
        {
            var partidas = await _uow.PartidaRepositorio
                .ObterPartidasAsync();

            return _mapper.Map<IReadOnlyList<PartidaModel>>(partidas);
        }
    }
}
