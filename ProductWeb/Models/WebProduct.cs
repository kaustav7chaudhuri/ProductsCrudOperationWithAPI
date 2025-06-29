using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductWeb.Models
{
    public class WebProduct
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? StockQnty { get; set; }

        public string? Category { get; set; }

        public string? Manufacturer { get; set; }

        public string? ProductAvailability { get; set; }

        public bool IsOnSale { get; set; }

        public bool IsNewArrival { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        //public int CategoryId { get; set; }
        //public int ManufacturerId { get; set; }

        //public List<SelectListItem> CategoryList { get; set; } 
        //public List<SelectListItem> ManufacturerList { get; set; } 
    }
}
