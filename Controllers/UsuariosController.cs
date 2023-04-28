using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Controllers
{
    
    [ApiController]
    [Route("users")]
    public class UsuariosController : ControllerBase
    {
        private readonly TestDbContext _context;

        public UsuariosController(TestDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: Usuarios
        public ActionResult<List<Usuario>> Get()
        {
            var user = from u in _context.Usuarios
                       select u;
              return Ok(user);
        }

    }
}
