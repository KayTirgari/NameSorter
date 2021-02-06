using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameSorter.Shared.Abstractions;
using NameSorter.Shared.Implementations;
using NameSorter.Shared.Models;
using System;
using System.Collections.Generic;

namespace NameSorter.Tests
{
    [TestClass]
    public class SortingServiceTest
    {
        private List<NameModel> unsortedNames;

        private ISortingService sortingService;

        [TestInitialize]
        public void SetUp()
        {
            unsortedNames = new List<NameModel>();
            sortingService = new SortingService();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Sort_InvalidInput()
        {
            _ = sortingService.Sort(null);
        }

        [TestMethod]
        public void Sort_Firstname_And_Lastname_Only()
        {
            var list = new List<string> { "B", "A", "C" };
            foreach (var item in list)
            {
                unsortedNames.Add(new NameModel
                {
                    FirstName = item,
                    LastName = item
                });
            }

            list.Sort();
            var names = sortingService.Sort(unsortedNames);
            for (int i = 0; i < names.Count; ++i)
            {
                Assert.AreEqual(names[i].LastName, list[i]);
            }
        }

        [TestMethod]
        public void Sort_Firstname_Lastname_GivenName()
        {
            var nameExtractionService = new NameExtractionService();

            unsortedNames = new List<NameModel>
            {
                nameExtractionService.ExtractName("Beau Tristan Bentley"),
                nameExtractionService.ExtractName("Adonis Julius Archer"),
                nameExtractionService.ExtractName("Shelby Nathan Yoder")
            };

            var sorted = new List<NameModel>
            {
                nameExtractionService.ExtractName("Adonis Julius Archer"),
                nameExtractionService.ExtractName("Beau Tristan Bentley"),
                nameExtractionService.ExtractName("Shelby Nathan Yoder")
            };

            var sortedNames = sortingService.Sort(unsortedNames);
            for (int i = 0; i < sortedNames.Count; ++i)
            {
                Assert.AreEqual(sortedNames[i].FirstName, sorted[i].FirstName);
                Assert.AreEqual(sortedNames[i].LastName, sorted[i].LastName);
                Assert.AreEqual(sortedNames[i].GivenNames[0], sorted[i].GivenNames[0]);
            }
        }
    }
}
