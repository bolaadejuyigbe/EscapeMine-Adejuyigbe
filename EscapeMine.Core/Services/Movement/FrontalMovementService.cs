using EscapeMine.Core.Interfaces.IServices;
using EscapeMine.Core.Models;
using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Services.Movement
{
    public class FrontalMovementService : IMoveService
    {
        public bool IsValid(MoveType move)
        {
            return move == MoveType.M;
        }

        public void TryMove(Game game)
        {
            Coordinate newCoordinate = GetNewPosition(game.Player);
            bool isValidMove = newCoordinate.X >= 0 && newCoordinate.X <= game.Board.Width && newCoordinate.Y >= 0 && newCoordinate.Y <= game.Board.Height;
            if (isValidMove)
            {
                game.Player.Position = newCoordinate;
            }
        }


        private Coordinate GetNewPosition(Player player)
        {
            Coordinate newCoordinate;

            switch (player.Direction)
            {
                case (Direction.N):
                    newCoordinate = new Coordinate() { X = player.Position.X, Y = player.Position.Y - 1 };
                    break;
                case (Direction.E):
                    newCoordinate = new Coordinate() { X = player.Position.X +1, Y = player.Position.Y};
                    break;
                case (Direction.S):
                    newCoordinate = new Coordinate() { X = player.Position.X, Y = player.Position.Y +1 };
                    break;
                case (Direction.W):
                    newCoordinate = new Coordinate() { X = player.Position.X-1, Y = player.Position.Y };
                    break;
                default:
                    throw new Exception(Constants.DirectionNotHandled);
            }
            return newCoordinate;
        }
    }
}
