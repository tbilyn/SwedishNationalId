using System;
using Xunit;

namespace SwedishNationalId.Tests
{
    public class NationalIdClassTests
    {
        [Fact]
        public void TestNormaSSN()
        {
            // Format YYYYMMDDCCCC
            NationalId nationaId = new NationalId("193910318637");

            Assert.Equal(12, nationaId.ToString().Length);
            Assert.Equal("193910318637", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.True(nationaId.IsSSN);
            Assert.False(nationaId.IsCIN);
            

            // Format YYYYMMDD-CCCC
            nationaId = new NationalId("19391031-8637");

            Assert.Equal(12, nationaId.ToString().Length);
            Assert.Equal("193910318637", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.True(nationaId.IsSSN);
            Assert.False(nationaId.IsCIN);

            // Format YYMMDDCCCC
            nationaId = new NationalId("3910318637");

            Assert.Equal(12, nationaId.ToString().Length);
            Assert.Equal("193910318637", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.True(nationaId.IsSSN);
            Assert.False(nationaId.IsCIN);


            // Format YYMMDD-CCCC
            nationaId = new NationalId("391031-8637");

            Assert.Equal(12, nationaId.ToString().Length);
            Assert.Equal("193910318637", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.True(nationaId.IsSSN);
            Assert.False(nationaId.IsCIN);

        }

        [Fact]
        public void TestNormaCIN()
        {
            // Format YYMMDDCCCC
            NationalId nationaId = new NationalId("5844208436");
            Assert.Equal("5844208436", nationaId.ToString());
            Assert.Equal(10, nationaId.ToString().Length);
            Assert.True(nationaId.IsValid());
            Assert.False(nationaId.IsSSN);
            Assert.True(nationaId.IsCIN);

            // Format YYMMDD-CCCC
            nationaId = new NationalId("584420-8436");
            Assert.Equal("5844208436", nationaId.ToString());
            Assert.Equal(10, nationaId.ToString().Length);
            Assert.True(nationaId.IsValid());
            Assert.False(nationaId.IsSSN);
            Assert.True(nationaId.IsCIN);
        }

        [Fact]
        public void TestWrong()
        {
            Assert.Throws<FormatException>(() => new NationalId(""));

            Assert.Throws<FormatException>(() => new NationalId("19391031863"));
            Assert.Throws<FormatException>(() => new NationalId("93910318637"));
            Assert.Throws<FormatException>(() => new NationalId("19391031--8637"));
            Assert.Throws<FormatException>(() => new NationalId("193920318637")); // wrong month for ssn

            Assert.Throws<FormatException>(() => new NationalId("584420843"));
            Assert.Throws<FormatException>(() => new NationalId("844208436"));
            Assert.Throws<FormatException>(() => new NationalId("584420--8436"));            
        }

        [Fact]
        public void TestNormaSSNFor21Century()
        {
            // Format YYMMDDCCCC
            var nationaId = new NationalId("1510318637");

            Assert.Equal(12, nationaId.ToString().Length);
            Assert.Equal("201510318637", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.True(nationaId.IsSSN);
            Assert.False(nationaId.IsCIN);


            // Format YYMMDD-CCCC
            nationaId = new NationalId("151031-8637");

            Assert.Equal(12, nationaId.ToString().Length);
            Assert.Equal("201510318637", nationaId.ToString());
            Assert.True(nationaId.IsValid());
            Assert.True(nationaId.IsSSN);
            Assert.False(nationaId.IsCIN);
            
        }

    }
}
