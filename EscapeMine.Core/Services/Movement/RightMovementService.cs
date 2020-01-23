using EscapeMine.Core.Interfaces.IServices;
using EscapeMine.Core.Models;
using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Services.Movement
{
    public class RightMovementService : IMoveService
    {
        public bool IsValid(MoveType move)
        {
            return move == MoveType.R;

        }

        public void TryMove(Game game)
        {
            Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));
            int newIndex = Array.IndexOf(directions, game.Player.Direction) + 1;
            game.Player.Direction = (newIndex == directions.Length) ? directions[0] : directions[newIndex];
        }
    }
}
