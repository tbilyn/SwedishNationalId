using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void TestEqual()
        {
            var one = new NationalId("197910318637");
            var two = new Ssn("791031-8637");

            var actual = one == two;

            Assert.True(actual);
        }

        [Fact]
        public void TestDistinct()
        {
            var list = new List<NationalId> { new Ssn("197910318637"), new NationalId("791031-8637") };

            var distinctItems = list.Distinct().ToList();

            Assert.Equal(1, distinctItems.Count);
        }
    }
}
