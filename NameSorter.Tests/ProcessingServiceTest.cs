using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NameSorter.Shared.Abstractions;
using NameSorter.Shared.Implementations;
using NameSorter.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NameSorter.Tests
{
    [TestClass]
    public class ProcessingServiceTest
    {
        private Mock<ISortingService> sortingService;

        private Mock<IInputFileService> inputFileService;

        private Mock<ILoggingService> loggingService;

        private IProcessingService processingService;

        [TestInitialize]
        public void SetUp()
        {
            sortingService = new Mock<ISortingService>();
            inputFileService = new Mock<IInputFileService>();
            loggingService = new Mock<ILoggingService>();

            processingService = new ProcessingService(
                sortingService.Object,
                inputFileService.Object,
                loggingService.Object);
        }

        [TestMethod]
        public async Task Process_NullList()
        {
            List<NameModel> list = null;
            inputFileService
                .Setup(s => s.ReadAsync(It.IsAny<string>()))
                .ReturnsAsync(list);

            await processingService.ProcessAsync("");

            inputFileService.Verify(
                s => s.ReadAsync(It.IsAny<string>()),
                Times.Once);

            loggingService.Verify(
                s => s.LogError(It.IsAny<string>()),
                Times.Once);

            sortingService.Verify(
                s => s.Sort(It.IsAny<List<NameModel>>()),
                Times.Never);
        }

        [TestMethod]
        public async Task Process_EmptyList()
        {
            inputFileService
                .Setup(s => s.ReadAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<NameModel>());

            await processingService.ProcessAsync("");

            inputFileService.Verify(
                s => s.ReadAsync(It.IsAny<string>()),
                Times.Once);

            loggingService.Verify(
                s => s.LogError(It.IsAny<string>()),
                Times.Once);

            sortingService.Verify(
                s => s.Sort(It.IsAny<List<NameModel>>()),
                Times.Never);
        }

        [TestMethod]
        public async Task Process_ListWithInvalidItems_1()
        {
            var list = new List<NameModel>
            {
                new NameModel
                {
                    FirstName = "name"
                }
            };

            inputFileService
                .Setup(s => s.ReadAsync(It.IsAny<string>()))
                .ReturnsAsync(list);

            await processingService.ProcessAsync("");

            inputFileService.Verify(
                s => s.ReadAsync(It.IsAny<string>()),
                Times.Once);

            loggingService.Verify(
                s => s.LogError(It.IsAny<string>()),
                Times.Once);

            sortingService.Verify(
                s => s.Sort(It.IsAny<List<NameModel>>()),
                Times.Never);
        }

        [TestMethod]
        public async Task Process_ListWithInvalidItems_2()
        {
            var list = new List<NameModel>
            {
                new NameModel
                {
                    FirstName = "testFirstname",
                    LastName = "testLastname",
                    GivenNames = new List<string>
                    {
                        "1",
                        "2",
                        "3"
                    }
                }
            };

            inputFileService
                .Setup(s => s.ReadAsync(It.IsAny<string>()))
                .ReturnsAsync(list);

            await processingService.ProcessAsync("");

            inputFileService.Verify(
                s => s.ReadAsync(It.IsAny<string>()),
                Times.Once);

            loggingService.Verify(
                s => s.LogError(It.IsAny<string>()),
                Times.Once);

            sortingService.Verify(
                s => s.Sort(It.IsAny<List<NameModel>>()),
                Times.Never);
        }

        [TestMethod]
        public async Task Process_ValidList()
        {
            var list = new List<NameModel>
            {
                new NameModel
                {
                    FirstName = "testFirstname1",
                    LastName = "testLastname1",
                    GivenNames = new List<string>
                    {
                        "1",
                        "3"
                    }
                },
                new NameModel
                {
                    FirstName = "testFirstname2",
                    LastName = "testLastname2",
                }
            };

            inputFileService
                .Setup(s => s.ReadAsync(It.IsAny<string>()))
                .ReturnsAsync(list);

            await processingService.ProcessAsync("");

            inputFileService.Verify(
                s => s.ReadAsync(It.IsAny<string>()),
                Times.Once);

            sortingService.Verify(
                s => s.Sort(It.IsAny<List<NameModel>>()),
                Times.Once);

            inputFileService.Verify(
                s => s.WriteAsync(It.IsAny<string>(), It.IsAny<List<NameModel>>()),
                Times.Once);

            loggingService.Verify(
                s => s.LogError(It.IsAny<string>()),
                Times.Never);
        }
    }
}
