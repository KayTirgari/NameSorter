using NameSorter.Shared.Abstractions;
using NameSorter.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NameSorter.Shared.Implementations
{
    public class InputFileService : IInputFileService
    {
        #region Fields

        private readonly INameExtractionService nameExtractionService;

        #endregion

        public InputFileService(
            INameExtractionService nameExtractionSrv)
        {
            nameExtractionService = nameExtractionSrv;
        }

        public async Task<List<NameModel>> ReadAsync(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new Exception($"Invalid input {nameof(filename)}");

            var names = new List<NameModel>();
            using var streamReader = new StreamReader(filename);
            var line = await streamReader.ReadLineAsync();

            while (line != null)
            {
                var name = nameExtractionService.ExtractName(line);
                names.Add(name);
                line = await streamReader.ReadLineAsync();
            }

            return names;
        }

        public async Task WriteAsync(string filename, List<NameModel> names)
        {
            if (string.IsNullOrEmpty(filename))
                throw new Exception($"Invalid input {nameof(filename)}");

            if (names == null)
                throw new Exception($"Invalid input {nameof(names)}");

            using var streamWriter = new StreamWriter(filename);
            foreach (var name in names)
                await streamWriter.WriteLineAsync(name.ToString());
        }
    }
}
