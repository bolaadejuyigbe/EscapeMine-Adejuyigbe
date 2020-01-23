using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Interfaces.IServices.Factory
{
   public interface IMoveFactory
    {
        IMoveService Create(MoveType move);
    }
}
