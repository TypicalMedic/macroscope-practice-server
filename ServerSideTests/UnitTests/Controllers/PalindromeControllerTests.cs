using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerSide.BusinessLogic;
using ServerSide.BusinessLogic.Interfaces;
using ServerSide.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSideTests.UnitTests.Controllers
{
    [TestClass]
    public class PalindromeControllerTests
    {
        private readonly Mock<IPalindromeService> ServiceMock = new();


        [TestMethod]
        public async Task NullString_True()
        {
            string input = "";
            ServiceMock.Setup(serviceMock => serviceMock.IsPalindrome(input)).Returns(Task.FromResult(true));
            var pController = new PalindromeController(ServiceMock.Object);

            var result = await pController.Check(null);
            var responce = result.Result;

            Assert.IsInstanceOfType(result, typeof(ActionResult<bool>));
            Assert.IsInstanceOfType(responce, typeof(BadRequestResult));
            Assert.AreEqual(((BadRequestResult)responce).StatusCode, 400);
        }

        [TestMethod]
        public async Task EmptyString_True()
        {
            string input = "";
            ServiceMock.Setup(serviceMock => serviceMock.IsPalindrome(input)).Returns(Task.FromResult(true));
            var pController = new PalindromeController(ServiceMock.Object);

            var result = await pController.Check(input);

            Assert.IsInstanceOfType(result, typeof(ActionResult<bool>));
            Assert.IsTrue(result?.Value);
        }

        [TestMethod]
        public async Task StringLength1_True()
        {
            string input = "1";
            ServiceMock.Setup(serviceMock => serviceMock.IsPalindrome(input)).Returns(Task.FromResult(true));
            var pContr = new PalindromeController(ServiceMock.Object);

            var result = await pContr.Check(input);

            Assert.IsInstanceOfType(result, typeof(ActionResult<bool>));
            Assert.IsTrue(result?.Value);
        }

        [TestMethod]
        public async Task StringPalindrome_True()
        {
            string input = "abba";
            ServiceMock.Setup(serviceMock => serviceMock.IsPalindrome(input)).Returns(Task.FromResult(true));
            var pContr = new PalindromeController(ServiceMock.Object);

            var result = await pContr.Check(input);

            Assert.IsInstanceOfType(result, typeof(ActionResult<bool>));
            Assert.IsTrue(result?.Value);
        }

        [TestMethod]
        public async Task StringNotPalindrome_False()
        {
            string input = "12gvqa4";
            ServiceMock.Setup(serviceMock => serviceMock.IsPalindrome(input)).Returns(Task.FromResult(false));
            var pContr = new PalindromeController(ServiceMock.Object);

            var result = await pContr.Check(input);

            Assert.IsInstanceOfType(result, typeof(ActionResult<bool>));
            Assert.IsFalse(result?.Value);
        }

        [TestMethod]
        public async Task StringPalindromeWithSymbols_True()
        {
            string input = "А [роза упала ()на ла,пу Азора.";
            ServiceMock.Setup(serviceMock => serviceMock.IsPalindrome(input)).Returns(Task.FromResult(true));
            var pContr = new PalindromeController(ServiceMock.Object);

            var result = await pContr.Check(input);

            Assert.IsInstanceOfType(result, typeof(ActionResult<bool>));
            Assert.IsTrue(result?.Value);
        }

        [TestMethod]
        public async Task StringNotPalindromeWithSymbols_False()
        {
            string input = "А роза3 -упала на, лапу Азора.";
            ServiceMock.Setup(serviceMock => serviceMock.IsPalindrome(input)).Returns(Task.FromResult(false));
            var pContr = new PalindromeController(ServiceMock.Object);

            var result = await pContr.Check(input);

            Assert.IsInstanceOfType(result, typeof(ActionResult<bool>));
            Assert.IsFalse(result?.Value);
        }
    }
}
