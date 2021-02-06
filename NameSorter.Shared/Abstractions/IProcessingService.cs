
using System.Threading.Tasks;

namespace NameSorter.Shared.Abstractions
{
    public interface IProcessingService
    {
        Task ProcessAsync(string filename);
    }
}
