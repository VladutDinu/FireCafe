using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireCaffeDAL.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [StringLength(150)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string Username { get; set; }

        [StringLength(150)]
        public string Password { get; set; }
        public int Admin { get; set; }

        public int GoldenCups { get; set; }

        public int SilverCups { get; set; }

        //[ForeignKey("Sale")]
        //public int SaleId { get; set; }

        //public Sale Sale { get; set; }

    }
}
