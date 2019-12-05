using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Net;
using System.Drawing;
using System.Windows.Forms;

namespace Final {
	public class Colecccion {

		//Múltiples diccionarios para las diferentes ramas que se agregan
		//public String masJugado;
		//public Dictionary<String,String> masJug = new Dictionary<string,string>();
		private static int masJugCant = 0;
		private static String masJugName = "";
		private static int menosJugCant = 10000;
		private static String menosJugName = "";

		public String totalJuegos;
		public XmlDocument documentoJuego;
		public SortedDictionary<String,List<Juego>> coleccionUsuario = new SortedDictionary<string,List<Juego>>();
		public SortedDictionary<int,List<Juego>> cantidadJugJuegos = new SortedDictionary<int,List<Juego>>();
		public SortedDictionary<String,List<Juego>> mecanicas = new SortedDictionary<string,List<Juego>>();
		public SortedDictionary<String,List<Juego>> familias = new SortedDictionary<string,List<Juego>>();
		public SortedDictionary<String,List<Juego>> categorías = new SortedDictionary<string,List<Juego>>();
		public Dictionary<String,String> vecesJugado = new Dictionary<string,string>();


		public Colecccion() {

		}

		//Actualizacion de archivo, sirve incluso despues de 1 min de descargado
		public void actualizarArchivoColeccion(String usuario,String rutaCacheColeccion) {
			String archivo = rutaCacheColeccion+usuario;
			if(File.Exists(archivo)) {
				String fechaArchivo = Convert.ToString(File.GetLastWriteTime(archivo));
				Console.WriteLine(File.GetLastWriteTime(archivo));
				if(fechaArchivo!=Convert.ToString(DateTime.Now)) {
					try {
						XmlDocument documento = new XmlDocument();
						documento=Consultas.consultarApiColeccion(usuario);
						documento.Save(rutaCacheColeccion+usuario);
						documentoJuego=leerColeccionUsuario(rutaCacheColeccion,usuario);
					} catch {
					}
				}
			}
		}


		//Asignación de valor nuevo al xml
		XmlDocument leerColeccionUsuario(String rutaCacheColeccion,String usuario) {
			XmlDocument documentoColeccion = new XmlDocument();
			documentoColeccion.Load(rutaCacheColeccion+usuario);
			return documentoColeccion;
		}
		//Constructor, solo se verifica si existe el archivo de coleccion
		//ademas de guardar el diccionario de datos, que es la coleccion 
		//que se usa en el form
		public Colecccion(String usuario,String rutaJuego,String rutaUsuario) {
			//XmlDocument documentoJuego;
			if(File.Exists(rutaJuego+"colecciones/"+usuario)) {
				documentoJuego=new XmlDocument();
				documentoJuego.Load(rutaJuego+"colecciones/"+usuario);

			} else {

				//Si no funciona, es que no hay conexión
				try {
					documentoJuego=Consultas.consultarApiColeccion(usuario);
					if(verSiExisteUsuario(usuario)) {
						asegurarExistenciaDirectorioColecciones(rutaJuego);
						//asegurarExistenciaDirectorioUsers(rutaUsuario,usuario);
						documentoJuego.Save(rutaJuego+"colecciones/"+usuario);
					} else {
						MessageBox.Show("No existe usuario");
					}

				} catch {
					//throw new Exception("no hay conexion");
					MessageBox.Show("No hay conexión, y no se encuentra en caché");
				}


			}

			try {
				tomarNodosJuego(documentoJuego,rutaJuego,rutaUsuario,usuario);
				totalJuegos="El total de juegos en la colección es: ";
				totalJuegos+=documentoJuego.DocumentElement.SelectSingleNode("/items").Attributes["totalitems"].Value;
				//Console.WriteLine("Juegos "+totalJuegos);
				//	guardarMasAutores();
			} catch {

			}

		}



		//Se van tomando uno por uno cada nodo, para ir agregando a los autores
		public void tomarNodosJuego(XmlDocument document,String rutaJuego,String rutaUsuario,String usuario) {
			masJugCant=0;
			masJugName="";
			menosJugCant=10000;
			menosJugName="";
			Juego tomarJuegos;
			int x = 0;
			foreach(XmlNode node in document.SelectNodes("/items/item")) {
				Thread.CurrentThread.IsBackground=true;
				tomarJuegos=new Juego(node.Attributes["objectid"].Value,rutaJuego,rutaUsuario,usuario);
				agregarJuegoAutor(tomarJuegos);
				agregarJuegoCantidad(tomarJuegos);
				agregarJuegoFamilias(tomarJuegos);
				agregarJuegoMecanica(tomarJuegos);
				agregarJuegoCategoria(tomarJuegos);
				String help = document.DocumentElement.SelectNodes("/items/item/numplays").Item(x).InnerText;
				String name = document.DocumentElement.SelectNodes("/items/item/name").Item(x).InnerText;
				vecesJugado.Add(node.Attributes["objectid"].Value,help);


				if(Convert.ToInt32(help)>masJugCant) {
					masJugCant=Convert.ToInt32(help);
					masJugName=name;
				}

				if(Convert.ToInt32(help)<=menosJugCant&&Convert.ToInt32(help)!=0) {
					menosJugCant=Convert.ToInt32(help);
					menosJugName=name;
				}
				x++;
			}

			//Console.WriteLine("Mas jugado es "+masJugName + " con " + masJugCant );
			//Console.WriteLine("Menos jugado es "+menosJugName+" con "+menosJugCant);
		}


