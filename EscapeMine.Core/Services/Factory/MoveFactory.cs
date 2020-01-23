using EscapeMine.Core.Interfaces.IServices;
using EscapeMine.Core.Interfaces.IServices.Factory;
using EscapeMine.Core.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeMine.Core.Services.Factory
{
   public class MoveFactory : IMoveFactory
    {
        private readonly IEnumerable<IMoveService> _moveServices;

        public MoveFactory (IEnumerable<IMoveService> moveServices)
        {
            _moveServices = moveServices;
        }

        public IMoveService Create(MoveType move)
        {
            return _moveServices.Single(item => item.IsValid(move));
        }
    }
}
