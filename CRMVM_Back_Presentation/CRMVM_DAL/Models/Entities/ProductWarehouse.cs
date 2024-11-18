using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMVM_DAL.Models.Entities
{
    public class ProductWarehouse
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        public Guid WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        public int Quantity { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public virtual ICollection<Deal> Deals { get; set; }
    }
}
