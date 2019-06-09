using ShoppingBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Repositories
{
    public interface IProductRepository
    {
        Product GetProductByBarcode(); 
    }
}
