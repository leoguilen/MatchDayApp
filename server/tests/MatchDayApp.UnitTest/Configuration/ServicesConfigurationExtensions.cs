using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Entidades.Enum;
using MatchDayApp.Domain.Helpers;
using MatchDayApp.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchDayApp.UnitTest.Configuration
{
    public static class ServicesConfigurationExtensions
    {
        public static MatchDayAppContext SeedFakeData(this MatchDayAppContext testContext)
        {
            #region Usuarios

            var salt = SenhaHasherHelper.CriarSalt(8);

            var Usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Nome = "Test",
                    Sobrenome = "One",
                    UserNome = "test1",
                    Email = "test1@email.com",
                    EmailConfirmado = true,
                    Telefone = "+551155256325",
                    Senha = SenhaHasherHelper.GerarHash("test123", salt),
                    Salt = salt,
                    TipoUsuario = TipoUsuario.ProprietarioQuadra,
                    Deletado = true
                },
                new Usuario
                {
                    Nome = "Test",
                    Sobrenome = "Two",
                    UserNome = "test2",
                    Email = "test2@email.com",
                    EmailConfirmado = true,
                    Telefone = "+551112345525",
                    Senha = SenhaHasherHelper.GerarHash("test321", salt),
                    Salt = salt,
                    TipoUsuario = TipoUsuario.ProprietarioTime
                },

                new Usuario
                {
                    Nome = "Test",
                    Sobrenome = "Three",
                    UserNome = "test3",
                    Email = "test3@email.com",
                    EmailConfirmado = true,
                    Telefone = "+551198765525",
                    Senha = SenhaHasherHelper.GerarHash("test231", salt),
                    Salt = salt,
                    TipoUsuario = TipoUsuario.ProprietarioQuadra
                }
            };

            testContext.Usuarios.AddRange(Usuarios);
            testContext.SaveChanges();

            #endregion

            #region Confirmação Email Request

            var confirmacaoEmail = new ConfirmacaoEmail
            {
                Id = Guid.NewGuid(),
                UsuarioId = Usuarios.Last().Id,
                RequisicaoEm = DateTime.Now,
                ChaveConfirmacao = Guid.Parse("C9267B0B-54A1-4971-9ED7-173008905696")
            };

            testContext.ConfirmacaoEmails.AddRange(confirmacaoEmail);
            testContext.SaveChanges();

            #endregion

            #region Times

            var teams = new List<Time>
            {
                new Time
                {
                    Nome = "Team 1",
                    Imagem = "team1.png",
                    QtdIntegrantes = 15,
                    UsuarioProprietarioId = testContext.Usuarios.ToList()[0].Id
                },
                new Time
                {
                    Nome = "Team 2",
                    Imagem = "team2.png",
                    QtdIntegrantes = 13,
                    UsuarioProprietarioId = testContext.Usuarios.ToList()[1].Id
                },
                new Time
                {
                    Nome = "Team 3",
                    Imagem = "team3.png",
                    QtdIntegrantes = 11,
                    UsuarioProprietarioId = testContext.Usuarios.ToList()[2].Id
                }
            };

            Usuarios[0].UsuarioTime = new UsuarioTime { UsuarioId = Usuarios[0].Id, TimeId = teams[0].Id, Aceito = true };
            Usuarios[1].UsuarioTime = new UsuarioTime { UsuarioId = Usuarios[1].Id, TimeId = teams[1].Id, Aceito = true };
            Usuarios[2].UsuarioTime = new UsuarioTime { UsuarioId = Usuarios[2].Id, TimeId = teams[2].Id, Aceito = true };

            testContext.Usuarios.UpDataPartidaRange(Usuarios);
            testContext.Times.AddRange(teams);
            testContext.SaveChanges();

            #endregion

            #region Quadras

            var QuadraFutebol = new List<QuadraFutebol>
            {
                new QuadraFutebol
                {
                    Nome = "Soccer Court 1",
                    Imagem = "QuadraFutebol1.png",
                    PrecoHora = 100M,
                    Telefone = "(11) 1234-5678",
                    Endereco = "Av. teste 10, teste",
                    Cep = "12345-789",
                    Latitude = -23.1278154,
                    Longitude = -46.5552845,
                    UsuarioProprietarioId = testContext.Usuarios.ToList()[0].Id
                },
                new QuadraFutebol
                {
                    Nome = "Soccer Court 2",
                    Imagem = "QuadraFutebol2.png",
                    PrecoHora = 110M,
                    Telefone = "(11) 0000-9999",
                    Endereco = "Av. teste 123, teste",
                    Cep = "98745-036",
                    Latitude = -22.3254,
                    Longitude = -43.7595,
                    UsuarioProprietarioId = testContext.Usuarios.ToList()[1].Id
                },
                new QuadraFutebol
                {
                    Nome = "Soccer Court 3",
                    Imagem = "QuadraFutebol3.png",
                    PrecoHora = 90M,
                    Telefone = "(11) 3692-1472",
                    Endereco = "Av. teste 321, teste",
                    Cep = "01012-345",
                    Latitude = -23.1096504,
                    Longitude = -46.533172,
                    UsuarioProprietarioId = testContext.Usuarios.ToList()[2].Id
                }
            };

            testContext.QuadraFutebols.AddRange(QuadraFutebol);
            testContext.SaveChanges();

            #endregion

            #region Partidas

            var matches = new List<Partida>
            {
                new Partida
                {
                    PrimeiroTimeId = testContext.Times.ToList()[0].Id,
                    PrimeiroTimeConfirmado = true,
                    SegundoTimeId = testContext.Times.ToList()[2].Id,
                    SegundoTimeConfirmado = true,
                    QuadraFutebolId = testContext.Quadras.ToList()[2].Id,
                    HorasPartida = 1,
                    DataPartida = new DateTime(2020,10,20,21,0,0,DateTimeKind.Local),
                    StatusPartida = StatusPartida.Confirmada
                },
                new Partida
                {
                    PrimeiroTimeId = testContext.Times.ToList()[1].Id,
                    PrimeiroTimeConfirmado = true,
                    SegundoTimeId = testContext.Times.ToList()[0].Id,
                    SegundoTimeConfirmado = false,
                    QuadraFutebolId = testContext.Quadras.ToList()[1].Id,
                    HorasPartida = 1,
                    DataPartida = new DateTime(2020,10,19,18,0,0,DateTimeKind.Local),
                    StatusPartida = StatusPartida.AguardandoConfirmacao
                },
                new Partida
                {
                    PrimeiroTimeId = testContext.Times.ToList()[1].Id,
                    PrimeiroTimeConfirmado = true,
                    SegundoTimeId = testContext.Times.ToList()[2].Id,
                    SegundoTimeConfirmado = true,
                    QuadraFutebolId = testContext.Quadras.ToList()[1].Id,
                    HorasPartida = 1,
                    DataPartida = new DateTime(2020,10,21,19,0,0,DateTimeKind.Local),
                    StatusPartida = StatusPartida.Confirmada
                },
                new Partida
                {
                    PrimeiroTimeId = testContext.Times.ToList()[0].Id,
                    PrimeiroTimeConfirmado = true,
                    SegundoTimeId = testContext.Times.ToList()[1].Id,
                    SegundoTimeConfirmado = true,
                    QuadraFutebolId = testContext.Quadras.ToList()[0].Id,
                    HorasPartida = 1,
                    DataPartida = new DateTime(2020,10,16,20,0,0,DateTimeKind.Local),
                    StatusPartida = StatusPartida.Finalizada
                },
                new Partida
                {
                    PrimeiroTimeId = testContext.Times.ToList()[2].Id,
                    PrimeiroTimeConfirmado = false,
                    SegundoTimeId = testContext.Times.ToList()[1].Id,
                    SegundoTimeConfirmado = true,
                    QuadraFutebolId = testContext.Quadras.ToList()[2].Id,
                    HorasPartida = 1,
                    DataPartida = new DateTime(2020,10,18,17,0,0,DateTimeKind.Local),
                    StatusPartida = StatusPartida.Cancelada
                }
            };

            testContext.Partidas.Add(matches[0]);
            testContext.SaveChanges();
            testContext.Partidas.Add(matches[1]);
            testContext.SaveChanges();
            testContext.Partidas.Add(matches[2]);
            testContext.SaveChanges();
            testContext.Partidas.Add(matches[3]);
            testContext.SaveChanges();
            testContext.Partidas.Add(matches[4]);
            testContext.SaveChanges();

            #endregion

            foreach (var entity in testContext.ChangeTracker.Entries())
                entity.State = EntityState.Detached;

            return testContext;
        }
    }
}
