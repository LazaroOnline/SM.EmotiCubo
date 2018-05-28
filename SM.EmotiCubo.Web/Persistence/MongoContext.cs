using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using MongoDB.Driver;
using SM.EmotiCubo.Web.Models;

namespace SM.EmotiCubo.Web.Persistence
{
	public class MongoContext
	{
		public IMongoDatabase Database;
		public IMongoCollection<Cubo> Cubos => Database.GetCollection<Cubo>(CollectionNames.Cubos);
		public IMongoCollection<Sesion> Sesiones => Database.GetCollection<Sesion>(CollectionNames.Sesiones);
		public IMongoCollection<EmocionAlumno> EmocionesAlumnos => Database.GetCollection<EmocionAlumno>(CollectionNames.EmocionesAlumnos);

		private readonly string EmotiCuboDbName = "EmotiCubo";

		private static class CollectionNames
		{
			public static readonly string Cubos = "Cubos";
			public static readonly string Sesiones = "Sesiones";
			public static readonly string Emociones = "Emociones";
			public static readonly string EmocionesAlumnos = "EmocionAlumno";
		}

        // public GridFSBucket ImagesBucket { get; set; } //  ImagesBucket = new GridFSBucket(Database);

        private const string defaultMongoDbUrl = "mongodb://localhost";

        public MongoContext(string mongoDbUrl = defaultMongoDbUrl)
		{
			if (String.IsNullOrWhiteSpace(mongoDbUrl)) {
				mongoDbUrl = defaultMongoDbUrl;
			}
			var settings = MongoClientSettings.FromUrl(new MongoUrl(mongoDbUrl));
			// settings.ClusterConfigurator = builder => builder.Subscribe(new Log4NetMongoEvents());
			var client = new MongoClient(settings);
			Database = client.GetDatabase(EmotiCuboDbName);
		}

	}
}
