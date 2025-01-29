using Microsoft.AspNetCore.Mvc;

using NorthwindApi.Dtos;
using NorthwindApi.Services;

using NorthwindDbLib;

namespace NorthwindApi.Maps;

public static class OrdersApi
{
  public static IEndpointRouteBuilder MapOrders(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("orders");
    group.MapGet("", (DbService service, [FromQuery] int employeeId, [FromQuery] string customerId) => 
      service.getOrdersWithEmployeeAndCustomer(employeeId,customerId));
    group.MapPost("", (DbService service, OrderDtoAdd order) => service.addOrder(order));
    group.MapDelete("/{orderId:int}", (DbService service,int orderId) => service.deleteOrder(orderId));
    return routes;
  }
}
