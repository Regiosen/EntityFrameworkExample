using System;
using System.Collections.Generic;

#nullable disable

namespace FullStackAppSample.Models
{
    public partial class Pedido
    {
        public int PedId { get; set; }
        public int? PedUsu { get; set; }
        public int? PedProd { get; set; }
        public decimal? PedVrUnit { get; set; }
        public double? PedCant { get; set; }
        public decimal? PedSubTot { get; set; }
        public double? PedIva { get; set; }
        public decimal? PedTotal { get; set; }

        public virtual Producto PedProdNavigation { get; set; }
        public virtual Usuario PedUsuNavigation { get; set; }
    }
}
