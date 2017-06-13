using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Common;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Tests.Common
{
    [TestFixture]
    public class GetFilmScore_GetAvgScoreShould
    {
        [TestCase(null)]
        [TestCase("")]
        public void ThrowException_WhenNullOrEmptyFilmIsPassed(string film)
        {
            //Arrange
            var databaseMock = new Mock<IDatabase>();
            var sut = new GetFilmScore(databaseMock.Object);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => sut.GetAvgFilmScore(film));
        }

        [Test]
        public void ThrowException_WhenFilmNotFound()
        {
            //Arrange
            var validFilmName = "validFilmName";
            
            var databaseMock = new Mock<IDatabase>();
            databaseMock.Setup(x => x.Films.SingleOrDefault(y => y.Name == validFilmName)).Returns<Film>(null);

            var sut = new GetFilmScore(databaseMock.Object);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => sut.GetAvgFilmScore(validFilmName));
        }

        //[Test]
        public void ShouldReturn_AverageFilmScoreFromAllReviews()
        {
            //Arrange
            var validFilmName = "validFilmName";

            var databaseMock = new Mock<IDatabase>();
            var filmMock = new Mock<Film>();
            filmMock.SetupGet(x => x.Id).Returns(1);

            var reviewsMock = new Mock<Review>();
            databaseMock.Setup(x => x.Films.SingleOrDefault(y => y.Name == validFilmName)).Returns<Film>(null);
        }
    }
}
