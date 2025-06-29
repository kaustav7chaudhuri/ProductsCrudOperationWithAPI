using System;
using System.Collections.Generic;

namespace ProductAPI.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? StockQnty { get; set; }

    public string? Category { get; set; }

    public string? Manufacturer { get; set; }

    public string? ProductAvailability { get; set; }

    public bool? IsOnSale { get; set; }

    public bool? IsNewArrival { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
