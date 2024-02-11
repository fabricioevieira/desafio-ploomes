using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSalesApi.Models
{
    public class OrderItem : Entity
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}