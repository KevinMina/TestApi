using System;
using System.Collections.Generic;

namespace TestApi.Models;

public partial class Usuario
{
    public int IdUser { get; set; }

    public string NombreUser { get; set; } = null!;

    public bool EstadoUser { get; set; }

    public virtual ICollection<VentasDetalle> VentasDetalles { get; set; } = new List<VentasDetalle>();
}
