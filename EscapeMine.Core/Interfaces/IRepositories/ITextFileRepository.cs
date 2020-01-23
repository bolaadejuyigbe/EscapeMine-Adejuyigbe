using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMine.Core.Interfaces.IRepositories
{
   public interface ITextFileRepository
    {
        string GetConfigDataRow(int skip);

        IEnumerable<string> GetConfigData(int skip);
    }
}
