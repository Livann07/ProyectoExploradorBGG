using System;
using System.Web;
using System.Xml;
using System.IO;
using System.Net;

public class Consultas {
	public WebException pro;
	public Consultas() {

	}

	private static XmlDocument consultarApi(String URL) {

		int delay = 500;
		WebResponse respuesta = null;
		while(respuesta==null&delay<60000) {
			try {
				WebRequest peticion = WebRequest.Create(URL);
				respuesta=peticion.GetResponse();
			} catch(WebException status) {
				System.Threading.Thread.Sleep(delay);
				delay+=500;
			}
		}
		Stream flujo = respuesta.GetResponseStream();
		StreamReader lectorFlujo = new StreamReader(flujo);
		String cadenaRespuesta = lectorFlujo.ReadToEnd();
		XmlDocument xmlRespuesta = new XmlDocument();
		xmlRespuesta.LoadXml(cadenaRespuesta);
		respuesta.Close();
		return xmlRespuesta;
	}

	public static XmlDocument consultarApiUsuario(String usuario) {
		String urlConsulta = "https://www.boardgamegeek.com/xmlapi2/user?name="+usuario;
		if(VerificarConexionURL(urlConsulta)) {
			return consultarApi(urlConsulta);
		} else {
			throw new Exception("no hay conexion");
		}
	}

	public static XmlDocument consultarApiJuego(String idJuego) {
		String urlConsulta = "https://www.boardgamegeek.com/xmlapi2/thing?id="+idJuego;
		return consultarApi(urlConsulta);
	}

	public static XmlDocument consultarApiColeccion(String usuario) {
		String urlConsulta = "https://api.geekdo.com/xmlapi2/collection?username="+usuario+"&own=1";
		if(VerificarConexionURL(urlConsulta)) {
			return consultarApi(urlConsulta);
		} else {
			throw new Exception("no hay conexion");
		}

	}

	public static XmlDocument consultarPartidasJuego(String usuario,String idJuego) {
		String urlConsulta = "https://www.boardgamegeek.com/xmlapi2/plays?username="+usuario+"&id="+idJuego;
		return consultarApi(urlConsulta);
	}

	//Verifica si existe conexión a internet, debido a que, cuando se tiene un
	//usuario que no ha sido guardado en caché, y se busca sin conexión
	//se trababa el programa
	public static bool VerificarConexionURL(string mURL) {
		WebRequest Peticion = default(System.Net.WebRequest);
		WebResponse Respuesta = default(System.Net.WebResponse);
		try {
			Peticion=System.Net.WebRequest.Create(mURL);
			Respuesta=Peticion.GetResponse();
			Respuesta.Close();
			return true;
		} catch(System.Net.WebException ex) {
			if(ex.Status==System.Net.WebExceptionStatus.NameResolutionFailure) {
				return false;
			}
			return false;
		}

	}

}
