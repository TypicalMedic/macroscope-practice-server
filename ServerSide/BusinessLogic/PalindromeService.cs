using ServerSide.BusinessLogic.Interfaces;
using ServerSide.PalindromeValidator.Interfaces;

namespace ServerSide.BusinessLogic
{
    public class PalindromeService(IPalindromeValidator validator) : IPalindromeService
    {
        readonly TimeSpan delay = TimeSpan.FromSeconds(1.5f);

        private readonly IPalindromeValidator _validator = validator;

        public Task<bool> IsPalindrome(string value)
        {
            return Task.Run(async delegate
            {
                await Task.Delay(delay);
                return _validator.IsValid(value);
            });
        }
    }
}
