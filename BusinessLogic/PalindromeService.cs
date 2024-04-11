using ServerSide.BusinessLogic.Interfaces;
using ServerSide.Controllers.Interfaces;

namespace ServerSide.BusinessLogic
{
    public class PalindromeService : IPalindromeService
    {
        IPalindromeValidator _validator { get; set; }
        public PalindromeService(IPalindromeValidator validator)
        {
            _validator = validator;
        }

        public Task<bool> IsPalindrome(string value)
        {
            return Task.Run(() => _validator.IsValid(value));
        }
    }
}
