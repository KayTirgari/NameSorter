using NameSorter.Shared.Models;

namespace NameSorter.Shared.Abstractions
{
    public interface INameExtractionService
    {
        NameModel ExtractName(string input);
    }
}
