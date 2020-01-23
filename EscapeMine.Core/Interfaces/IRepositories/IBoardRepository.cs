using EscapeMine.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Interfaces.IRepositories
{
  public  interface IBoardRepository
    {
        Board GetBoard();
    }
}
