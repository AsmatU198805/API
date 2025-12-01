using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Model
{
    public class PurchaseRequestmodel
    {
        [Key]
        public int PRID { get; set; }
        public string PRNo { get; set; }
        public DateTime? PRDate { get; set; }
        public string PRQuantity { get; set; }

        [ForeignKey("MUnit")]
        public int MUnitId { get; set; }

        [ForeignKey("Product")]
        public int Id { get; set; }


        public ProductsModel? Product { get; set; }
        public MUnitModel? MUnit { get; set; }
    }
}
