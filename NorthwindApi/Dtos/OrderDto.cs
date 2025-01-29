namespace NorthwindApi.Dtos;

internal class OrderDto
{
  public int Id { get; set; }
  public string? DateString { get; set; }
  public bool IsShipped { get; set; }
  public int NrItems { get; set; }
}
