using Moq;
using NUnit.Framework;
using System;
using VideoRentalSystem.Common.Contracts;
using VideoRentalSystem.Core;

namespace VideoRentalSystem.Tests.Core
{
    [TestFixture]
    public class Engine_StartShould
    {
        [Test]
        public void StarShould_CallReadLineOfTheReader()
        {
            //Arange
            var readerMock = new Mock<IReader>();
            var writerMock = new Mock<IWriter>();
            var processorMock = new Mock<IProcessor>();

            readerMock.Setup(r => r.ReadLine()).Returns("Exit");

            var engine = new Engine(readerMock.Object, writerMock.Object, processorMock.Object);

            //Act
            engine.Start();

            //Assert
            readerMock.Verify(v => v.ReadLine(), Times.Once);
        }

        [TestCase("exit")]
        [TestCase("Exit")]
        [TestCase("EXIT")]
        public void StarShould_CallWriteLineOfTheWriter(string command)
        {
            //Arange
            var readerMock = new Mock<IReader>();
            var writerMock = new Mock<IWriter>();
            var processorMock = new Mock<IProcessor>();

            readerMock.Setup(r => r.ReadLine()).Returns(command);

            var engine = new Engine(readerMock.Object, writerMock.Object, processorMock.Object);

            //Act
            engine.Start();

            //Assert
            writerMock.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void StarShould_CallWriteLineOfTheWriterWithProgramTerminated()
        {
            //Arange
            var message = "Program terminated.";
            var readerMock = new Mock<IReader>();
            var writerMock = new Mock<IWriter>();
            var processorMock = new Mock<IProcessor>();

            readerMock.Setup(r => r.ReadLine()).Returns("exit");

            var engine = new Engine(readerMock.Object, writerMock.Object, processorMock.Object);

            //Act
            engine.Start();

            //Assert
            writerMock.Verify(w => w.WriteLine(message), Times.Once);
        }

        [Test]
        public void StarShould_CallProcessCommandOfTheProcessor_WhenValidCommandIsPassed()
        {
            //Arange
            var command = "someValidCommand";
            var readerMock = new Mock<IReader>();
            var writerMock = new Mock<IWriter>();
            var processorMock = new Mock<IProcessor>();

            readerMock.SetupSequence(r => r.ReadLine()).Returns(command)
                                                       .Returns("exit");
            var engine = new Engine(readerMock.Object, writerMock.Object, processorMock.Object);

            //Act
            engine.Start();

            //Assert
            processorMock.Verify(p => p.ProcessCommand(command), Times.Once);
        }

        [Test]
        public void StarShould_WriteTheResultOfTheCommandExecution_WhenValidCommandIsPassed()
        {
            //Arange
            var command = "someValidCommand";
            var readerMock = new Mock<IReader>();
            var writerMock = new Mock<IWriter>();
            var processorMock = new Mock<IProcessor>();

            readerMock.SetupSequence(r => r.ReadLine()).Returns(command)
                                                       .Returns("exit");
            var executionResult = "Some command result";
            processorMock.Setup(p => p.ProcessCommand(It.IsAny<string>())).Returns(executionResult);

            var engine = new Engine(readerMock.Object, writerMock.Object, processorMock.Object);

            //Act
            engine.Start();

            //Assert
            writerMock.Verify(w => w.WriteLine(executionResult), Times.Once);
        }

        [Test]
        public void StarShould_WriteSpecificMessage_WhenParceCommandThrowException()
        {
            //Arange
            var errorMessage = "errorMessage";
            var specificMessage = "something happened";
            var readerMock = new Mock<IReader>();
            var writerMock = new Mock<IWriter>();
            var processorMock = new Mock<IProcessor>();

            readerMock.SetupSequence(r => r.ReadLine()).Returns("InvalidCommand")
                                                       .Returns("exit");

            processorMock.Setup(p => p.ProcessCommand(It.IsAny<string>())).Throws(new Exception(errorMessage));

            var engine = new Engine(readerMock.Object, writerMock.Object, processorMock.Object);

            //Act
            engine.Start();

            //Assert
            writerMock.Verify(w => w.WriteLine(It.Is<string>(str => str.Contains(specificMessage))), Times.Once);
            writerMock.Verify(w => w.WriteLine(It.Is<string>(str => str.Contains(errorMessage))), Times.Once);
        }
    }
}
