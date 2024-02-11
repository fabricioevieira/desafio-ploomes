using System.ComponentModel.DataAnnotations;

namespace TicketSalesApi.Models
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
    }
}
