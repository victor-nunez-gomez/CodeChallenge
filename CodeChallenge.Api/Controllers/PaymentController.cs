using CodeChallenge.Cards.Interfaces;
using CodeChallenge.Cards.ViewModel;
using CodeChallenge.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly ICardService _cardService;

        public PaymentController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Endpoint to create a new Card
        /// </summary>
        /// <param name="card">Object which contains the Card information</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Card), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(UpsertViewModel<Card>), 500)]
        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] Card card)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cardService.InsertCard(card);

            return result.HasError
                ? StatusCode(500, result)
                : Created("", result.Entity);
        }

        /// <summary>
        /// Makes a payment
        /// </summary>
        /// <param name="id">The Card's Id</param>
        /// <param name="amount">The amount to be paid</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Payment), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPost("{id}")]
        public async Task<IActionResult> Pay(int id, decimal amount)
        {
            var result = await _cardService.MakePayment(id, amount);

            return result.HasError
                    ? result.Error.ErrorCode == "404" 
                        ? NotFound() 
                        : StatusCode(500, result)
                    : Ok(result.Entity);
        }

        /// <summary>
        /// Gets the balance of any given card
        /// </summary>
        /// <param name="id">The card's Id</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(decimal), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(decimal), 404)]
        [ProducesResponseType(typeof(decimal), 500)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBalance(int id)
        {
            var result = await _cardService.GetCard(id);

            return result == null ? NotFound() : Ok(result.Balance);
        }

    }
}
