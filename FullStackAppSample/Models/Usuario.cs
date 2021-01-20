using System;
using System.Collections.Generic;

#nullable disable

namespace FullStackAppSample.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int UsuId { get; set; }
        public string UsuNombre { get; set; }
        public string UsuPass { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
