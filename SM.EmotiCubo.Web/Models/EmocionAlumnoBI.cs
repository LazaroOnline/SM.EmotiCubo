using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SM.EmotiCubo.Web.Models
{
    public class EmocionAlumnoBI
	{
		public string Clase   { get; set; }
		public string Materia { get; set; }
		public int EmocionId { get; set; }
		public DateTime Fecha { get; set; }
    }
}
