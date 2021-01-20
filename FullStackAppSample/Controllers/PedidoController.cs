using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using FullStackAppSample.Models;

namespace FullStackAppSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;

        public PedidoController(ILogger<PedidoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Pedido> Get()
        {
            List<Pedido> result = new List<Pedido>();
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext()) 
            {
                foreach (var pedidos in db.Pedidos) 
                {
                    result.Add(pedidos);
                }
            }
            return result;

        }
        [HttpPost]
        [Route("delete")]
        public Pedido Delete([FromBody] Pedido pedido)
        {
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext())
            {
                Pedido ped = db.Pedidos.Where(c => c.PedId == pedido.PedId).First();
                db.Pedidos.Remove(ped);
                db.SaveChanges();
            }
            return pedido;
        }
        [HttpPost]
        [Route("insert")]
        public Pedido Insert([FromBody] Pedido newPedido)
        {   
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext())
            {
                db.Pedidos.Add(newPedido);
                db.SaveChanges();
            }
            return newPedido;
        }
        [HttpPost]
        [Route("modify")]
        public Pedido Modify([FromBody] Pedido pedido)
        {
            Pedido result = pedido;
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext())
            {
                Pedido ped = db.Pedidos.Where(c => c.PedId == pedido.PedId).First();
                ped.PedId = pedido.PedId;
                ped.PedUsu = pedido.PedUsu;
                ped.PedProd = pedido.PedProd;
                ped.PedVrUnit = pedido.PedVrUnit;
                ped.PedCant = pedido.PedCant;
                ped.PedSubTot = pedido.PedSubTot;
                ped.PedIva = pedido.PedIva;
                ped.PedTotal = pedido.PedTotal;
                db.Entry(ped).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            return result;
        }
    }
}
