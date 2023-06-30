using System;
using System.ComponentModel.DataAnnotations;

namespace kafka_project_mvc.Models
{
  public class OrderModel
  {
    [Display(Name="Order Id")]
    public Guid OrderId { get; set; } = Guid.NewGuid();
    [Display(Name="Customer Name")]
    public string CustomerName { get; set; }
    [Display(Name="Order Description")]
    public string OrderDescription { get; set; }
    [Display(Name="Order Date")]
    public DateTime OrderDate { get; set; } = DateTime.Now;
    [Display(Name="Order Amount")]
    public float OrderAmount { get; set; }
    [Display(Name="Order Status")]
    public string OrderStatus { get; set; }
  }
}