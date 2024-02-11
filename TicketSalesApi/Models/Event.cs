﻿using System.ComponentModel.DataAnnotations;

namespace TicketSalesApi.Models
{
    public class Event : Entity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [MaxLength(200)]
        public string Adress { get; set; }
        public DateTime Date { get; set; }
        public int MaxCapacity { get; set; }
    }
}
