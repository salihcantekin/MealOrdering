using MealOrdering.Server.Services.Infrastruce;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.FilterModels;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService OrderService)
        {
            orderService = OrderService;
        }



        #region Order Methods

        [HttpGet("OrderById/{Id}")]
        public async Task<ServiceResponse<OrderDTO>> GetOrderById(Guid Id)
        {
            return new ServiceResponse<OrderDTO>()
            {
                Value = await orderService.GetOrderById(Id)
            };
        }

        [HttpGet("OrdersByDate")]
        public async Task<ServiceResponse<List<OrderDTO>>> GetOrder(DateTime OrderDate)
        {
            return new ServiceResponse<List<OrderDTO>>()
            {
                Value = await orderService.GetOrders(OrderDate)
            };
        }

        [HttpPost("OrdersByFilter")]
        public async Task<ServiceResponse<List<OrderDTO>>> GetOrdersByFilter([FromBody] OrderListFilterModel Filter)
        {
            return new ServiceResponse<List<OrderDTO>>()
            {
                Value = await orderService.GetOrdersByFilter(Filter)
            };
        }

        [HttpGet("TodaysOrder")]
        public async Task<ServiceResponse<List<OrderDTO>>> GetTodaysOrder()
        {
            return new ServiceResponse<List<OrderDTO>>()
            {
                Value = await orderService.GetOrders(DateTime.Now)
            };
        }

        [HttpPost("CreateOrder")]
        public async Task<ServiceResponse<OrderDTO>> CreateOrder(OrderDTO Order)
        {
            return new ServiceResponse<OrderDTO>()
            {
                Value = await orderService.CreateOrder(Order)
            };
        }

        [HttpPost("UpdateOrder")]
        public async Task<ServiceResponse<OrderDTO>> UpdateOrder(OrderDTO Order)
        {
            return new ServiceResponse<OrderDTO>()
            {
                Value = await orderService.UpdateOrder(Order)
            };
        }

        [HttpPost("DeleteOrder")]
        public async Task<BaseResponse> DeleteOrder([FromBody] Guid OrderId)
        {
            await orderService.DeleteOrder(OrderId);
            return new BaseResponse();
        }

        [HttpGet("DeleteOrder/{OrderId}")]
        public async Task<BaseResponse> DeleteOrderFromQueryString(Guid OrderId)
        {
            await orderService.DeleteOrder(OrderId);
            return new BaseResponse();
        }

        #endregion

        #region OrderItem Methods

        #region Get

        [HttpGet("OrderItemsById/{Id}")]
        public async Task<ServiceResponse<OrderItemsDTO>> GetOrderItemsById(Guid Id)
        {
            return new ServiceResponse<OrderItemsDTO>()
            {
                Value = await orderService.GetOrderItemsById(Id)
            };
        }

        #endregion


        [HttpPost("CreateOrderItem")]
        public async Task<ServiceResponse<OrderItemsDTO>> CreateOrderItem(OrderItemsDTO OrderItem)
        {
            return new ServiceResponse<OrderItemsDTO>()
            {
                Value = await orderService.CreateOrderItem(OrderItem)
            };
        }

        [HttpPost("UpdateOrderItem")]
        public async Task<ServiceResponse<OrderItemsDTO>> UpdateOrderItem(OrderItemsDTO OrderItem)
        {
            return new ServiceResponse<OrderItemsDTO>()
            {
                Value = await orderService.UpdateOrderItem(OrderItem)
            };
        }


        [HttpPost("DeleteOrderItem")]
        public async Task<BaseResponse> DeleteOrderItem([FromBody] Guid OrderItemId)
        {
            await orderService.DeleteOrderItem(OrderItemId);
            return new BaseResponse();
        }

        [HttpGet("OrderItems")]
        public async Task<ServiceResponse<List<OrderItemsDTO>>> GetOrderItems(Guid OrderId)
        {
            return new ServiceResponse<List<OrderItemsDTO>>()
            {
                Value = await orderService.GetOrderItems(OrderId)
            };
        }

        #endregion

    }
}
