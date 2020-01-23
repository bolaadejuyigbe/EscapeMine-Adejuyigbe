using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Rule
{
    //assigning numbers to the movemement type
  public enum MoveType 
    {
        R = 1,

        L = 2,
        
        M = 3
    }
    //assigning numbers to the game status 
    public enum Status
    {

        Success = 1,

        MineHit = 2,

        StillInDanger = 3
    }

    //assigning numbers t game direction 
    public enum Direction
    {

        N= 1,

        E = 2,

        S = 3,

        W = 4

    }
}
