using EscapeMine.Core.Interfaces.IRepositories;
using EscapeMine.Core.Interfaces.IServices.Factory;
using EscapeMine.Core.Models;
using EscapeMine.Core.Rule;
using EscapeMine.Core.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EscapeMine.Tests.Services
{
 public   class GameServiceTest
    {
        private readonly GameService _gameService;
        private readonly PlayerService _playerService;
        private readonly Mock<IBoardCoordinatesRepository> _mockBoardCoordinatesRepository;
        private readonly Mock<IBoardRepository> _mockBoardRepository;
        private readonly Mock<IPlayerRepository> _mockPlayerRepository;
        private readonly Mock<IMoveFactory> _mockMoveFactory;

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
        public GameServiceTest()
        {
            _mockMoveFactory = new Mock<IMoveFactory>();
            _playerService = new PlayerService(_mockMoveFactory.Object);
            _mockBoardCoordinatesRepository = new Mock<IBoardCoordinatesRepository>();
            _mockBoardRepository = new Mock<IBoardRepository>();
            _mockPlayerRepository = new Mock<IPlayerRepository>();

            _gameService = new GameService(
               _playerService,
               _mockBoardCoordinatesRepository.Object,
               _mockBoardRepository.Object,
               _mockPlayerRepository.Object
               );
        }

        [Theory]
        [InlineData(0, 1, Status.StillInDanger)]
        [InlineData(1, 1, Status.MineHit)]
        [InlineData(4, 2, Status.Success)]
        public void GetNewStatusTest(int xPosition, int yPosition, Status expectedStatus)
        {
            game.Player.Position.X = xPosition;
            game.Player.Position.Y = yPosition;
            Status status = _gameService.GetNewStatus(game);

            Assert.Equal(expectedStatus, status);
        }

        [Fact]
        public void LoadTest()
        {
            _mockBoardRepository.Setup(x => x.GetBoard()).Returns(new Board() { Width = 5, Height = 4 });
            _mockBoardCoordinatesRepository.Setup(x => x.GetMines()).Returns(new List<Coordinate>()
            {
                new Coordinate() { X = 1, Y = 1 },
                new Coordinate() { X = 3, Y = 1 },
                new Coordinate() { X = 3, Y = 3 }
            });
            _mockBoardCoordinatesRepository.Setup(x => x.GetExit()).Returns(new Coordinate() { X = 4, Y = 2 });
            _mockPlayerRepository.Setup(x => x.GetPlayer()).Returns(new Player() { Position = new Coordinate() { X = 0, Y = 1 }, Direction = Direction.N });
            _mockPlayerRepository.Setup(x => x.GetMoves()).Returns(new List<List<MoveType>>
            {
                new List<MoveType>() { MoveType.R, MoveType.M, MoveType.L, MoveType.M, MoveType.M },
                new List<MoveType>() { MoveType.M, MoveType.R, MoveType.M, MoveType.M, MoveType.M, MoveType.M, MoveType.R, MoveType.M, MoveType.M },
                new List<MoveType>() { MoveType.R, MoveType.R, MoveType.M, MoveType.M, MoveType.L, MoveType.M, MoveType.M }
            });

            Game mockGame = _gameService.Load();

            mockGame.Should().BeEquivalentTo(game);
        }

        [Fact]
        public void LoadExitExceptionTest()
        {
            _mockBoardRepository.Setup(x => x.GetBoard()).Returns(new Board() { Width = 5, Height = 4 });
            _mockBoardCoordinatesRepository.Setup(x => x.GetMines()).Returns(new List<Coordinate>()
            {
                new Coordinate() { X = 1, Y = 1 },
                new Coordinate() { X = 3, Y = 1 },
                new Coordinate() { X = 3, Y = 3 }
            });
            _mockBoardCoordinatesRepository.Setup(x => x.GetExit()).Returns(new Coordinate() { X = 4, Y = 2 });
            _mockPlayerRepository.Setup(x => x.GetPlayer()).Returns(new Player() { Position = new Coordinate() { X = 4, Y = 2 }, Direction = Direction.N });
            _mockPlayerRepository.Setup(x => x.GetMoves()).Returns(new List<List<MoveType>>
            {
                new List<MoveType>() { MoveType.R, MoveType.M, MoveType.L, MoveType.M, MoveType.M },
                new List<MoveType>() { MoveType.M, MoveType.R, MoveType.M, MoveType.M, MoveType.M, MoveType.M, MoveType.R, MoveType.M, MoveType.M },
                new List<MoveType>() { MoveType.R, MoveType.R, MoveType.M, MoveType.M, MoveType.L, MoveType.M, MoveType.M }
            });

            Exception exception = Assert.Throws<Exception>(() => _gameService.Load());

            Assert.Equal(Constants.ExitException, exception.Message);
        }

        [Fact]
        public void LoadMinesExceptionTest()
        {
            _mockBoardRepository.Setup(x => x.GetBoard()).Returns(new Board() { Width = 5, Height = 4 });
            _mockBoardCoordinatesRepository.Setup(x => x.GetMines()).Returns(new List<Coordinate>()
            {
                new Coordinate() { X = 1, Y = 1 },
                new Coordinate() { X = 3, Y = 1 },
                new Coordinate() { X = 3, Y = 3 }
            });
            _mockBoardCoordinatesRepository.Setup(x => x.GetExit()).Returns(new Coordinate() { X = 4, Y = 2 });
            _mockPlayerRepository.Setup(x => x.GetPlayer()).Returns(new Player() { Position = new Coordinate() { X = 1, Y = 1 }, Direction = Direction.N });
            _mockPlayerRepository.Setup(x => x.GetMoves()).Returns(new List<List<MoveType>>
            {
                new List<MoveType>() { MoveType.R, MoveType.M, MoveType.L, MoveType.M, MoveType.M },
                new List<MoveType>() { MoveType.M, MoveType.R, MoveType.M, MoveType.M, MoveType.M, MoveType.M, MoveType.R, MoveType.M, MoveType.M },
                new List<MoveType>() { MoveType.R, MoveType.R, MoveType.M, MoveType.M, MoveType.L, MoveType.M, MoveType.M }
            });

            Exception exception = Assert.Throws<Exception>(() => _gameService.Load());

            Assert.Equal(Constants.MinesException, exception.Message);
        }
    }
}
