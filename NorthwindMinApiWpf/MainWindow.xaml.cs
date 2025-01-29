using RestSharp;

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NorthwindMinApiWpf;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  private RestClient _client = new("http://localhost:5216/");
  public MainWindow() => InitializeComponent();

  private void AddOrderDetail_Clicked(object sender, RoutedEventArgs e)
  {
    AddOrderDetail();
  }


  private void GrdOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    DisplayOrderDetails();
  }

  private void BtnDeleteOrder_Clicked(object sender, RoutedEventArgs e)
  {
    DeleteOrder();
  }


  private void BtnNewOrder_Clicked(object sender, RoutedEventArgs e)
  {
    AddOrder();
  }


  private void CboEmployeesSelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    DisplayOrders();
  }

  private void CboCustomersSelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    DisplayOrders();
  }

  private void DisplayOrders()
  {
    if (cboEmployees.SelectedItem == null) return;
    if (cboCustomers.SelectedItem == null) return;
    var orderDtos = _client.Get<List<OrderDto>>($"orders?employeeId={((EmployeeDto)cboEmployees.SelectedItem).EmployeeId}&customerId={((CustomerDto)cboCustomers.SelectedItem).CustomerId}")
                           .OrderBy(x => x.Id)
                           .Reverse();
    grdOrders.ItemsSource = orderDtos;
    grdOrders.SelectedIndex = 0;
  }
  private void DisplayOrderDetails()
  {
    if (grdOrders.SelectedItem is not OrderDto) return;
    var orderDetailDtos = _client.Get<List<OrderDetailDto>>($"orderDetails?orderId={((OrderDto)grdOrders.SelectedItem).Id}");
    grdOrderDetails.ItemsSource = orderDetailDtos;
    grdOrderDetails.SelectedIndex = 0;
  }
  private void AddOrder()
  {
    if (cboEmployees.SelectedItem == null) return;
    if (cboCustomers.SelectedItem == null) return;
    var orderDtoAdd = new OrderDtoAdd()
    {
      EmployeeId = ((EmployeeDto)cboEmployees.SelectedItem).EmployeeId,
      CustomerId = ((CustomerDto)cboCustomers.SelectedItem).CustomerId,
    };
    var reqest = new RestRequest("orders").AddJsonBody(orderDtoAdd);
    var response = _client.Post(reqest);
    DisplayOrders();
  }
  private void AddOrderDetail()
  {
    if (grdOrders.SelectedItem is not OrderDto) return;
    if (cboProducts.SelectedItem == null) return;
    var orderDetailDtoAdd = new OrderDetailDtoAdd()
    {
      OrderId = ((OrderDto)grdOrders.SelectedItem).Id,
      ProductId = ((ProductDto)cboProducts.SelectedItem).ProductId,
      Quantity = int.Parse(txtQuantity.Text),
    };
    var reqest = new RestRequest("orderdetails").AddJsonBody(orderDetailDtoAdd);
    var response = _client.Post(reqest);
    DisplayOrders();
    DisplayOrderDetails();
  }
  private void DeleteOrder()
  {
    if (grdOrders.SelectedItem is not OrderDto) return;
    var reqest = new RestRequest($"orders/{((OrderDto)grdOrders.SelectedItem).Id}");
    var response = _client.Delete(reqest);
    DisplayOrders();
  }

  private async void Window_Loaded(object sender, RoutedEventArgs e)
  {
    var employeeDtos = _client.Get<List<EmployeeDto>>("employees");
    foreach (var item in employeeDtos)
    {
      cboEmployees.Items.Add(item);
    }
    cboEmployees.DisplayMemberPath = "Display";
    var customerDtos = _client.Get<List<CustomerDto>>("customers");
    foreach (var item in customerDtos)
    {
      cboCustomers.Items.Add(item);
    }
    cboCustomers.DisplayMemberPath = "CompanyName";
    var productDtos = _client.Get<List<ProductDto>>("products");
    foreach (var item in productDtos)
    {
      cboProducts.Items.Add(item);
    }
    cboProducts.DisplayMemberPath = "ProductName";
    cboEmployees.SelectedIndex = 3;
    cboCustomers.SelectedIndex = 0;
    cboProducts.SelectedIndex = 0;
  }
}
