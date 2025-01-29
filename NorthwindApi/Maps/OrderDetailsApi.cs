using Microsoft.AspNetCore.Mvc;

using NorthwindApi.Services;

namespace NorthwindApi.Maps;

public static class OrderDetailsApi
{
  public static IEndpointRouteBuilder MapOrderDetails(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("orderdetails");
    group.MapGet("", (DbService service, [FromQuery] int orderId) =>
      service.getOrderDetailsWithOrderId(orderId));
    return routes;
  }
}
