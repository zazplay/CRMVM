using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CRMVM_DAL.Models.Entities
{
    public class Deal
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public Guid ClientId { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Status { get; set; }


    }


}