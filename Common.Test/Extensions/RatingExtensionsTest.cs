using System.Net.Http;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Infrastructure.Api;
using log4net;
using Moq;
using NUnit.Framework;

namespace Common.UnitTest.Extensions
{
    [TestFixture]
    public class RatingExtensionsTest
    {
        [Test]
        public void Extensions_RatingExtension_RoundPercentage_Test()
        {
            Assert.AreEqual(10.22, (0.10222m).RoundPercentage());
        }
        [Test]
        public void Extensions_RatingExtension_GetStarName_Test()
        {
            Assert.AreEqual("1 - Star", ("1").GetStarName());
            Assert.AreEqual("2 - Star", ("2").GetStarName());
            Assert.AreEqual("Test", ("Test").GetStarName());
            Assert.AreEqual("0.000", ("0.000").GetStarName());
            Assert.AreEqual("", ("").GetStarName());
        }
       
    }
}