using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerSide.PalindromeValidator;

namespace macroscopTestTask.Test
{
    [TestClass]
    internal class PalindromeValidatorTests
    {
        readonly PalindromeValidator Validator = new();

        [TestMethod]
        public void TestEmptyString()
        {
            string input = "";

            bool result = Validator.IsValid(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestStringLength1()
        {
            string input = "a";

            bool result = Validator.IsValid(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestStringPalindrome()
        {
            string input = "ab1ba";

            bool result = Validator.IsValid(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestStringNotPalindrome()
        {
            string input = "ae1grs";

            bool result = Validator.IsValid(input);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestStringPalindromeWithSymbols()
        {
            string input = "А [роза упала ()на ла,пу Азора.";

            bool result = Validator.IsValid(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestStringNotPalindromeWithSymbols()
        {
            string input = "А роза3 -упала на, лапу Азора.";

            bool result = Validator.IsValid(input);

            Assert.IsFalse(result);
        }
    }
}
