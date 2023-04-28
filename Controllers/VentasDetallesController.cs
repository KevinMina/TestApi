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
    [Route("venta")]
    public class VentasDetallesController : ControllerBase
    {
        private readonly TestDbContext _context;

        public VentasDetallesController(TestDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: Ventas
        public ActionResult<List<VentasDetalle>> Get()
        {
            var user = from u in _context.VentasDetalles
                       select u;
            return Ok(user);
        }



    }
}