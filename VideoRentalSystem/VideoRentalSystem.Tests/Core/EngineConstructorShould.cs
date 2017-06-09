using Moq;
using NUnit.Framework;
using System;
using VideoRentalSystem.Common.Contracts;
using VideoRentalSystem.Core;

namespace VideoRentalSystem.Tests.Core
{
    [TestFixture]
    public class EngineConstructorShould
    {
        [Test]
        public void NotThrowArgumentNullException_WhenParametersAreProvided()
        {
            //Arange
            var readerMock = new Mock<IReader>();
            var writerMock = new Mock<IWriter>();
            var processorMock = new Mock<IProcessor>();

            //Act & Assert
            Assert.DoesNotThrow(() => new Engine(readerMock.Object, writerMock.Object, processorMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenInvalidReaderIsProvided()
        {
            //Arange
            var writerMock = new Mock<IWriter>();
            var processorMock = new Mock<IProcessor>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Engine(null, writerMock.Object, processorMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenInvalidWriterIsProvided()
        {
            //Arange
            var readerMock = new Mock<IReader>();
            var processorMock = new Mock<IProcessor>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Engine(readerMock.Object, null, processorMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenInvalidProcessorIsProvided()
        {
            //Arange
            var writerMock = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Engine(readerMock.Object, writerMock.Object, null));
        }
    }
}
