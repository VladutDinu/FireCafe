using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireCaffeDAL.Models
{
    public interface IProduct
    {

       int Id { get; set; }

       string Name { get; set; }

       int Price { get; set; }

       string Description { get; set; }

       string Size { get; set; }

       string Type { get; set; }

    }
}
