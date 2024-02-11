using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TicketSalesApi.Models.Dtos
{
    public class CreateEventDto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [MaxLength(200)]
        public string Adress { get; set; }
        public DateTime Date { get; set; }
        public int MaxCapacity { get; set; }
        public IEnumerable<CreateTicketDto>? Tickets { get; set; }
    }
}
