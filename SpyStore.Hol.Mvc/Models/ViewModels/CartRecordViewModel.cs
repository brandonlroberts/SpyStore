using System.ComponentModel.DataAnnotations;
using SpyStore.Hol.Models.ViewModels;
using SpyStore.Hol.Mvc.Validation;

namespace SpyStore.Hol.Models.ViewModels
{
    public class CartRecordViewModel : CartRecordWithProductInfo
    {
        [Required, MustNotBeGreaterThan(nameof(UnitsInStock))]

        public new int Quantity { get; set; }
    }
}
