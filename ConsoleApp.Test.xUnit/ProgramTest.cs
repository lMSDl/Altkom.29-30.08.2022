using ConsoleApp;

namespace ConsoleApp.Test.xUnit
{
    public class ProgramTest
    {
        [Fact]
        public void Main_HelloWorldOnOutput()
        {
            //Arrange
            var stringWriter = new StringWriter();
            var originalOutput = Console.Out;
            Console.SetOut(stringWriter);

            var entryPoint = typeof(Program).Assembly.EntryPoint;

            //Act
            entryPoint.Invoke(null, new object[] { Array.Empty<string>() });

            //Assert
            Console.SetOut(originalOutput);
            Assert.Equal("Hello, World!\n", stringWriter.ToString(), ignoreLineEndingDifferences: true);
            stringWriter.Dispose();
        }
    }
}