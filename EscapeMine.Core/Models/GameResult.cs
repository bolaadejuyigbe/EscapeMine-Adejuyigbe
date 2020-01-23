using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Models
{
    public class GameResult
    {
        public IEnumerable<MoveType> Moves { get; set; }
        public Status Status { get; set; } 
    }
}
