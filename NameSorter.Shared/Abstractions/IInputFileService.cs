using NameSorter.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NameSorter.Shared.Abstractions
{
    public interface IInputFileService
    {
        Task<List<NameModel>> ReadAsync(string filename);

        Task WriteAsync(string filename, List<NameModel> items);
    }
}
