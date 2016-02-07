using System.Net.Http;
using System.Threading.Tasks;
using Common.Infrastructure.Api;
using log4net;
using Moq;
using NUnit.Framework;

namespace Common.UnitTest.Infrastructure.Api
{
    [TestFixture]
    public class ApiTest
    {
        private string BaseAddress { get; } = "http://api.ratings.food.gov.uk/";

        [Test]
        public void Api_GetAsync_Invalid_Url_Throw_HttpRequestException()
        {
            var api = new Api<EmtpyTestClass>(new Mock<ILog>().Object) {BaseApiUrl = "http://testWrongUri"};
            Assert.That(() => api.GetAsync("AnyUri"), Throws.TypeOf<HttpRequestException>());
            Assert.That(() => api.GetAsync("AnyUri"), Throws.TypeOf<HttpRequestException>());
        }
        [Test]
        public void Api_GetAsync_Invalid_Json_Response_Throw_NotAcceptableException()
        {
            var api = new Api<EmtpyTestClass>(new Mock<ILog>().Object) {BaseApiUrl = "http://www.google.co.uk/"};
            Assert.That(() => api.GetAsync("AnyUri"), Throws.TypeOf<HttpRequestException>().And.Not.Message.Empty);
        }
        
    }

    public abstract class EmtpyTestClass
    { }
}