using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Common;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Data.Repository.Contracts;
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

        [Test]
        public void ShouldReturn_AverageFilmScoreFromAllReviews()
        {
            //Arrange
            var validFilmName = "validFilmName";

            var filmMock = new Mock<Film>();
            filmMock.Object.Id = 1;
            filmMock.Object.Name = validFilmName;

            var review1Mock = new Mock<Review>();
            review1Mock.SetupGet(x => x.Film).Returns(filmMock.Object);
            review1Mock.Object.Rating = 3.5;

            var review2Mock = new Mock<Review>();
            review2Mock.SetupGet(x => x.Film).Returns(filmMock.Object);
            review2Mock.Object.Rating = 7.5;

            var review3Mock = new Mock<Review>();
            review3Mock.SetupGet(x => x.Film).Returns(filmMock.Object);
            review3Mock.Object.Rating = 4.0;

            var reviewCollection = new HashSet<Review>();
            reviewCollection.Add(review1Mock.Object);
            reviewCollection.Add(review2Mock.Object);
            reviewCollection.Add(review3Mock.Object);

            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(x => x.GetAll()).Returns(reviewCollection);

            var databaseMock = new Mock<IDatabase>();
            databaseMock.SetupGet(x => x.Reviews).Returns(reviewRepository.Object);

            databaseMock.Setup(x => x.Films.SingleOrDefault(It.IsAny<Expression<Func<Film, bool>>>())).Returns(filmMock.Object);

            var sut = new GetFilmScore(databaseMock.Object);

            //Act
            var result = sut.GetAvgFilmScore(validFilmName);

            //Assert
            Assert.AreEqual(5.0, result);
        }
    }
}
