using Xunit;

namespace ConsoleApp.Test.xUnit
{
    public class GardenTest
    {
        [Fact]
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

        [Fact]
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

        [Fact]
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
        [Fact]
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
    }
}