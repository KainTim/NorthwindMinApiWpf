namespace NorthwindApi.Dtos;

internal class OrderDetailDto
{
  public int OrderId { get; set; }
  public string? Product { get; set; }
  public string? Category { get; set; }
  public int Quantity { get; set; }
  public int UnitPrice { get; set; }
}
