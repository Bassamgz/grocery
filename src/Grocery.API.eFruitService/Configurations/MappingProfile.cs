namespace Grocery.API.eFruitService.Configurations
{
    using AutoMapper;
    using Grocery.Core.Data.Model.DAO;

    public class MappingProfile : Profile
    {
        // Classes implement Profile are Automatically loaded by Automapper
        public MappingProfile()
        {
            CreateMap<Cart, Core.Data.Model.DTO.Cart>();

            // Include product name for Cart display
            CreateMap<CartItem, Core.Data.Model.DTO.CartItem>().
                ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<CustomerCart, Core.Data.Model.DTO.CustomerCart>();
            CreateMap<CustomerOrder, Core.Data.Model.DTO.CustomerOrder>();
            CreateMap<Order, Core.Data.Model.DTO.Order>();
            CreateMap<OrderItem, Core.Data.Model.DTO.OrderItem>();
            CreateMap<Product, Core.Data.Model.DTO.Product>();
            CreateMap<Core.Data.Model.DTO.Product, Product>().
                ForMember(x => x.CartItem, opt => opt.Ignore());
        }
    }
}
