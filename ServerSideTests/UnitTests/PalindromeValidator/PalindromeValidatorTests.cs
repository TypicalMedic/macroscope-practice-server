using ServerSide.PalindromeValidator;

namespace ServerSideTests.UnitTests.PalindromeValidator
{
    [TestClass]
    public class PalindromeValidatorTests
    {
        readonly ServerSide.PalindromeValidator.PalindromeValidator Validator = new();

        [TestMethod]
        public void EmptyString_True()
        {
            string input = "";

            bool result = Validator.IsValid(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringLength1_True()
        {
            string input = "a";

            bool result = Validator.IsValid(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringPalindrome_True()
        {
            string input = "ab1ba";

            bool result = Validator.IsValid(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringNotPalindrome_False()
        {
            string input = "ae1grs";

            bool result = Validator.IsValid(input);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void StringPalindromeWithSymbols_True()
        {
            string input = "А [роза упала ()на ла,пу Азора.";

            bool result = Validator.IsValid(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringNotPalindromeWithSymbols_False()
        {
            string input = "А роза3 -упала на, лапу Азора.";

            bool result = Validator.IsValid(input);

            Assert.IsFalse(result);
        }
    }
}
