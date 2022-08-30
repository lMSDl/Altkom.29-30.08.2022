using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static ConsoleApp.Logger;

namespace ConsoleApp.Test.xUnit
{
    public class LoggerTest
    {
        public void Log_AnyMessage_MessageLogged()
        {
            //TODO
        }

        /*[Fact]
        public void Log_AnyMessage_EventInvoked()
        {
            //Arrage
            var logger = new Logger();
            var anyMessage = "";
            var result = false;
            logger.MessageLogged += (sender, args) => result = true;

            //Act
            logger.Log(anyMessage);

            //Assert
            Assert.True(result);
        }*/

        /*[Fact]
        public void Log_AnyMessage_ValidEventInvoked()
        {
            //Arrage
            var logger = new Logger();
            var anyMessage = "";
            object eventSender = null;
            LoggerEventArgs? loggerEventArgs = null;
            logger.MessageLogged += (sender, args) => { loggerEventArgs = args; eventSender = sender; };

            //var timeBefore = DateTime.Now;
            //Act
            logger.Log(anyMessage);

            //Assert
            
            var timeAfter = DateTime.Now;
            //Assert.Equal(logger, eventSender);
            //Assert.Equal(anyMessage, loggerEventArgs.Message);
            //Assert.InRange(loggerEventArgs.DateTime, timeBefore, timeAfter);

            using (new AssertionScope())
            {
                eventSender.Should().Be(logger);
                loggerEventArgs.Message.Should().Be(anyMessage);
                loggerEventArgs.DateTime.Should().BeCloseTo(timeAfter, TimeSpan.FromMilliseconds(5));
            }
        }*/

        [Fact]
        public void Log_AnyMessage_ValidEventInvoked_FA()
        {
            //Arrage
            var logger = new Logger();
            using var monitor = logger.Monitor();
            var anyMessage = "";

            //Act
            logger.Log(anyMessage);

            //Assert
            monitor.Should().Raise(nameof(Logger.MessageLogged))
                .WithSender(logger);
        }
    }
}
