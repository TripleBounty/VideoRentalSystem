using Moq;
using NUnit.Framework;
using System;
using VideoRentalSystem.Common;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Tests.Common
{
    [TestFixture]
    public class GetFilmScore_ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullException_WhenInvalidGetFilmScoreIsPassed()
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new GetFilmScore(null));
        }

        [Test]
        public void NotThrow_WhenValidGetFilmScoreIsPassed()
        {
            //Arrange
            var databaseMock = new Mock<IDatabase>();

            //Act & Assert
            Assert.DoesNotThrow(() => new GetFilmScore(databaseMock.Object));
        }
    }
}
