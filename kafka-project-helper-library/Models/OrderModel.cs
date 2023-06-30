using System;

namespace kafka_project_helper_library.Models
{
  public class OrderModel
  {
    public string OrderId { get; set; }
    public string CustomerName { get; set; }
    public string OrderDescription { get; set; }
    public DateTime OrderDate { get; set; }
    public float OrderAmount { get; set; }
    public string OrderStatus { get; set; }
  }
}