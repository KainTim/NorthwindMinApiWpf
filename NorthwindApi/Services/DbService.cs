
using Microsoft.EntityFrameworkCore;

using NorthwindApi.Dtos;

using NorthwindDbLib;

namespace NorthwindApi.Services;

public class DbService(NorthwindContext db)
{
  internal List<EmployeeDto> getEmployees()
  {
    return db.Employees.Select((employee) => new EmployeeDto().CopyFrom(employee)).ToList();
  }
  internal List<CustomerDto> getCustomers()
  {
    return db.Customers.Select((customer) => new CustomerDto().CopyFrom(customer)).ToList();
  }
  internal List<ProductDto> getProducts()
  {
    return db.Products.Select((product) => new ProductDto().CopyFrom(product)).ToList();
  }

  internal List<OrderDto> getOrdersWithEmployeeAndCustomer(int employeeId, string customerId)
  {
    return db.Orders
      .Where((order) => order.EmployeeId == employeeId && order.CustomerId == customerId)
      .Select((order) => new OrderDto()
      {
        Id = order.OrderId,
        DateString = order.OrderDate.ToString(),
        IsShipped = order.ShippedDate.HasValue,
        NrItems = order.OrderDetails.Count,
      }
    ).ToList();
  }

  internal object getOrderDetailsWithOrderId(int orderId)
  {

    return db.OrderDetails
      .Include(x=>x.Product)
      .ThenInclude(x=>x.Category)
      .Where((detail) => detail.OrderId == orderId)
      .Select((detail) => new OrderDetailDto()
      {
        Category = detail.Product.Category.CategoryName,
        OrderId = detail.OrderId,
        Product = detail.Product.ProductName,
        Quantity = detail.Quantity,
        UnitPrice = (int)detail.UnitPrice,

      }
    ).ToList();
  }

  internal void addOrder(OrderDtoAdd order) {
    db.Orders.Add(new Order()
    {
      EmployeeId = order.EmployeeId,
      CustomerId = order.CustomerId,
    });
  }
}
