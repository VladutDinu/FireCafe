using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireCaffeDAL.Models
{
    [Table("Sales")]
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        public int SilverCups { get; set; }

        public virtual List<Product> Products { get; set; }

        public DateTime SaleTime { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }


    }
}
