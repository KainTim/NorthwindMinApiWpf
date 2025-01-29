namespace NorthwindMinApiWpf;

internal class EmployeeDto
{
  public int EmployeeId { get; set; }
  public  string LastName { get; set; }
  public  string FirstName { get; set; }
  public string Display => $"{LastName} {FirstName}";
}
