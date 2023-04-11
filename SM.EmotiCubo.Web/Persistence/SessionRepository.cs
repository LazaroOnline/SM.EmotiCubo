using System;
using System.Web;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using SM.EmotiCubo.Web.Models;

namespace SM.EmotiCubo.Web.Persistence
{
	public class SessionRepository
	{
		public MongoContext Context;

		public SessionRepository(string mongoDbUrl = null)
		{
			Context = new MongoContext(mongoDbUrl);
		}

		public void Insert(Sesion sesion)
		{
			Context.Sesiones.InsertOne(sesion);
		}

		public IEnumerable<Sesion> GetAll()
		{
			return Context.Sesiones.Find(s => true).ToList();
		}

		public IEnumerable<Sesion> GetByCubo(string cuboId)
		{
			//return Context.Sesiones.AsQueryable().Where(s => s.CuboId == cuboId).FirstOrDefault();
			return Context.Sesiones.Find(s => s.CuboId == cuboId)
				.ToList();
		}

		//public Session Get(ObjectId id)
		public Sesion Get(string sesionId)
		{
			//return Context.Sesiones.AsQueryable().Where(s => s.Id == id).FirstOrDefault();
			return Context.Sesiones.Find(s => s.Id == sesionId)
				.FirstOrDefault();
		}

		public Sesion GetOrCreateActiveSession(string cuboId)
		{
			var activeSession = GetActiveSesion(cuboId);
			if (activeSession == null) {
				activeSession = new Sesion {
					 CuboId = cuboId
					,Id = ObjectId.GenerateNewId().ToString()
					,Fecha = DateTime.UtcNow.Date
					,Cerrada = false
					,Nombre = DateTime.UtcNow.Date.ToString("yyyy-MM-dd")
					,EmocionesAlumnos = new List<EmocionAlumno>()
				};
				Insert(activeSession);
			}
			return activeSession;
		}

		public Sesion GetActiveSesion(string cuboId)
		{
			return Context.Sesiones.Find(s => 
					s.CuboId == cuboId 
					&& s.Fecha == DateTime.UtcNow.Date 
					&& s.Cerrada == false
				)
				.FirstOrDefault();
		}

		public IEnumerable<Sesion> GetByCuboId(string cuboId)
		{
			return Context.Sesiones.Find(s => s.CuboId == cuboId).ToList();
		}

		public IEnumerable<Sesion> GetByGroup(string group)
		{
			return Context.Sesiones.Find(s => s.Grupos.Contains(group)).ToList();
		}



		public void AddEmocion(string sesionId, EmocionAlumno emocionAlumno)
		{
			if (String.IsNullOrWhiteSpace(emocionAlumno.Id)) { // TODO: test if this is optional and delete it if unnecessary
				emocionAlumno.Id = ObjectId.GenerateNewId().ToString();
			}
			/*
			var sesion = Get(sesionId);
			sesion.EmocionesAlumnos.Add(emocionAlumno);
			//Context.Rentals.Save(rental);
			Context.Sesiones.ReplaceOne(r => r.Id == sesionId, sesion);
			*/
			Context.Sesiones.UpdateOne(s => s.Id == sesionId
				,Builders<Sesion>.Update.Push(s => s.EmocionesAlumnos, emocionAlumno)
			);
		}

		/* TODO:
		public void RemoveEmocion(string sesionId, string emocionAlumnoId)
		{
			Context.Sesiones.UpdateOne(s => s.Id == sesionId
				, Builders<Sesion>.Update.Pull(s => s.EmocionesAlumnos, Builders<EmocionAlumno>.Filter.Eq(s => s.Id, emocionAlumnoId).ToBson()
			);

			Context.Sesiones.UpdateOne(s => s.Id == sesionId
				, Builders<Sesion>.Update.Pull(s => s.EmocionesAlumnos, emocionAlumno // <-- requires the whole object!
			);
		}
		*/

		public void AddGroup(string sesionId, string group)
		{
			Context.Sesiones.UpdateOne(s => s.Id == sesionId
				,Builders<Sesion>.Update.AddToSet(s => s.Grupos, group)
			);
		}

		public void RemoveGroup(string sesionId, string group)
		{
			Context.Sesiones.UpdateOne(s => s.Id == sesionId
				, Builders<Sesion>.Update.Pull(s => s.Grupos, group)
			);
		}

		public void Rename(string sesionId, string nombre)
		{
			Context.Sesiones.UpdateOne(s => s.Id == sesionId
				,Builders<Sesion>.Update.Set(s => s.Nombre, nombre)
			);
		}

		public void Close(string sesionId, bool closed = true)
		{
			Context.Sesiones.UpdateOne(s => s.Id == sesionId
				, Builders<Sesion>.Update.Set(s => s.Cerrada, closed)
			);
		}

	}
}
