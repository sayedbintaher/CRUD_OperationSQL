using System.ComponentModel.DataAnnotations;

namespace CRUDOperationWithQuery.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }

    }
}
