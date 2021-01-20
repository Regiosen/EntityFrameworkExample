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
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            List<Usuario> result = new List<Usuario>();
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext()) 
            {
                foreach (var usuarios in db.Usuarios) 
                {
                    result.Add(usuarios);
                }
            }
            return result;

        }
        [HttpPost]
        [Route("delete")]
        public Usuario Delete([FromBody] Usuario usuario)
        {
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext())
            {
                Usuario usu = db.Usuarios.Where(c => c.UsuId == usuario.UsuId).First();
                db.Usuarios.Remove(usu);
                db.SaveChanges();
            }
            return usuario;
        }
        [HttpPost]
        [Route("insert")]
        public Usuario Insert([FromBody] Usuario newUsuario)
        {   
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext())
            {
                db.Usuarios.Add(newUsuario);
                db.SaveChanges();
            }
            return newUsuario;
        }
        [HttpPost]
        [Route("modify")]
        public Usuario Modify([FromBody] Usuario usuario)
        {
            Usuario result = null;
            using (EntityFrameworkExampleContext db = new EntityFrameworkExampleContext())
            {
                Usuario usu = db.Usuarios.Where(c => c.UsuId == usuario.UsuId).First();
                usu.UsuNombre = usuario.UsuNombre;
                usu.UsuPass = usuario.UsuPass;
                db.Entry(usu).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            return result;
        }
    }
}
