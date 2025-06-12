using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EcommerceAPI.Services;
using EcommerceAPI.DTOs;
using System.Security.Claims;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var cartItems = await _cartService.GetCartItemsAsync(userId);
            return Ok(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var cartItem = await _cartService.AddToCartAsync(userId, addToCartDto);
            if (cartItem == null)
                return BadRequest("Failed to add item to cart");

            return Ok(cartItem);
        }

        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, [FromBody] UpdateCartItemDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _cartService.UpdateCartItemAsync(userId, cartItemId, updateDto.Quantity);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _cartService.RemoveFromCartAsync(userId, cartItemId);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }
    }

    public class UpdateCartItemDto
    {
        public int Quantity { get; set; }
    }
}

