using Microsoft.AspNetCore.Mvc;

namespace ServerSide.Controllers.Interfaces
{
    public interface IPalindromeService
    {
        Task<bool> IsPalindrome(string value);
    }
}
