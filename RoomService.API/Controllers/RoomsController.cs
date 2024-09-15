using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomService.Application.Commands;
using RoomService.Application.Models;
using RoomService.Application.Queries;

namespace RoomService.API.Controllers
{
    /// <summary>
    /// Handles HTTP requests related to auctions, providing CRUD operations and more.
    /// </summary>
    [ApiController]
    [Route("api/auctions")]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all auctions.
        /// </summary>
        /// <returns>A list of auction DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionDto>>> GetAuctions([FromQuery] AuctionQueries.Query query)
        {
            var auctions = await _mediator.Send(query);
            return Ok(auctions);
        }

        /// <summary>
        /// Retrieves a specific auction by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the auction.</param>
        /// <returns>An auction DTO.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
        {
            var result = await _mediator.Send(new GetAuctionQuery.Query(id));
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Creates a new auction.
        /// </summary>
        /// <param name="command">The auction creation command.</param>
        /// <returns>The created auction DTO.</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AuctionDto>> CreateAuction([FromBody] CreateAuction.CreateAuctionCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAuctionById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing auction.
        /// </summary>
        /// <param name="id">The unique identifier of the auction to update.</param>
        /// <param name="command">The auction update command.</param>
        /// <returns>A response indicating the outcome of the operation.</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuction(Guid id, [FromBody] UpdateAuction.UpdateAuctionCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            command.Id = id;
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Deletes an auction.
        /// </summary>
        /// <param name="id">The unique identifier of the auction to delete.</param>
        /// <returns>A response indicating the outcome of the operation.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuction(Guid id)
        {
            string currentUser = User?.Identity?.Name;
            var result = await _mediator.Send(new DeleteAuction.DeleteAuctionCommand { Id = id, CurrentUser = currentUser });

            if (!result)
            {
                var auction = await _mediator.Send(new GetAuctionQuery.Query(id));
                if (auction == null)
                {
                    return NotFound();
                }

                return Forbid();
            }

            return NoContent();
        }
    }
}
