using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SM.EmotiCubo.Web.Models
{
	public class Emocion
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		// [BsonElement("name")]
		public string Nombre { get; set; }
	}

	public enum EmocionesCubo {
		 Alegria = 1
		,Curiosidad = 2
		,Culpa = 3
		,Tristeza = 4
		,Miedo = 5
		,Rabia = 6
	}
}