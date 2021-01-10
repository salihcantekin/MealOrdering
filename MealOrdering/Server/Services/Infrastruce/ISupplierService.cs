using MealOrdering.Shared.DTO;
using MealOrdering.Shared.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Infrastruce
{
    public interface IOrderService
    {
        public Task<OrderDTO> CreateOrder(OrderDTO Order);

        public Task<OrderDTO> UpdateOrder(OrderDTO Order);

        public Task DeleteOrder(Guid OrderId);

        public Task<List<OrderDTO>> GetOrders(DateTime OrderDate);

        public Task<List<OrderDTO>> GetOrdersByFilter(OrderListFilterModel Filter);

        public Task<OrderDTO> GetOrderById(Guid Id);



        public Task<OrderItemsDTO> CreateOrderItem(OrderItemsDTO OrderItem);

        public Task<OrderItemsDTO> UpdateOrderItem(OrderItemsDTO OrderItem);

        public Task<List<OrderItemsDTO>> GetOrderItems(Guid OrderId);

        public Task<OrderItemsDTO> GetOrderItemsById(Guid Id);

        public Task DeleteOrderItem(Guid OrderItemId);
    }
}
