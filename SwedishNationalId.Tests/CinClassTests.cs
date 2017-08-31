using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SwedishNationalId.Tests
{
    public class CinClassTests
    {
        [Fact]
        public void TestNormalCIN()
        {
            var nationaId = new OrganisationNumber("5844208436");

            Assert.Equal(10, nationaId.ToString().Length);
            Assert.Equal("5844208436", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.False(nationaId.IsSSN);
            Assert.True(nationaId.IsCIN);

            nationaId = new OrganisationNumber("584420-8436");

            Assert.Equal(10, nationaId.ToString().Length);
            Assert.Equal("5844208436", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.False(nationaId.IsSSN);
            Assert.True(nationaId.IsCIN);
        }

        [Fact]
        public void TestNormalSsnAsCin()
        {
            Assert.Throws<FormatException>(() => new OrganisationNumber("193910318637"));
            Assert.Throws<FormatException>(() => new OrganisationNumber("19391031-8637"));
            Assert.Throws<FormatException>(() => new OrganisationNumber("3910318637"));
            Assert.Throws<FormatException>(() => new OrganisationNumber("391031-8637"));
        }

        [Fact]
        public void TestEqual()
        {
            var one = new NationalId("5844208436");
            var two = new OrganisationNumber("584420-8436");

            var actual = one == two;

            Assert.True(actual);
        }

        [Fact]
        public void TestDistinct()
        {
            var list = new List<NationalId> { new OrganisationNumber("5844208436"), new NationalId("584420-8436") };

            var distinctItems = list.Distinct().ToList();

            Assert.Equal(1, distinctItems.Count);
        }
    }
}
