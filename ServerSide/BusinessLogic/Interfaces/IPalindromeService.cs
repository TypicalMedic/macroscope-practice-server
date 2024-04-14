using Microsoft.AspNetCore.Mvc;

namespace ServerSide.BusinessLogic.Interfaces
{
    public interface IPalindromeService
    {
        Task<bool> IsPalindrome(string value);
    }
}
