namespace TicketSalesApi.Models.Dtos
{
    public class CreateOrderDto
    {
        public int Id { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }

    public class CreateOrderItemDto
    {
        public int TicketId { get; set; }
        public int Quantity { get; set; }
    }
}
