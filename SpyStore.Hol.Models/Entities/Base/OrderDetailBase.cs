using System.ComponentModel.DataAnnotations;

namespace SpyStore.Hol.Models.Entities.Base
{
    public class OrderDetailBase : EntityBase
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required, DataType(DataType.Currency), Display(Name = "Unit Cost")]
        public decimal UnitCost { get; set; }
    }
}
