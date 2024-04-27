using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopCrudApi.Shops.Model
{
    [Table("shop")]
    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("location")]
        public string Location { get; set; }

        [Required]
        [Column("employees")]
        public int Employees { get; set; }


    }
}
