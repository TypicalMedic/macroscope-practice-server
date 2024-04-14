using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ServerSide.BusinessLogic.Interfaces;

namespace ServerSide.Controllers
{
    /// <summary>
    /// ������������� ���������� ��� ������ � ������������
    /// </summary>
    [ApiController]
    [EnableRateLimiting("Concurrency")]
    [Route("palindrome")]

    public class PalindromeController(IPalindromeService service) : ControllerBase
    {

        private readonly IPalindromeService _pService = service;

        /// <summary>
        /// �������� �������� �� �������� ������ �����������
        /// </summary>
        /// <response code="200">���������� true, ���� ������ - ���������, ����� false</response>
        /// <response code="400">���� ������� �� ������������� �����</response>
        /// <response code="503">������ �� ����� ���������� ������</response>
        [HttpPost("check")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<bool>> Check([FromBody]string? input)
        {
            if(input == null)
            {
                return BadRequest();
            }

            return await _pService.IsPalindrome(input);
        }
    }
}
