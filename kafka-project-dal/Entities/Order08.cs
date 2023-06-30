using System;
using System.Collections.Generic;

namespace kafka_project_dal.Entities;

public partial class Order08
{
    public Guid OrderId { get; set; }

    public string CustomerName { get; set; }

    public string OrderDescription { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal? OrderAmount { get; set; }

    public string OrderStatus { get; set; }
}
