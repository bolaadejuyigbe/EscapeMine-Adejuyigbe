using EscapeMine.Core.Interfaces.IRepositories;
using EscapeMine.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Background.Repositories
{
  public  class BoardRepository : IBoardRepository
    {
        private readonly ITextFileRepository _textFileRepository;

        public BoardRepository (ITextFileRepository textFileRepository)
        {
            _textFileRepository = textFileRepository;
        }
        public Board GetBoard()
        {
            string[] boardSizeConfig = _textFileRepository.GetConfigDataRow(1).Split(" ");
            return new Board() { Width = Convert.ToInt32(boardSizeConfig[0]), Height = Convert.ToInt32(boardSizeConfig[1]) };
        }

    }
}
