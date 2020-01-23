using EscapeMine.Core.Models;
using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Interfaces.IRepositories
{
   public  interface IPlayerRepository
    {
        Player GetPlayer();
        IEnumerable<IEnumerable<MoveType>> GetMoves();
    }
}
