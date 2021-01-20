using System;
using System.Collections.Generic;

#nullable disable

namespace FullStackAppSample.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int ProId { get; set; }
        public string ProDesc { get; set; }
        public decimal? ProValor { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
