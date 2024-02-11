using Microsoft.EntityFrameworkCore;

namespace TicketSalesApi.Models.Dtos
{
    public class CreateTicketDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int InitialQuantity { get; set; }
    }
}
