using NameSorter.Shared.Models;
using System.Collections.Generic;

namespace NameSorter.Shared.Abstractions
{
    public interface ISortingService
    {
        List<NameModel> Sort(List<NameModel> items);
    }
}
