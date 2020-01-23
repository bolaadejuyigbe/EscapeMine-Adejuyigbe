using EscapeMine.Core.DataSettings;
using EscapeMine.Core.Interfaces.IRepositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EscapeMine.Background.Repositories
{
   public  class TextFileRepository : ITextFileRepository
    {
        private readonly DataStorageSettings _dataStorageSettings;

        public TextFileRepository(IOptions<DataStorageSettings> dataStorageSettings)
        {
            _dataStorageSettings = dataStorageSettings.Value;
        }
     public string GetConfigDataRow(int skip)
        {
            return File.ReadLines(_dataStorageSettings.DefaultConfig).Skip(skip - 1).First();
        }

        public IEnumerable<string> GetConfigData(int skip)
        {
            return File.ReadLines(_dataStorageSettings.DefaultConfig).Skip(skip - 1);
        }

     
    }
}
