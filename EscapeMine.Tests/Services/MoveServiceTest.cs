using System;
using EscapeMine.Core.Services.Movement;
using System.Collections.Generic;
using System.Text;
using EscapeMine.Core.Models;
using EscapeMine.Core.Rule;
using Xunit;

namespace EscapeMine.Tests.Services
{
     public class MoveServiceTest
    {
        private readonly FrontalMovementService _frontalMovementService;
        private readonly LeftMovementService _leftMovementService;
        private readonly RightMovementService _rightMovementService;

        private readonly Game game = new Game()
        {
            Board = new Board() { Width = 5, Height = 4 },
            Mines = new List<Coordinate>()
            {
                new Coordinate() { X = 1, Y = 1 },
                new Coordinate() { X = 3, Y = 1 },
                new Coordinate() { X = 3, Y = 3 }
            },
            Player = new Player() { Position = new Coordinate() { X = 0, Y = 1 }, Direction = Direction.N },
            Exit = new Coordinate() { X = 4, Y = 2 },
            Moves = new List<List<MoveType>>
            {
                new List<MoveType>() { MoveType.R, MoveType.M, MoveType.L, MoveType.M, MoveType.M },
                new List<MoveType>() { MoveType.M, MoveType.R, MoveType.M, MoveType.M, MoveType.M, MoveType.M, MoveType.R, MoveType.M, MoveType.M },
                new List<MoveType>() { MoveType.R, MoveType.R, MoveType.M, MoveType.M, MoveType.L, MoveType.M, MoveType.M }
            }
        };

        public MoveServiceTest()
        {
            _frontalMovementService = new FrontalMovementService();
            _leftMovementService = new LeftMovementService();
            _rightMovementService = new RightMovementService();
        }

        [Theory]
        [InlineData(Direction.N, 0, 0)]
        [InlineData(Direction.E, 1, 1)]
        [InlineData(Direction.W, 0, 1)]
        [InlineData(Direction.S, 0, 2)]
        public void MoveFrontTest(Direction initialDirection, int expectedX, int expectedY)
        {
            game.Player.Direction = initialDirection;
            _frontalMovementService.TryMove(game);

            Assert.Equal(expectedX, game.Player.Position.X);
            Assert.Equal(expectedY, game.Player.Position.Y);
        }

        [Theory]
        [InlineData(Direction.N, Direction.W)]
        [InlineData(Direction.E, Direction.N)]
        [InlineData(Direction.S, Direction.E)]
        [InlineData(Direction.W, Direction.S)]
        public void MoveLeftTest(Direction initialDirection, Direction expectedDirection)
        {
            game.Player.Direction = initialDirection;
            _leftMovementService.TryMove(game);

            Assert.Equal(expectedDirection, game.Player.Direction);
        }

        [Theory]
        [InlineData(Direction.N, Direction.E)]
        [InlineData(Direction.E, Direction.S)]
        [InlineData(Direction.S, Direction.W)]
        [InlineData(Direction.W, Direction.N)]
        public void MoveRightTest(Direction initialDirection, Direction expectedDirection)
        {
            game.Player.Direction = initialDirection;
            _rightMovementService.TryMove(game);

            Assert.Equal(expectedDirection, game.Player.Direction);
        }

    }
}