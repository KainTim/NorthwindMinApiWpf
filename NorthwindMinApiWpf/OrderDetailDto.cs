namespace NorthwindMinApiWpf;

internal class OrderDetailDto
{
  public int OrderId { get; set; }
  public required string Product { get; set; }
  public required string Category { get; set; }
  public int Quantity { get; set; }
  public int UnitPrice { get; set; }
}
