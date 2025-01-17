using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GlowExp.Components.Data
{
    public class FileManager
    {
        private readonly string _filePath;

        public FileManager()
        {
            string appDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GlowExp");
            if (!Directory.Exists(appDataDirectory))
            {
                Directory.CreateDirectory(appDataDirectory);
            }
            _filePath = Path.Combine(appDataDirectory, "transactions.json");
        }

        public string FilePath => _filePath;
    }

}
