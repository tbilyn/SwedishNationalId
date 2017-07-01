using System;
using Xunit;

namespace SwedishNationalId.Tests
{
    public class SsnClassTests
    {
        [Fact]
        public void TestNormalSSN()
        {
            var nationaId = new Ssn("193910318637");

            Assert.Equal(12, nationaId.ToString().Length);
            Assert.Equal("193910318637", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.True(nationaId.IsSSN);
            Assert.False(nationaId.IsCIN);

            nationaId = new Ssn("19391031-8637");

            Assert.Equal(12, nationaId.ToString().Length);
            Assert.Equal("193910318637", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.True(nationaId.IsSSN);
            Assert.False(nationaId.IsCIN);
        }

        [Fact]
        public void TestNormalCinAsSsn()
        {
            Assert.Throws<FormatException>(() => new Ssn("5844208436"));
            Assert.Throws<FormatException>(() => new Ssn("584420-8436"));
        }
    }
}
