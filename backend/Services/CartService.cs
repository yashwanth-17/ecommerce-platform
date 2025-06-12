using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Data;
using EcommerceAPI.Models;
using EcommerceAPI.DTOs;

namespace EcommerceAPI.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync(string userId)
        {
            var cartItems = await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            return cartItems.Select(MapToDto);
        }

        public async Task<CartItemDto?> AddToCartAsync(string userId, AddToCartDto addToCartDto)
        {
            var product = await _context.Products.FindAsync(addToCartDto.ProductId);
            if (product == null || !product.IsActive)
                return null;

            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == addToCartDto.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += addToCartDto.Quantity;
                existingCartItem.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.CartItems.Add(cartItem);
                existingCartItem = cartItem;
            }

            await _context.SaveChangesAsync();

            // Reload with product information
            await _context.Entry(existingCartItem)
                .Reference(ci => ci.Product)
                .LoadAsync();

            return MapToDto(existingCartItem);
        }

        public async Task<bool> UpdateCartItemAsync(string userId, int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.UserId == userId);

            if (cartItem == null)
                return false;

            if (quantity <= 0)
            {
                _context.CartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
                cartItem.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromCartAsync(string userId, int cartItemId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.UserId == userId);

            if (cartItem == null)
                return false;

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var cartItems = await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return true;
        }

        private static CartItemDto MapToDto(CartItem cartItem)
        {
            return new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = cartItem.Product.Name,
                ProductPrice = cartItem.Product.Price,
                ProductImageUrl = cartItem.Product.ImageUrl,
                Quantity = cartItem.Quantity
            };
        }
    }
}

