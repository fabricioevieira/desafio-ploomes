using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSalesApi.Models
{
    public class Ticket : Entity
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        [Precision(13,2)]
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int InitialQuantity { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
