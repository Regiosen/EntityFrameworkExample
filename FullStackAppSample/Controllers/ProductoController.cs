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
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            List<Producto> result = new List<Producto>();
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext()) 
            {
                foreach (var products in db.Productos) 
                {
                    result.Add(products);
                }
            }
            return result;

        }
        [HttpPost]
        [Route("delete")]
        public Producto Delete([FromBody] Producto producto)
        {
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext())
            {
                Producto prod = db.Productos.Where(c => c.ProId == producto.ProId).First();
                db.Productos.Remove(prod);
                db.SaveChanges();
            }
            return producto;
        }
        [HttpPost]
        [Route("insert")]
        public Producto Insert([FromBody] Producto newProduct)
        {   
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext())
            {
                db.Productos.Add(newProduct);
                db.SaveChanges();
            }
            return newProduct;
        }
        [HttpPost]
        [Route("modify")]
        public Producto Modify([FromBody] Producto producto)
        {
            Producto result = null;
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext())
            {
                Producto prod = db.Productos.Where(c => c.ProId == producto.ProId).First();
                prod.ProDesc = producto.ProDesc;
                prod.ProValor = producto.ProValor;
                db.Entry(prod).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            return result;
        }
    }
}
