﻿using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Events.Time
{
    public class TimeJogadorRemovidoEvent : INotification
    {
        public Guid TimeId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
