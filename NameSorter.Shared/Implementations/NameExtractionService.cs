using NameSorter.Shared.Abstractions;
using NameSorter.Shared.Models;
using System;
using System.Linq;

namespace NameSorter.Shared.Implementations
{
    public class NameExtractionService : INameExtractionService
    {
        public NameModel ExtractName(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new Exception("Invalid input");

            var name = new NameModel();
            var parts = input.Split(' ').ToList();

            // Store and remove the first element
            if (parts.Any())
            {
                name.FirstName = parts[0];
                parts.RemoveAt(0);
            }

            // Store and remove the last element
            if (parts.Any())
            {
                var lastIndex = parts.Count - 1;
                name.LastName = parts[lastIndex];
                parts.RemoveAt(lastIndex);
            }

            name.GivenNames = parts;

            return name;
        }
    }
}
