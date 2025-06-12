using EcommerceAPI.DTOs;

namespace EcommerceAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
    }

    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
        Task<ProductDto?> UpdateProductAsync(int id, CreateProductDto updateProductDto);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string category);
    }

    public interface IOrderService
    {
        Task<OrderDto?> CreateOrderAsync(string userId, CreateOrderDto createOrderDto);
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId);
        Task<OrderDto?> GetOrderByIdAsync(int orderId, string userId);
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    }

    public interface ICartService
    {
        Task<IEnumerable<CartItemDto>> GetCartItemsAsync(string userId);
        Task<CartItemDto?> AddToCartAsync(string userId, AddToCartDto addToCartDto);
        Task<bool> UpdateCartItemAsync(string userId, int cartItemId, int quantity);
        Task<bool> RemoveFromCartAsync(string userId, int cartItemId);
        Task<bool> ClearCartAsync(string userId);
    }
}

