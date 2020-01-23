using EscapeMine.Core.Interfaces.IServices;
using EscapeMine.Core.Interfaces.IServices.Factory;
using EscapeMine.Core.Models;
using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IMoveFactory _moveFactory;
            public PlayerService (IMoveFactory moveFactory)
        {
            _moveFactory = moveFactory;
        }
        public void Move(MoveType move, Game game)
        {
            _moveFactory.Create(move).TryMove(game);
        }
    }
}
