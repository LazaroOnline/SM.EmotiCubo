using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SM.EmotiCubo.Web.Models
{
	public class Cubo
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		//[BsonRepresentation(BsonType.ObjectId)]
		public string PropietarioId { get; set; }
	}
}