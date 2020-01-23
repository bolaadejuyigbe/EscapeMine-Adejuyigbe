using EscapeMine.Core.Models;
using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Interfaces.IServices
{
  public  interface IGameService
    {
        IEnumerable<GameResult> Run(Game game);

        Status GetNewStatus(Game game);
        Game Load();
    }
}
