using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using SM.EmotiCubo.Web.Models;
using SM.EmotiCubo.Web.Persistence;

namespace SM.EmotiCubo.Web.Services
{
    public class SessionService
    {
		private SessionRepository _sessionsRep { get; set; }

		public SessionService(SessionRepository sessionRepository)
		{
			_sessionsRep = sessionRepository;
		}


		public IEnumerable<EmocionAlumnoBI> GetAllBI()
		{
			var sesiones = _sessionsRep.GetAll();
			var sesionesBi = sesiones.SelectMany(s => ConvertToBI(s)).ToList();
			return sesionesBi;
		}

		private IEnumerable<EmocionAlumnoBI> ConvertToBI(Sesion sesion)
		{
			return sesion.EmocionesAlumnos.Select(e => ConvertToBI(e)).ToList();
		}

		private EmocionAlumnoBI ConvertToBI(EmocionAlumno emocionAlumno)
		{
			var clases = new string[] { "PrimariaA", "PrimariaB", "PrimariaD", "PrimariaC" };
			var materias = new string[] { "Inglés", "Religión", "Lengua y literatura", "Música" };
			var rnd = new Random();
			return new EmocionAlumnoBI {
				 Clase= "PrimariaA" // clases[rnd.Next(0, clases.Length)]
				,Materia = "Música" // clases[rnd.Next(0, clases.Length)]
				,EmocionId = emocionAlumno.EmocionId
				,Fecha = emocionAlumno.Fecha.Date
			};
		}


		public string GetDescription(EmocionesCubo emocion)
		{
			return GetDescription((int)emocion);
		}

		public string GetDescription(int emocionId)
		{
			switch (emocionId)
			{
				case 1: return "Alegría"; //nameof(EmocionesCubo.Alegria); // +Acento
				case 2: return nameof(EmocionesCubo.Curiosidad);
				case 3: return nameof(EmocionesCubo.Culpa);
				case 4: return nameof(EmocionesCubo.Tristeza);
				case 5: return nameof(EmocionesCubo.Miedo);
				case 6: return nameof(EmocionesCubo.Rabia);
			}
			throw new NotImplementedException(nameof(emocionId) + ": " + emocionId);
		}

		#region Rep

		public void Insert(Sesion sesion)
		{
			_sessionsRep.Insert(sesion);
		}

		public IEnumerable<Sesion> GetAll()
		{
			return _sessionsRep.GetAll();
		}

		public IEnumerable<Sesion> GetByCubo(string cuboId)
		{
			return _sessionsRep.GetByCubo(cuboId);
		}

		//public Sesion Get(ObjectId id)
		public Sesion Get(string sesionId)
		{
			return _sessionsRep.Get(sesionId);
		}

		public Sesion GetOrCreateActiveSession(string cuboId)
		{
			return _sessionsRep.GetOrCreateActiveSession(cuboId);
		}

		public Sesion GetActiveSesion(string cuboId)
		{
			return _sessionsRep.GetActiveSesion(cuboId);
		}

		public IEnumerable<Sesion> GetByCuboId(string cuboId)
		{
			return _sessionsRep.GetByCuboId(cuboId);
		}

		public IEnumerable<Sesion> GetByGroup(string group)
		{
			return _sessionsRep.GetByGroup(group);
		}

		public void AddEmocion(string sesionId, EmocionAlumno emocionAlumno)
		{
			_sessionsRep.AddEmocion(sesionId, emocionAlumno);
		}

		/* TODO:
		public void RemoveEmocion(string sesionId, string emocionAlumnoId)
		{
			return _sessionsRep.RemoveEmocion(sesionId, emocionAlumnoId);
		}
		*/

		public void AddGroup(string sesionId, string group)
		{
			_sessionsRep.AddGroup(sesionId, group);
		}

		public void RemoveGroup(string sesionId, string group)
		{
			_sessionsRep.RemoveGroup(sesionId, group);
		}

		public void Rename(string sesionId, string nombre)
		{
			_sessionsRep.Rename(sesionId, nombre);
		}

		public void Close(string sesionId, bool closed = true)
		{
			_sessionsRep.Close(sesionId, closed);
		}
		#endregion
	}
}
