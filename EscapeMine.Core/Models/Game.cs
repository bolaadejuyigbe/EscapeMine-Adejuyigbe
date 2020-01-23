using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Models
{
   public class Game
    {
        public Board Board { get; set; }

        public Player Player { get; set; }

        public IEnumerable<Coordinate> Mines { get; set; }

        public Coordinate Exit { get; set; }
        
        public IEnumerable<IEnumerable<MoveType>> Moves { get; set; }
    }
}
