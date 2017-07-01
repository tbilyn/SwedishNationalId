using System;
using Xunit;

namespace SwedishNationalId.Tests
{
    public class CinClassTests
    {
        [Fact]
        public void TestNormalCIN()
        {
            var nationaId = new Cin("5844208436");

            Assert.Equal(10, nationaId.ToString().Length);
            Assert.Equal("5844208436", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.False(nationaId.IsSSN);
            Assert.True(nationaId.IsCIN);

            nationaId = new Cin("584420-8436");

            Assert.Equal(10, nationaId.ToString().Length);
            Assert.Equal("5844208436", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.False(nationaId.IsSSN);
            Assert.True(nationaId.IsCIN);
        }

        [Fact]
        public void TestNormalSsnAsCin()
        {
            Assert.Throws<FormatException>(() => new Cin("193910318637"));
            Assert.Throws<FormatException>(() => new Cin("19391031-8637"));
            Assert.Throws<FormatException>(() => new Cin("3910318637"));
            Assert.Throws<FormatException>(() => new Cin("391031-8637"));
        }
    }
}
