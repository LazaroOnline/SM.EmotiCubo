using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SM.EmotiCubo.Web.Models
{
	public class Sesion
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		//[BsonRepresentation(BsonType.ObjectId)]
		public string CuboId { get; set; }
		//public Cubo Cubo { get; set; }

		public string Nombre { get; set; }

		public bool Cerrada { get; set; }

		[BsonRepresentation(BsonType.DateTime)]
		[BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
		public DateTime Fecha { get; set; }

		public List<string> Grupos { get; set; }
		public List<EmocionAlumno> EmocionesAlumnos { get; set; }
	}
}
