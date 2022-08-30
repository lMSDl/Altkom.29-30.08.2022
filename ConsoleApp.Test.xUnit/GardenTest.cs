using FluentAssertions;
using Moq;
using System.Diagnostics;
using Xunit;

namespace ConsoleApp.Test.xUnit
{
    public class GardenTest : IDisposable
    {

        private Garden Garden { get; }

        //Odpowiednik SetUp [BadPractise]
        public GardenTest()
        {
            Garden = new Garden(1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Garden_InvalidSize_Exception(int invalidSize)
        {
            //Act
            Action result = () => new Garden(invalidSize);

            //Assert
            Assert.ThrowsAny<ArgumentException>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void Garden_ValidSize_SizeInit(int validSize)
        {
            //Act
            var garden = new Garden(validSize);

            //Assert
            Assert.Equal(validSize, garden.Size);
        }

        [Fact]
        public void Garden_ValidSize_ListNotNull()
        {
            //Arrange
            int validSize = 1;

            //Act
            var garden = new Garden(validSize);

            //Assert
            Assert.NotNull(garden.GetPlants());
        }


        [Fact(Skip = "replaced by Plant_ValidName_ExpectedResultIndicatingOverflow")]
        //public void Plant_<scenatio>_<result>()
        //public void Plant_<scenatio&result>()
        //public void Plant_PassValidAndUniqueName_ReturnsTrue()
        public void Plant_ValidAndUniqueName_True()
        //public void Plant_ValidAndUniqueNameShallReturnTrue()
        {
            //Arrange
            var garden = new Garden(1);
            var validAndUniquePlantName = "a";
            //Act
            var result = garden.Plant(validAndUniquePlantName);

            //Assert
            Assert.True(result);
        }

        [Fact(Skip = "replaced by Plant_ValidName_ExpectedResultIndicatingOverflow")]
        public void Plant_GardenOverflow_False()
        {
            //Arrange
            var garden = new Garden(1);
            var validPlantName1 = "a";
            var validPlantName2 = "b";
            garden.Plant(validPlantName1);

            //Act
            var result = garden.Plant(validPlantName2);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        public void Plant_ValidName_ExpectedResultIndicatingOverflow(int gardenSize, bool expectedResult)
        {
            //Arrange
            var garden = new Garden(gardenSize);
            var validPlantName1 = "a";
            var validPlantName2 = "b";
            garden.Plant(validPlantName1);

            //Act
            var result = garden.Plant(validPlantName2);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact(Skip = "replaced by Plant_InvalidName_ArgumentException")]
        public void Plant_WhitespaceName_ArgumentException()
        {
            //Arrange
            var gardern = new Garden(1);
            string whitespaceName = " ";

            //Act
            Action result = () => gardern.Plant(whitespaceName);

            //Assert
            var argumentNullException = Assert.Throws<ArgumentException>(result);
            Assert.Equal("Name", argumentNullException.ParamName, ignoreCase: true);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Plant_InvalidName_ArgumentException(string invalidName)
        {
            //Arrange
            var gardern = new Garden(1);

            //Act
            Action result = () => gardern.Plant(invalidName);

            //Assert
            var argumentNullException = Assert.ThrowsAny<ArgumentException>(result);
            Assert.Equal("Name", argumentNullException.ParamName, ignoreCase: true);
        }

        [Fact]
        public void Plant_ExistingName_ArgumentException()
        {
            //Arrange
            var gardern = new Garden(1);
            string validName = "a";
            gardern.Plant(validName);

            //Act
            Action result = () => gardern.Plant(validName);

            //Assert
            var argumentNullException = Assert.Throws<ArgumentException>(result);
            Assert.Equal("Name", argumentNullException.ParamName, ignoreCase: true);
            Assert.Contains("Roœlina ju¿ istnieje w ogrodzie", argumentNullException.Message);
        }

        [Fact(Skip = "replaced by Plant_InvalidName_ArgumentException")]
        public void Plant_NullName_ArgumentNullException()
        {
            //Arrange
            var gardern = new Garden(1);
            string nullName = null;

            //Act & Assert
            //Assert.Throws<ArgumentNullException>(() => gardern.Plant(nullName));

            //Act
            Action result = () => gardern.Plant(nullName);

            //Assert
            var argumentNullException = Assert.Throws<ArgumentNullException>(result); //Throws sprawdza konkretn¹ klasê
            //var argumentNullException = Assert.ThrowsAny<ArgumentException>(result); //ThrowsAny sprawdza hierarchiê dziedziczenai
            Assert.Equal("Name", argumentNullException.ParamName, ignoreCase: true);
            //Dodatkowa assertacja - sprawdza ca³y czas ten sam rezultat
        }

        [Fact]
        public void Plant_ValidUniqueName_AddedToGarden()
        {
            //Arrange
            var garden = new Garden(1);
            var validPlantName = "a";
            //Act
            garden.Plant(validPlantName);

            //Assert
            Assert.Contains(validPlantName, garden.GetPlants());
        }

        [Fact]
        public void Plant_GardenOverflow_NotAddedToGarden()
        {
            //Arrange
            var garden = new Garden(1);
            var validPlantName1 = "a";
            var validPlantName2 = "b";
            garden.Plant(validPlantName1);

            //Act
            garden.Plant(validPlantName2);

            //Assert
            Assert.DoesNotContain(validPlantName2, garden.GetPlants());
        }


        [Fact]
        public void Plant_CopyOfPlantsList()
        {
            //Arrange
            var loggerStub = new Mock<ILogger>();
            var garden = new Garden(1, loggerStub.Object);

            //Act
            var list1 = garden.GetPlants();
            var list2 = garden.GetPlants();

            //Assert
            Assert.NotSame(list1, list2);
        }


        //Odpowiednik TearDown [BadPractise]
        public void Dispose()
        {

        }

        [Fact]
        public void Plant_ValidUniqueName_MessageLogged()
        {
            //Arrage
            var loggerMock = new Mock<ILogger>();
            //logger.Setup(x => x.Log(It.IsAny<string>())).Verifiable();

            var gardern = new Garden(1, loggerMock.Object);
            var name = "a";

            //Act
            gardern.Plant(name);

            //Assert
            //logger.Verify(x => x.Log($"Roœlina {name} zosta³a dodana do ogrodu"));
            loggerMock.Verify(x => x.Log(It.IsAny<string>()), Times.Once);

            //logger.Verify();
        }

        [Fact]
        public void ShowLastLog_LastLog()
        {
            //Arrage
            var name1 = "a";
            var name2 = "b";


            var loggerStub = new Mock<ILogger>();
            loggerStub.Setup(x => x.GetLogs(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns($"{name1}\n{name2}");


            var gardern = new Garden(1, loggerStub.Object);
            gardern.Plant(name1);
            gardern.Plant(name2);

            //Act
            var result = gardern.ShowLastLog();

            //Assert
            result.Should().Be(name2);
        }
    }
}