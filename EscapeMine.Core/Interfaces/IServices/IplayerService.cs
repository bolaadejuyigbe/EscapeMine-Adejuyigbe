using EscapeMine.Core.Models;
using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Interfaces.IServices
{
   public interface IPlayerService
    {
        void Move(MoveType move, Game game);
    }
}
