using EscapeMine.Core.Interfaces.IRepositories;
using EscapeMine.Core.Interfaces.IServices;
using EscapeMine.Core.Models;
using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeMine.Core.Services
{
   public class GameService : IGameService
    {
        private readonly IPlayerService _playerService;

        private readonly IBoardCoordinatesRepository _boardCoordinatesRepository;

        private readonly IBoardRepository _boardRepository;

        private readonly IPlayerRepository _playerRepository;
        public GameService(IPlayerService playerService, IBoardCoordinatesRepository boardCoordinatesRepository, IBoardRepository boardRepository, IPlayerRepository playerRepository)
        {
            _playerService = playerService;
            _boardCoordinatesRepository = boardCoordinatesRepository;
            _boardRepository = boardRepository;
            _playerRepository = playerRepository;
        }
        public IEnumerable<GameResult> Run(Game game)
        {
            List<GameResult> gameResults = new List<GameResult>();
            Status status;
            //setting player initial position 
            Coordinate initialPosition = new Coordinate() { X = game.Player.Position.X, Y = game.Player.Position.Y };

            Direction initialDirection = game.Player.Direction;

            foreach(IEnumerable<MoveType> moves in game.Moves)
            {
                status = Status.StillInDanger;
                game.Player.Position = initialPosition;
                game.Player.Direction = initialDirection;

                foreach (MoveType move in moves)
                {
                    _playerService.Move(move, game);

                    status = GetNewStatus(game);

                    if (status != Status.StillInDanger)
                    {
                        break;
                    }
                }

                gameResults.Add(new GameResult() { Moves = moves, Status = status });
            }
            return gameResults;

        }

        public Status GetNewStatus(Game game)
        {
            Status newStatus = Status.StillInDanger;

            bool isMinehit = game.Mines.Where(m => m.X == game.Player.Position.X && m.Y == game.Player.Position.Y).Any();
            if (isMinehit)
            {
                newStatus = Status.MineHit;
            }
            else
            {
                bool isExitReached = game.Player.Position.X == game.Exit.X && game.Player.Position.Y == game.Exit.Y;
                if (isExitReached)
                {
                    newStatus = Status.Success;
                }
            }
            return newStatus;
        }

        public Game Load()
        {
            Board board = _boardRepository.GetBoard();
            IEnumerable<Coordinate> mines = _boardCoordinatesRepository.GetMines();
            Coordinate exit = _boardCoordinatesRepository.GetExit();
            Player player = _playerRepository.GetPlayer();

            if (exit.X == player.Position.X && exit.Y == player.Position.Y)
            {
                throw new Exception(Constants.ExitException);
            }
            bool isStartOnMine = mines.Any(j => j.X == player.Position.X && j.Y == player.Position.Y);

            if (isStartOnMine)
            {
                throw new Exception(Constants.MinesException);
            }
            IEnumerable<IEnumerable<MoveType>> moves = _playerRepository.GetMoves();
            return new Game()
            {
                Board = board,
                Mines = mines,
                Player = player,
                Exit = exit,
                Moves = moves
            };
        }

        

    }
}
