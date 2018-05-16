
function SesionesVM() {
	var self = this;
	self.cuboId = ko.observable();
	self.cuboId('5a945a56c65d263a9077c5f0'); // For demo purposes.
	self.logedIn = ko.observable(false);
	self.entrarEnable = ko.computed(function () {
		return self.cuboId() != null && self.cuboId().length > 0;
	});

	self.widgetPie = ko.observable();
	self.sesionActual = ko.observable();
	self.sesionActualSeriesData = ko.observableArray();
	self.sesionActual.subscribe(function (sesionActual) {
		
		var alegria = {
			category: 'Alegría'
			, name: 'Alegría'
			,color: '#FFC400'
			,value: sesionActual.emocionesAlumnos.filter(function (em) {
				return em.emocionId == 1;
			}).length
		};
		var curiosidad = {
			category: 'Curiosidad'
			, name: 'Curiosidad'
			,color: '#41BEFD'
			,value: sesionActual.emocionesAlumnos.filter(function (em) {
				return em.emocionId == 2;
			}).length
		};
		var culpa = {
			category: 'Culpa'
			, name: 'Culpa'
			,color: '#915BB5'
			,value: sesionActual.emocionesAlumnos.filter(function (em) {
				return em.emocionId == 3;
			}).length
		};
		var tristeza = {
			category: 'Tristeza'
			, name: 'Tristeza'
			,color: '#3B41F7'
			,value: sesionActual.emocionesAlumnos.filter(function (em) {
				return em.emocionId == 4;
			}).length
		};
		var miedo = {
			category: 'Miedo'
			,name: 'Miedo'
			,color: '#140044'
			,value: sesionActual.emocionesAlumnos.filter(function (em) {
				return em.emocionId == 5;
			}).length
		};
		var rabia = {
			category: 'Rabia'
			,name: 'Rabia'
			,color: '#FF3737'
			,value: sesionActual.emocionesAlumnos.filter(function (em) {
				return em.emocionId == 6;
			}).length
		};

		self.sesionActualSeriesData([
			 alegria
			,curiosidad
			,culpa
			,tristeza
			,miedo
			,rabia
		]);
		/*
		if (self.widgetPie() != null) {
			self.refreshGraphs(self.widgetPie(), self.sesionActualSeriesData());
		}
		*/
		self.sesionActualChartBarsDomId = 'sesionActualChartBars';
		$('#' + self.sesionActualChartBarsDomId).kendoChart({
			title: {
				position: 'bottom',
				text: 'Emociones en este momento.'
			}
			,legend: {
				visible: true
				,position: 'bottom'
			}
			,tooltip: {
				visible: true
				,template: '#= name # #= value #'
			}

			,series: [{
				//type: 'pie',
				data: self.sesionActualSeriesData()
			}]
		});


		self.sesionActualChartPieDomId = 'sesionActualChartPie';
		$('#' + self.sesionActualChartPieDomId).kendoChart({
			 title: {
				position: 'bottom'
				//,text: 'Emociones en este momento.'
			}
			,legend: {
				visible: true
				,position: 'bottom'
			}
			,tooltip: {
				visible: true
				,template: '#= category #: #= value #'
			}
			,seriesDefaults: {
				labels: {
					visible: true,
					background: "transparent",
					template: "#= category # #= value#"
				}
			}
			,series: [{
				 type: 'pie'
				,data: self.sesionActualSeriesData()
			}]
		});

	});
	self.sesionActualNombre = ko.computed(function () {
		var sesion = self.sesionActual();
		if (sesion == null) {
			return '';
		}
		return sesion.nombre;
	});
	self.sesiones = ko.observableArray();
	//self.sesiones.push({});
};

SesionesVM.prototype.loginCuboId = function () {
	this.logedIn(true);
	this.getSesionActual();
	this.getSesiones();
};


SesionesVM.prototype.logout = function () {
	this.logedIn(false);
};


SesionesVM.prototype.getSesionActual = function () {
	var self = this;
	var urlSesionActual = '/api/Session/Actual/' + this.cuboId();
	$.get(urlSesionActual).then(function (data) {
        // self.addSesionMockData(data);
		self.sesionActual(data);
	});
};

SesionesVM.prototype.addSesionMockData = function (sesionData) {
    sesionData.emocionesAlumnos.push(
        {
            id: 1
            , fecha: '2018-02-27T00:00:00'
            , emocionId: 1
            , emocion: 'Alegria'
        }
        , { emocionId: 1 }
        , { emocionId: 1 }
        , { emocionId: 2 }
        , { emocionId: 2 }
        , { emocionId: 3 }
        , { emocionId: 3 }
        , { emocionId: 4 }
        , { emocionId: 5 }
        , { emocionId: 6 }
    );
};


SesionesVM.prototype.getSesiones = function () {
	var self = this;
	var urlSesionesCubo = '/api/Session/Cubo/' + this.cuboId();
	$.get(urlSesionesCubo).then(function (dataSesionesCubo) {
		self.sesiones(dataSesionesCubo);
	});
};

SesionesVM.prototype.terminarSesionActual = function () {
	var self = this;
	var urlTerminarSesionActual = '/api/Session/Actual/Terminar/' + this.cuboId();
	$.get(urlTerminarSesionActual).then(function () {
		self.getSesionActual();
	});
};

SesionesVM.prototype.refreshGraphs = function (widget, value) {
	var self = this;
	widget.options.series = value;
	widget._sourceSeries = value;
	widget.refresh();
	widget.redraw();
};

var sesionesVM = new SesionesVM();
var sesionesVMDomElementId = 'sesionesVM';
ko.applyBindings(sesionesVM, document.getElementById(sesionesVMDomElementId));
