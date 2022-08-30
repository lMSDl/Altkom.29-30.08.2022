﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test.NUnit
{
    [TestFixture]
    public class FizzBuzzTest
    {
        [Test]
        public void ReturnOne()
        {
            Assert.That(FizzBuzz.Compute(1),
                        Is.EqualTo(new List<string> { "1" }));
        }

        [Test]
        public void ReturnFifteen()
        {
            int n = 15;
            //List<string> result = FizzBuzz.Compute(n);
            List<string> expected = new List<string> {
                "1", "2", "Fizz", "4", "Buzz",
                "Fizz", "7", "8", "Fizz", "Buzz",
                "11", "Fizz", "13", "14", "FizzBuzz"
            };

            Assert.That(FizzBuzz.Compute(n),
                        Is.EqualTo(expected));

            /*Assert.That(result.Count,
                        Is.EqualTo(expected.Count),
                        string.Format("Your function should return {0} objects for n = {1}", n, n));

            for (int i = 0; i < n; i++)
            {
                Assert.That(result[i],
                            Is.EqualTo(expected[i]),
                            string.Format("Your function should convert the value {0} to {1} instead of {2}",
                                          i + 1, expected[i], result[i]));
            }*/
        }

        [Theory]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(100)]
        public void Compute_AnyInt_CorrentLength(int count)
        {
            //Act
            var result = FizzBuzz.Compute(count);

            //Assert
            Assert.That(result.Count, Is.EqualTo(count));
        }

        [Theory]
        [TestCase(1, "Buzz", 0)]
        [TestCase(15, "Buzz", 3)]
        [TestCase(100, "Buzz", 20)]
        [TestCase(1, "Fizz", 0)]
        [TestCase(15, "Fizz", 5)]
        [TestCase(100, "Fizz", 33)]
        public void Compute_AnyInt_CorrenctFizzCount(int count, string fizzBuzz, int expectedCount)
        {
            //Act
            var result = FizzBuzz.Compute(count);

            //Assert
            var fizzResult = result.Count(x => x.Contains(fizzBuzz));
            Assert.That(fizzResult, Is.EqualTo(expectedCount));
        }

        [Theory]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(1000)]
        public void Compute_AnyInt_NonFizzBuzzValues(int count)
        {
            //Act
            var result = FizzBuzz.Compute(count);

            //Assert
            var reference = Enumerable.Range(1, count).Select(x => x.ToString()).ToList();
            var zip = result.Zip(reference);
            Assert.IsTrue(zip.Where(x => !x.First.Contains("Fizz"))
                .Where(x => !x.First.Contains("Buzz"))
                .All(x => x.First == x.Second));
        }

    }
}
