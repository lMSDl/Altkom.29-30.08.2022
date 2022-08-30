using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test.NUnit
{
    [TestFixture]
    public class SentenceUtilsTest
    {
        [Theory]
        [TestCase("a", "A")]
        [TestCase("A", "A")]
        [TestCase("Abc", "Abc")]
        [TestCase("ABC", "Abc")]
        [TestCase("abc", "Abc")]
        [TestCase("aBc", "Abc")]
        [TestCase("Abc ABC aBc AbC", "Abc Abc Abc Abc")]
        [TestCase(" Abc ABC aBc AbC  ", " Abc Abc Abc Abc  ")]
        [TestCase("1", "1")]
        public void ToTitleCase_AnyInput_FirstLetterUpperCase(string input, string output)
        {
            Assert.That(SentenceUtils.ToTitleCase(input),
                        Is.EqualTo(output));
        }

       /* [Test]
        public void Character()
        {
            Assert.That(SentenceUtils.ToTitleCase("a"),
                        Is.EqualTo("A"),
                        "Your function should convert a single character.");
            Assert.That(SentenceUtils.ToTitleCase("A"),
                        Is.EqualTo("A"),
                        "Your function should convert a single character.");
        }

        [Test]
        public void Word()
        {
            Assert.That(SentenceUtils.ToTitleCase("abc"),
                        Is.EqualTo("Abc"),
                        "Your function should convert a single word.");
            Assert.That(SentenceUtils.ToTitleCase("ABC"),
                        Is.EqualTo("Abc"),
                        "Your function should convert a single word.");
            Assert.That(SentenceUtils.ToTitleCase("aBC"),
                        Is.EqualTo("Abc"),
                        "Your function should convert a single word.");
            Assert.That(SentenceUtils.ToTitleCase("Abc"),
                        Is.EqualTo("Abc"),
                        "Your function should convert a single word.");
        }

        [Test]
        public void Sentence()
        {
            Assert.That(SentenceUtils.ToTitleCase("abc def ghi"),
                        Is.EqualTo("Abc Def Ghi"),
                        "Your function should convert a sentence.");
        }

        [Test]
        public void MultipleWhitespace()
        {
            Assert.That(SentenceUtils.ToTitleCase("  abc  def ghi "),
                        Is.EqualTo("  Abc  Def Ghi "),
                        "Your function should keep multiple whitespaces.");
        }*/
    }
}
