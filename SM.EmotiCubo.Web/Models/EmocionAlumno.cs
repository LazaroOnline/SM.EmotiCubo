using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SM.EmotiCubo.Web.Models
{
	public class EmocionAlumno
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		//[BsonRepresentation(BsonType.ObjectId)]
		//public string SesionId { get; set; }

		//[BsonRepresentation(BsonType.DateTime)]
		[BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
		public DateTime Fecha { get; set; }

		//[BsonRepresentation(BsonType.ObjectId)]
		//public string EmocionId { get; set; }
		public int EmocionId { get; set; }
		//public Emocion Emocion { get; set; }
		public string Emocion { get; set; }

	}
}