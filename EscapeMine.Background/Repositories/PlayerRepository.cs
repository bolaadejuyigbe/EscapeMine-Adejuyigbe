using EscapeMine.Core.Interfaces.IRepositories;
using EscapeMine.Core.Models;
using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeMine.Background.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ITextFileRepository _textFileRepository;

        public PlayerRepository(ITextFileRepository textFileRepository)
        {
            _textFileRepository = textFileRepository;
        }
        public IEnumerable<IEnumerable<MoveType>> GetMoves()
        {
            List<List<MoveType>> moves = new List<List<MoveType>>();
            IEnumerable<string> lines = _textFileRepository.GetConfigData(5);
            lines.ToList().ForEach(line =>
            {

                List<MoveType> movesRow = new List<MoveType>();

                line.Split(' ').ToList().ForEach(p =>
                {
                    movesRow.Add((MoveType)Enum.Parse(typeof(MoveType), p));
                });
                moves.Add(movesRow);
            });
            return moves;
        }

        public Player GetPlayer()
        {
            string[] initialPositionConfig = _textFileRepository.GetConfigDataRow(4).Split(" ");
            int initialX = Convert.ToInt32(initialPositionConfig[0]);
            int InitialY = Convert.ToInt32(initialPositionConfig[1]);

            Direction initialDirection = (Direction)Enum.Parse(typeof(Direction), initialPositionConfig[2]);
            return new Player()
            {
                Direction = initialDirection,
                Position = new Coordinate() { X = initialX, Y = InitialY }
            };
        }
    }
}