		//Aquí se agregan el juego con su respectivo autor
		public void agregarJuegoAutor(Juego agregarJuego) {
			//Si ya existe el autor, solo se agrega la lista de los juegos
			for(int i = 0;i<agregarJuego.autores.Count;i++) {


				List<Juego> listaJuegos;
				if(coleccionUsuario.TryGetValue(agregarJuego.autores[i],out listaJuegos)) {
					listaJuegos.Add(agregarJuego);
					coleccionUsuario[agregarJuego.autores[i]]=listaJuegos;
				} else {
					//Si no existe el autor, se crea
					listaJuegos=new List<Juego>();
					listaJuegos.Add(agregarJuego);
					coleccionUsuario.Add(agregarJuego.autores[i],listaJuegos);
				}
			}

		}


		public String obtenerMasAutores() {
			int cantidad = 0;
			String nombre = "";
			for(int i = 0;i<coleccionUsuario.Count;i++) {
				List<Juego> listaJuegos;
				listaJuegos=coleccionUsuario.ElementAt(i).Value;
				int c = listaJuegos.Count;
				if(cantidad<c) {
					cantidad=c;
					nombre=coleccionUsuario.ElementAt(i).Key;
				}
			}

			if(nombre.Equals("")) {
				return "No tiene autores";
			} else {
				return "El autor que tiene más juegos de la coleccion es "+nombre+" con "+cantidad+" juegos";
			}

		}


		public String getJuegoMasJugado() {
			if(masJugName.Equals("")) {
				return "No se ha jugado alguna partida";
			} else {
				return "El juego más jugado es: "+masJugName+" con "+masJugCant;
			}

		}

		public String getJuegoMenosJugado() {
			if(menosJugName.Equals("")) {
				return "No se ha jugado alguna partida";
			} else {
				return "El juego menos jugado es: "+menosJugName+" con "+menosJugCant;
			}
		}




		//Aqui se agrega el numero con sus respectivos juegos
		public void agregarJuegoCantidad(Juego agregarJuego) {

			for(int i = 0;i<agregarJuego.jugadoresJuego.Count;i++) {
				List<Juego> listaJuegos;
				if(cantidadJugJuegos.TryGetValue(agregarJuego.jugadoresJuego[i],out listaJuegos)) {
					listaJuegos.Add(agregarJuego);
					cantidadJugJuegos[agregarJuego.jugadoresJuego[i]]=listaJuegos;
				} else {
					//Si no existe la cantidad, se crea
					listaJuegos=new List<Juego>();
					listaJuegos.Add(agregarJuego);
					cantidadJugJuegos.Add(agregarJuego.jugadoresJuego[i],listaJuegos);
				}
			}

		}

		//Aqui se agrega la mecánica con sus respectivos juegos
		public void agregarJuegoMecanica(Juego agregarJuego) {

			for(int i = 0;i<agregarJuego.mecanica.Count;i++) {
				List<Juego> listaJuegos;
				if(mecanicas.TryGetValue(agregarJuego.mecanica[i],out listaJuegos)) {
					listaJuegos.Add(agregarJuego);
					mecanicas[agregarJuego.mecanica[i]]=listaJuegos;
				} else {
					//Si no existe la cantidad, se crea
					listaJuegos=new List<Juego>();
					listaJuegos.Add(agregarJuego);
					mecanicas.Add(agregarJuego.mecanica[i],listaJuegos);
				}
			}

		}

		//Aqui se agrega la familia con sus respectivos juegos
		public void agregarJuegoFamilias(Juego agregarJuego) {

			for(int i = 0;i<agregarJuego.familia.Count;i++) {
				List<Juego> listaJuegos;
				if(familias.TryGetValue(agregarJuego.familia[i],out listaJuegos)) {
					listaJuegos.Add(agregarJuego);
					familias[agregarJuego.familia[i]]=listaJuegos;
				} else {
					//Si no existe la cantidad, se crea
					listaJuegos=new List<Juego>();
					listaJuegos.Add(agregarJuego);
					familias.Add(agregarJuego.familia[i],listaJuegos);
				}
			}

		}

		//Aqui se agrega el numero con su respectivo juego
		public void agregarJuegoCategoria(Juego agregarJuego) {

			for(int i = 0;i<agregarJuego.categoria.Count;i++) {
				List<Juego> listaJuegos;
				if(categorías.TryGetValue(agregarJuego.categoria[i],out listaJuegos)) {
					listaJuegos.Add(agregarJuego);
					categorías[agregarJuego.categoria[i]]=listaJuegos;
				} else {
					//Si no existe la cantidad, se crea
					listaJuegos=new List<Juego>();
					listaJuegos.Add(agregarJuego);
					categorías.Add(agregarJuego.categoria[i],listaJuegos);
				}
			}

		}

		public void asegurarExistenciaDirectorioColecciones(String rutaJuego) {
			if(!Directory.Exists(rutaJuego+"colecciones/")) {
				Directory.CreateDirectory(rutaJuego+"colecciones/");
			}
		}



		//Metodo utilizado al momento de querer actualizar la colecció, ya que
		//esta puede cambiar
		public void actualizarColeccion(String usuario,String ruta) {
			XmlDocument documentoXMl = new XmlDocument();
			documentoXMl=Consultas.consultarApiColeccion(usuario);
			documentoXMl.Save(ruta+"colecciones/"+usuario);
		}

		//Verificar si existe usuario
		public Boolean verSiExisteUsuario(String nombreUsuario) {
			XmlDocument doc = Consultas.consultarApiUsuario(nombreUsuario);
			String aux = doc.DocumentElement.SelectSingleNode("/user/yearregistered").Attributes["value"].Value;
			if(aux=="") {
				return false;
			} else {
				return true;
			}
		}


		//Se obtiene las veces jugadas de cada juego, al pedir el id
		public String obtenerVecesJugado(String id) {
			String vecesJug = "";
			vecesJugado.TryGetValue(id,out vecesJug);
			return vecesJug;
		}
	}


}
