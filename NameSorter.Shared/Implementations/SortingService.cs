using NameSorter.Shared.Abstractions;
using NameSorter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NameSorter.Shared.Implementations
{
    public class SortingService : ISortingService
    {
        public List<NameModel> Sort(List<NameModel> names)
        {
            if (names == null)
                throw new Exception($"Invalid input {nameof(names)}");

            var sortedList = names.OrderBy(n => n.LastName)
                  .ThenBy(n => n.FirstName)
                  .ThenBy(n => n.GivenNames[0])
                  .ThenBy(n => n.GivenNames[1]);
            return sortedList.ToList();
        }
    }
}
