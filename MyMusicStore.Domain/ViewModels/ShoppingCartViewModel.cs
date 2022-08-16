using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicStore.Domain.ViewModels
{
        public class ShoppingCartViewModel
        {
            public List<CartItem>? CartItems { get; set; }
            public decimal CartTotal { get; set; }
        }
}
