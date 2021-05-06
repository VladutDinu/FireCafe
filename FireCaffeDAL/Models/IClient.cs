using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireCaffeDAL.Models
{
    public interface IClient
    {
       int Id { get; set; }

       string FirstName { get; set; }

       string LastName { get; set; }

       string Username { get; set; }

       string Password { get; set; }
       int Admin { get; set; }

       int GoldenCups { get; set; }

       int SilverCups { get; set; }

        //[ForeignKey("Sale")]
        //public int SaleId { get; set; }

        //public Sale Sale { get; set; }

    }
}
