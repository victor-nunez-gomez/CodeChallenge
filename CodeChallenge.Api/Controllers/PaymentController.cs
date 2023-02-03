using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        /// <summary>
        /// Endpoint to create a new Card
        /// </summary>
        /// <param name="card">Object which contains the Card information</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [HttpPost]
        public IActionResult CreateCard()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }

        /// <summary>
        /// Makes a payment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPost("{id}")]
        public IActionResult Pay(int id, decimal amount)
        {
            return Ok();
        }

        /// <summary>
        /// Gets balance from a given card specified by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public IActionResult GetBalance(int id)
        {
            return Ok();
        }

    }
}
