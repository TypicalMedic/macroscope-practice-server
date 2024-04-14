using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ServerSide;
using System.Text.Json.Nodes;

namespace ServerSideTests.IntergationTests.Controllers
{
    [TestClass]
    public class PalindromeControllerTests
    {
        // интеграционные тесты должны быть выполнены при работающем сервере
        private readonly HttpClient _client = new HttpClient();
        private const int MaxConnections = 5;
        private const string ServerUrl = "http://localhost:5015";
        private const string CheckPalindromeEndpointUrl = "/palindrome/check";

        public PalindromeControllerTests()
        {
            _client.BaseAddress = new Uri(ServerUrl);
        }
        [TestMethod]
        public async Task NullReqBody_BadReq()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, CheckPalindromeEndpointUrl);

            var responce = await _client.SendAsync(request);

            Assert.AreEqual(responce.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task InvalidReqBody_BadReq()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, CheckPalindromeEndpointUrl)
            {
                Content = new StringContent("{\"value\":1}", Encoding.UTF8, "application/json")
            };

            var responce = await _client.SendAsync(request);

            Assert.AreEqual(responce.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task ValidReqBodyEmpty_True()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, CheckPalindromeEndpointUrl)
            {
                Content = new StringContent("\"\"", Encoding.UTF8, "application/json")
            };

            var responce = await _client.SendAsync(request);

            Assert.AreEqual(responce.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(responce.Content);
            Assert.AreEqual(await responce.Content.ReadAsStringAsync(), "true");
        }

        [TestMethod]
        public async Task ValidReqBodyPalindrome_True()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, CheckPalindromeEndpointUrl)
            {
                Content = new StringContent("\"MdaRsrAdm\"", Encoding.UTF8, "application/json")
            };

            var responce = await _client.SendAsync(request);

            Assert.AreEqual(responce.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(responce.Content);
            Assert.AreEqual(await responce.Content.ReadAsStringAsync(), "true");
        }

        [TestMethod]
        public async Task ValidReqBodyNotPalindrome_False()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, CheckPalindromeEndpointUrl)
            {
                Content = new StringContent("\"Md1aRsrAd3m\"", Encoding.UTF8, "application/json")
            };

            var responce = await _client.SendAsync(request);

            Assert.AreEqual(responce.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(responce.Content);
            Assert.AreEqual(await responce.Content.ReadAsStringAsync(), "false");
        }
        [TestMethod]
        public async Task ValidReqBodyPalindromeWithSymbols_True()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, CheckPalindromeEndpointUrl)
            {
                Content = new StringContent("\"Md- aR s.r)Adm'\"", Encoding.UTF8, "application/json")
            };

            var responce = await _client.SendAsync(request);

            Assert.AreEqual(responce.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(responce.Content);
            Assert.AreEqual(await responce.Content.ReadAsStringAsync(), "true");
        }

        [TestMethod]
        public async Task ValidReqBodyNotPalindromeWithSymbols_False()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, CheckPalindromeEndpointUrl)
            {
                Content = new StringContent("\"M d12aRs rAd^3 m\"", Encoding.UTF8, "application/json")
            };

            var responce = await _client.SendAsync(request);

            Assert.AreEqual(responce.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(responce.Content);
            Assert.AreEqual(await responce.Content.ReadAsStringAsync(), "false");
        }

        [TestMethod]
        public async Task ValidReqBodyTooManyrequests_200and503()
        {
            // Тест может успеть послать все запросы успешно, если сервер имеет минимальное время обработки
            Random random = new Random();
            List<Task<HttpResponseMessage>> manyRequests = [];
            List<HttpResponseMessage> manyResponces = [];
            int requestCount = random.Next(MaxConnections + 1, MaxConnections * 10);

            for (int i = 0; i < requestCount; i++)
            {
                var mes = new HttpRequestMessage(HttpMethod.Post, CheckPalindromeEndpointUrl)
                {
                    Content = new StringContent($"\"{random.Next(20)}\"", Encoding.UTF8, "application/json")
                };
                manyRequests.Add(_client.SendAsync(mes));
            }

            foreach (var t in manyRequests)
            {
                manyResponces.Add(await t);
            }

            Assert.AreEqual(manyResponces
                .Where(r=>r.StatusCode==System.Net.HttpStatusCode.ServiceUnavailable)
                .Count(), requestCount - MaxConnections);
            Assert.AreEqual(manyResponces
                .Where(r=>r.StatusCode==System.Net.HttpStatusCode.OK)
                .Count(), MaxConnections);
        }
    }
}
