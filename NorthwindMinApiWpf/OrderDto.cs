namespace NorthwindMinApiWpf;

internal class OrderDto
{
  public int Id { get; set; }
  public required string DateString { get; set; }
  public bool IsShipped { get; set; }
  public int NrItems { get; set; }
}
