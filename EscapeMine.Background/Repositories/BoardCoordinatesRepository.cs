using EscapeMine.Core.Interfaces.IRepositories;
using EscapeMine.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Background.Repositories
{
    public class BoardCoordinatesRepository : IBoardCoordinatesRepository
    {
        private readonly ITextFileRepository _textFileRepository;

        public BoardCoordinatesRepository (ITextFileRepository textFileRepository)
        {
            _textFileRepository = textFileRepository;
        }
        public Coordinate GetExit()
        {
            string[] exitConfig = _textFileRepository.GetConfigDataRow(3).Split(" ");
            return new Coordinate() { X = Convert.ToInt32(exitConfig[0]), Y = Convert.ToInt32(exitConfig[1]) };

        }

        public IEnumerable<Coordinate> GetMines()
        {
            string[] minesConfig = _textFileRepository.GetConfigDataRow(2).Split(" ");
            List<Coordinate> mines = new List<Coordinate>();
            foreach (string mineConfig in minesConfig)
            {
                string[] mine = mineConfig.Split(",");
                if (mine.Length > 1)
                {
                    mines.Add(new Coordinate() { X = Convert.ToInt32(mine[0]), Y = Convert.ToInt32(mine[1]) });
                }
            }
            return mines;
        }

    }
}
