using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSalesApi.Models
{
    public class Order : Entity
    {
        public DateTime Date { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        [Precision(13,2)]
        public decimal TotalAmount { get; set; }
    }
}
