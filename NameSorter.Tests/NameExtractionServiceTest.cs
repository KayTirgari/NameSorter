using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameSorter.Shared.Abstractions;
using NameSorter.Shared.Implementations;
using System;
using System.Linq;

namespace NameSorter.Tests
{
    [TestClass]
    public class NameExtractionServiceTest
    {
        private INameExtractionService nameExtractionService;

        [TestInitialize]
        public void SetUp()
        {
            nameExtractionService = new NameExtractionService();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ExtractName_EmptyInput()
        {
            _ = nameExtractionService.ExtractName("");
        }

        [TestMethod]
        public void ExtractName_FirstnameOnly()
        {
            var name = nameExtractionService.ExtractName("Tirgari");
            Assert.IsFalse(name.Validate());
            Assert.IsTrue(string.IsNullOrEmpty(name.LastName));
            Assert.IsFalse(string.IsNullOrEmpty(name.FirstName));

            Assert.AreEqual(name.GivenNames.Count(), 2);
            Assert.AreEqual(name.GivenNames.Count(n => !string.IsNullOrEmpty(n)), 0);
        }

        [TestMethod]
        public void ExtractName_Firstname_And_Lastname()
        {
            var name = nameExtractionService.ExtractName("Kay Tirgari");
            Assert.IsTrue(name.Validate());
            Assert.IsFalse(string.IsNullOrEmpty(name.LastName));
            Assert.IsFalse(string.IsNullOrEmpty(name.FirstName));

            Assert.AreEqual(name.GivenNames.Count(), 2);
            Assert.AreEqual(name.GivenNames.Count(n => !string.IsNullOrEmpty(n)), 0);
        }

        [TestMethod]
        public void ExtractName_Firstname_Lastname_OneGivenName()
        {
            var name = nameExtractionService.ExtractName("Kay a Tirgari");
            Assert.IsTrue(name.Validate());
            Assert.IsFalse(string.IsNullOrEmpty(name.LastName));
            Assert.IsFalse(string.IsNullOrEmpty(name.FirstName));

            Assert.AreEqual(name.GivenNames.Count(), 2);
            Assert.AreEqual(name.GivenNames.Count(n => !string.IsNullOrEmpty(n)), 1);
        }

        [TestMethod]
        public void ExtractName_Firstname_Lastname_TwoGivenNames()
        {
            var name = nameExtractionService.ExtractName("Kay a b Tirgari");
            Assert.IsTrue(name.Validate());
            Assert.IsFalse(string.IsNullOrEmpty(name.LastName));
            Assert.IsFalse(string.IsNullOrEmpty(name.FirstName));

            Assert.AreEqual(name.GivenNames.Count(), 2);
            Assert.AreEqual(name.GivenNames.Count(n => !string.IsNullOrEmpty(n)), 2);
        }

        [TestMethod]
        public void ExtractName_InvalidInput_ThreeGivenNames()
        {
            var name = nameExtractionService.ExtractName("Kay a b c Tirgari");
            Assert.IsFalse(name.Validate());
            Assert.IsFalse(string.IsNullOrEmpty(name.LastName));
            Assert.IsFalse(string.IsNullOrEmpty(name.FirstName));

            Assert.AreEqual(name.GivenNames.Count(), 3);
            Assert.AreEqual(name.GivenNames.Count(n => !string.IsNullOrEmpty(n)), 3);
        }
    }
}
