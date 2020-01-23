using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Rule
{

    //here i need to specify some rules which will serve as constants for the game 
  public static  class Constants
    {
        public const string DirectionNotHandled = "Direction not handled";

        public const string ExitException = "Start position cannot be on a mine";

        public const string MinesException = "start position cannot be on be on the exit ";
    }
}
