

using Moq;
using ServerSide.BusinessLogic;
using ServerSide.PalindromeValidator.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ServerSideTests.UnitTests.BusinessLogic
{
    [TestClass]
    public class PalindromeServiceTests
    {

        private readonly Mock<IPalindromeValidator> ValidatorMock = new Mock<IPalindromeValidator>();

        [TestMethod]
        public async Task EmptyString_True()
        {
            string input = "";
            ValidatorMock.Setup(validatorMock => validatorMock.IsValid(input)).Returns(true);
            var pService = new PalindromeService(ValidatorMock.Object);

            var result = await pService.IsPalindrome(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task StringLength1_True()
        {
            string input = "1";
            ValidatorMock.Setup(validatorMock => validatorMock.IsValid(input)).Returns(true);
            var pService = new PalindromeService(ValidatorMock.Object);

            var result = await pService.IsPalindrome(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task StringPalindrome_True()
        {
            string input = "abba";
            ValidatorMock.Setup(validatorMock => validatorMock.IsValid(input)).Returns(true);
            var pService = new PalindromeService(ValidatorMock.Object);

            var result = await pService.IsPalindrome(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task StringNotPalindrome_False()
        {
            string input = "12gvqa4";
            ValidatorMock.Setup(validatorMock => validatorMock.IsValid(input)).Returns(false);
            var pService = new PalindromeService(ValidatorMock.Object);

            var result = await pService.IsPalindrome(input);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task StringPalindromeWithSymbols_True()
        {
            string input = "А [роза упала ()на ла,пу Азора.";
            ValidatorMock.Setup(validatorMock => validatorMock.IsValid(input)).Returns(true);
            var pService = new PalindromeService(ValidatorMock.Object);

            var result = await pService.IsPalindrome(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task StringNotPalindromeWithSymbols_False()
        {
            string input = "А роза3 -упала на, лапу Азора.";
            ValidatorMock.Setup(validatorMock => validatorMock.IsValid(input)).Returns(false);
            var pService = new PalindromeService(ValidatorMock.Object);

            var result = await pService.IsPalindrome(input);

            Assert.IsFalse(result);
        }
    }
}
