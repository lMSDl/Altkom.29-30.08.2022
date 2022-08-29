
namespace ConsoleApp.Test.MSTest
{
    [TestClass]
    public class GardenTest : IDisposable
    {

        private Garden Garden { get; set; }

        [TestInitialize]
        public void DefaultGarden()
        {
            Garden = new Garden(1);
        }


        [TestCleanup]
        public void Dispose()
        {

        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(int.MinValue)]
        public void Garden_InvalidSize_Exception(int invalidSize)
        {
            //Act
            Action result = () => new Garden(invalidSize);

            //Assert
            Assert.ThrowsException<ArgumentException>(result);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(int.MaxValue)]
        public void Garden_ValidSize_SizeInit(int validSize)
        {
            //Act
            var garden = new Garden(validSize);

            //Assert
            Assert.AreEqual(validSize, garden.Size);
        }

        [TestMethod]
        public void Garden_ValidSize_ListNotNull()
        {
            //Arrange
            int validSize = 1;

            //Act
            var garden = new Garden(validSize);

            //Assert
            Assert.IsNotNull(garden.GetPlants());
        }


        [TestMethod]
        [Ignore("replaced by Plant_ValidName_ExpectedResultIndicatingOverflow")]
        public void Plant_ValidAndUniqueName_True()
        {
            //Arrange
            var garden = new Garden(1);
            var validAndUniquePlantName = "a";
            //Act
            var result = garden.Plant(validAndUniquePlantName);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
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
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(1, false)]
        [DataRow(2, true)]
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
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Plant_WhitespaceName_ArgumentException()
        {
            //Arrange
            var gardern = new Garden(1);
            string whitespaceName = " ";

            //Act
            Action result = () => gardern.Plant(whitespaceName);

            //Assert
            var argumentNullException = Assert.ThrowsException<ArgumentException>(result);
            Assert.AreEqual("name", argumentNullException.ParamName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Plant_WhitespaceName_Exception()
        {
            //Arrange
            var gardern = new Garden(1);
            string whitespaceName = " ";

            //Act
            gardern.Plant(whitespaceName);
        }


        [TestMethod]
        public void Plant_ExistingName_ArgumentException()
        {
            //Arrange
            var gardern = new Garden(1);
            string validName = "a";
            gardern.Plant(validName);

            //Act
            Action result = () => gardern.Plant(validName);

            //Assert
            var argumentNullException = Assert.ThrowsException<ArgumentException>(result);
            Assert.AreEqual("name", argumentNullException.ParamName);
            Assert.IsTrue(argumentNullException.Message.Contains("Roœlina ju¿ istnieje w ogrodzie"));
        }

        [TestMethod]
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
            var argumentNullException = Assert.ThrowsException<ArgumentNullException>(result); //Throws sprawdza konkretn¹ klasê
            Assert.AreEqual("name", argumentNullException.ParamName);
        }

        [TestMethod]
        public void Plant_ValidUniqueName_AddedToGarden()
        {
            //Arrange
            var garden = new Garden(1);
            var validPlantName = "a";
            //Act
            garden.Plant(validPlantName);

            //Assert
            Assert.IsTrue(garden.GetPlants().Contains(validPlantName));
        }

        [TestMethod]
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
            Assert.IsFalse(garden.GetPlants().Contains(validPlantName2));
        }


        [TestMethod]
        public void Plant_CopyOfPlantsList()
        {
            //Arrange
            var garden = new Garden(1);

            //Act
            var list1 = garden.GetPlants();
            var list2 = garden.GetPlants();

            //Assert
            Assert.AreNotSame(list1, list2);
        }

    }
}