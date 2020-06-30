using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Buffers;
using System.Net.Http.Headers;
using SM.EmotiCubo.Web.Models;
using SM.EmotiCubo.Web.Services;
using SM.EmotiCubo.Web.Persistence;
using SM.EmotiCubo.Web.Controllers.Models;


namespace SM.EmotiCubo.Web.Controllers
{
    [Route("api/[controller]")]
    public class SessionController : Controller
    {
		private AppSettings _appSettings { get; set; }
		private SessionService sessions { get; set; }

		public SessionController(IOptions<AppSettings> settings)
		{
			_appSettings = settings.Value;
			var sessionRepository = new SessionRepository(_appSettings.MongoDbUrl);
			sessions = new SessionService(sessionRepository);
		}

		// For BI export demo
		// $.get("/api/Session/Get");
		// GET: api/Session
		[HttpGet]
		public IEnumerable<Sesion> Get()
		{
			return sessions.GetAll();
		}

		[HttpGet("Cubo/{cuboId}")]
		public IEnumerable<Sesion> GetByCubo(string cuboId)
		{
			return sessions.GetByCubo(cuboId);
		}

		//  
		[HttpGet("BI")]
		public IEnumerable<EmocionAlumnoBI> GetBI()
		{
			return sessions.GetAllBI();
		}

		[HttpGet("BIPascal")]
		public JsonResult GetBIPascal()
		{
			var sessionsBI = sessions.GetAllBI();
			var serializer = new JsonSerializerSettings();
			serializer.ContractResolver = new DefaultContractResolver();
			return Json(sessionsBI, serializer);
		}

		// GET: api/Session/5/6
		[HttpGet("{cuboId}/{sessionId}")]
		public Sesion Get(string cuboId, string sessionId)
		{
			var sesion = sessions.Get(sessionId);
			return sesion;
		}


		// GET: api/Session/5
		[HttpGet("Actual/{cuboId}")]
		public Sesion GetSesionActual(string cuboId)
		{
			return sessions.GetOrCreateActiveSession(cuboId);
		}

		[HttpGet("Actual/Terminar/{cuboId}")]
		public void TerminarSesionActual(string cuboId)
		{
			var sesionActiva = sessions.GetOrCreateActiveSession(cuboId);
			sessions.Close(sesionActiva.Id);
		}

        // $.post("/api/Session/Post", {CuboId: '5a945a56c65d263a9077c5f0', EmocionId: 2});
        // POST: api/Session
        [HttpPost()]
        public void Post([FromBody]EmocionAlumnoPost emocionAlumnoPost)
        {
            _post(emocionAlumnoPost);
        }

        // GET: /api/Session/Save/2         $.get("/api/Session/Save/3");
        [HttpGet("Save/{emocionId}")]
        public void Save(int emocionId)
        {
            var emocionAlumnoPost = new EmocionAlumnoPost {
                 CuboId = "5a945a56c65d263a9077c5f0"
                ,EmocionId = emocionId
            };
            _post(emocionAlumnoPost);
        }

        private void _post(EmocionAlumnoPost emocionAlumnoPost)
		{
			var sesionActual = sessions.GetOrCreateActiveSession(emocionAlumnoPost.CuboId);
			var emocionAlumno = new EmocionAlumno
			{
				 EmocionId = emocionAlumnoPost.EmocionId
				,Emocion = sessions.GetDescription(emocionAlumnoPost.EmocionId)
				,Fecha = DateTime.UtcNow
			};
			sessions.AddEmocion(sesionActual.Id, emocionAlumno);
			Console.WriteLine("Emocion: " + emocionAlumno.Id + " - " + emocionAlumno.Emocion);
		}
	}
}
