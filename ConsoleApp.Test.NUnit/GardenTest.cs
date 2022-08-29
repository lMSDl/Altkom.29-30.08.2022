using NUnit;

namespace ConsoleApp.Test.NUnit
{
    [TestFixture]
    public class GardenTest : IDisposable
    {

        private Garden Garden { get; set; }

        [SetUp]
        public void DefaultGarden()
        {
            Garden = new Garden(1);
        }


        [TearDown]
        public void Dispose()
        {

        }

        [Theory]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void Garden_InvalidSize_Exception(int invalidSize)
        {
            //Act
            TestDelegate result = () => new Garden(invalidSize);

            //Assert
            Assert.Throws<ArgumentException>(result);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(int.MaxValue)]
        public void Garden_ValidSize_SizeInit(int validSize)
        {
            //Act
            var garden = new Garden(validSize);

            //Assert
            Assert.That(garden.Size, Is.EqualTo(validSize));
        }

        [Test]
        public void Garden_ValidSize_ListNotNull()
        {
            //Arrange
            int validSize = 1;

            //Act
            var garden = new Garden(validSize);

            //Assert
            Assert.NotNull(garden.GetPlants());
        }


        [Test]
        [Ignore("replaced by Plant_ValidName_ExpectedResultIndicatingOverflow")]
        public void Plant_ValidAndUniqueName_True()
        {
            //Arrange
            var garden = new Garden(1);
            var validAndUniquePlantName = "a";
            //Act
            var result = garden.Plant(validAndUniquePlantName);

            //Assert
            Assert.True(result);
        }

        [Test]
        [Ignore("replaced by Plant_ValidName_ExpectedResultIndicatingOverflow")]
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
        [TestCase(1, false)]
        [TestCase(2, true)]
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
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [Ignore("replaced by Plant_InvalidName_ArgumentException")]
        public void Plant_WhitespaceName_ArgumentException()
        {
            //Arrange
            var gardern = new Garden(1);
            string whitespaceName = " ";

            //Act
            TestDelegate result = () => gardern.Plant(whitespaceName);

            //Assert
            var argumentNullException = Assert.Throws<ArgumentException>(result);
            Assert.That(argumentNullException.ParamName, Is.EqualTo("name"));
        }

        [Theory]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Plant_InvalidName_ArgumentException(string invalidName)
        {
            //Arrange
            var gardern = new Garden(1);

            //Act
            TestDelegate result = () => gardern.Plant(invalidName);

            //Assert
            var argumentNullException = (ArgumentException)Assert.Throws(Is.InstanceOf<ArgumentException>(), result)!;
            Assert.That(argumentNullException.ParamName, Is.EqualTo("name"));

            Assert.Throws(Is.InstanceOf<ArgumentException>()
                            .And.Property("ParamName").EqualTo("name"), result);
        }

        [Test]
        public void Plant_ExistingName_ArgumentException()
        {
            //Arrange
            var gardern = new Garden(1);
            string validName = "a";
            gardern.Plant(validName);

            //Act
            TestDelegate result = () => gardern.Plant(validName);

            //Assert
            var argumentNullException = Assert.Throws<ArgumentException>(result);
            Assert.That(argumentNullException.ParamName, Is.EqualTo("name"));
            Assert.That(argumentNullException.Message, Does.Contain("Roœlina ju¿ istnieje w ogrodzie"));
        }

        [Test]
        [Ignore("replaced by Plant_InvalidName_ArgumentException")]
        public void Plant_NullName_ArgumentNullException()
        {
            //Arrange
            var gardern = new Garden(1);
            string nullName = null;

            //Act & Assert
            //Assert.Throws<ArgumentNullException>(() => gardern.Plant(nullName));

            //Act
            TestDelegate result = () => gardern.Plant(nullName);

            //Assert
            var argumentNullException = Assert.Throws<ArgumentNullException>(result); //Throws sprawdza konkretn¹ klasê
            Assert.That(argumentNullException.ParamName, Is.EqualTo("name"));
        }

        [Test]
        public void Plant_ValidUniqueName_AddedToGarden()
        {
            //Arrange
            var garden = new Garden(1);
            var validPlantName = "a";
            //Act
            garden.Plant(validPlantName);

            //Assert
            Assert.That(garden.GetPlants(), Does.Contain(validPlantName));
        }

        [Test]
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
            Assert.That(garden.GetPlants(), Does.Not.Contain(validPlantName2));
        }


        [Test]
        public void Plant_CopyOfPlantsList()
        {
            //Arrange
            var garden = new Garden(1);

            //Act
            var list1 = garden.GetPlants();
            var list2 = garden.GetPlants();

            //Assert
            Assert.That(list1, Is.Not.SameAs(list2));
        }

    }
}