using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class MUnitModel
    {
        [Key]
        public int MUnitId {  get; set; }
        public required string MUnitCode { get; set; }
        public required string MUnitName { get; set; }

    }
}