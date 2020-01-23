using EscapeMine.Core.Models;
using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Interfaces.IServices
{
   public  interface IMoveService
    {
        bool IsValid(MoveType move);

        void TryMove(Game game);
    }
}
