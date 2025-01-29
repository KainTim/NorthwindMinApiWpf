using Microsoft.AspNetCore.Mvc;

using NorthwindApi.Dtos;
using NorthwindApi.Services;

namespace NorthwindApi.Maps;

public static class OrderDetailsApi
{
  public static IEndpointRouteBuilder MapOrderDetails(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("orderdetails");
    group.MapGet("", (DbService service, [FromQuery] int orderId) =>
      service.getOrderDetailsWithOrderId(orderId));
    group.MapPost("", (DbService service, OrderDetailDtoAdd orderDetailDtoAdd) =>
      service.addOrderDetail(orderDetailDtoAdd));
    return routes;
  }
}
