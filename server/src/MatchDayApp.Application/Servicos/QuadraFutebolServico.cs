using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Especificacoes.Quadra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class QuadraFutebolServico : IQuadraFutebolServico
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public QuadraFutebolServico(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<QuadraModel> AdicionarQuadraAsync(QuadraModel quadra)
        {
            var novaQuadra = _mapper.Map<QuadraFutebol>(quadra);

            var quadraInserida = await _uow.QuadraFutebolRepositorio
                .SaveAsync(novaQuadra);

            return _mapper.Map<QuadraModel>(quadraInserida);
        }

        public async Task<QuadraModel> AtualizarQuadraAsync(Guid quadraId, QuadraModel quadra)
        {
            var quadraAtualizar = await _uow.QuadraFutebolRepositorio
                .GetByIdAsync(quadraId);

            if (quadraAtualizar != null)
            {
                quadraAtualizar.Nome = quadra.Nome ?? quadraAtualizar.Nome;
                quadraAtualizar.Imagem = quadra.Imagem ?? quadraAtualizar.Imagem;
                quadraAtualizar.Telefone = quadra.Telefone ?? quadraAtualizar.Telefone;
                quadraAtualizar.Endereco = quadra.Endereco ?? quadraAtualizar.Endereco;
                quadraAtualizar.Cep = quadra.Cep ?? quadraAtualizar.Cep;

                var quadraAtualizada = await _uow.QuadraFutebolRepositorio
                    .SaveAsync(quadraAtualizar);
                
                return _mapper.Map<QuadraModel>(quadraAtualizada);
            }

            return null;
        }

        public async Task<bool> DeletarQuadraAsync(Guid quadraId)
        {
            var quadra = await _uow.QuadraFutebolRepositorio
                .GetByIdAsync(quadraId);

            if (quadra != null)
            {
                await _uow.QuadraFutebolRepositorio
                    .DeleteAsync(quadra);
                return true;
            }

            return false;
        }

        public async Task<QuadraModel> ObterQuadraPorIdAsync(Guid quadraId)
        {
            var quadra = await _uow.QuadraFutebolRepositorio
                .GetByIdAsync(quadraId);

            return _mapper.Map<QuadraModel>(quadra);
        }

        public async Task<IReadOnlyList<QuadraModel>> ObterQuadrasAsync()
        {
            var quadras = await _uow.QuadraFutebolRepositorio
                .ListAllAsync();
            
            return _mapper.Map<IReadOnlyList<QuadraModel>>(quadras);
        }

        public async Task<IReadOnlyList<QuadraModel>> ObterQuadrasPorLocalizacaoAsync(double lat, double lon)
        {
            var spec = new QuadraProximaAoUsuarioEspecificacao(lat, lon);
            var quadras = await _uow.QuadraFutebolRepositorio.GetAsync(spec);
            return _mapper.Map<IReadOnlyList<QuadraModel>>(quadras);
        }
    }
}
