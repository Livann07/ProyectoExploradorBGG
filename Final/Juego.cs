using System;
using System.Web;
using System.Xml;
using System.IO;
using System.Net;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Juego {
	public String id;
	public String nombreJuego;
	public String autor;
	public String ilustrador;
	private String rutaImagen;
	public String cantidadJugado;
	public List<String> autores = new List<string>();
	public List<String> ilustradores = new List<string>();
	public List<int> jugadoresJuego = new List<int>();
	public List<String> mecanica = new List<string>();
	public List<String> familia = new List<string>();
	public List<String> categoria = new List<string>();

	public int cantidadVecesJug;
	public int cantMin;
	public int cantMax;
	public Image img;
	public bool cooperativo = false;
	public Juego(String idJuego,String rutaCacheJuego,String rutaCacheUsuario,String usuario) {

		XmlDocument documentoJuego;
		//XmlDocument documentoUsuario;

		if(File.Exists(rutaCacheJuego+idJuego)) {
			documentoJuego=new XmlDocument();
			documentoJuego.Load(rutaCacheJuego+idJuego);
		} else {
			documentoJuego=Consultas.consultarApiJuego(idJuego);
			//System.Threading.Thread.Sleep(2000);
		}

		/*
		if(File.Exists(rutaCacheUsuario+usuario)) {
			documentoUsuario=new XmlDocument();
			documentoUsuario.Load(rutaCacheUsuario+usuario);
		} else {
			documentoUsuario=Consultas.consultarApiColeccion(usuario);
			System.Threading.Thread.Sleep(2000);
		}*/


		id=idJuego;



		nombreJuego=documentoJuego.DocumentElement.SelectSingleNode("/items/item/name").Attributes["value"].Value;
		XmlNodeList nilustradores = documentoJuego.DocumentElement.SelectNodes("/items/item/link[@type='boardgameartist']");
		XmlNodeList node = documentoJuego.SelectNodes("/items/item/link[@type='boardgamedesigner']");
		XmlNodeList nodeMecanica = documentoJuego.SelectNodes("/items/item/link[@type='boardgamemechanic']");
		XmlNodeList nodeFamilia = documentoJuego.SelectNodes("/items/item/link[@type='boardgamefamily']");
		XmlNodeList nodeCategoría = documentoJuego.SelectNodes("/items/item/link[@type='boardgamecategory']");

		//Agregando autores
		for(int i = 0;i<node.Count;i++) {
			autores.Add(node.Item(i).Attributes["value"].Value);
		}
		if(node.Count==0) {
			autores.Add("(Uncredited)");
		}

		//Agregar ilustradores
		for(int i = 0;i<nilustradores.Count;i++) {
			ilustradores.Add(nilustradores.Item(i).Attributes["value"].Value);
		}
		if(nilustradores.Count==0) {
			ilustradores.Add("(Uncredited)");
		}


		//Agregando mecanica
		for(int i = 0;i<nodeMecanica.Count;i++) {
			mecanica.Add(nodeMecanica.Item(i).Attributes["value"].Value);
			if(nodeMecanica.Item(i).Attributes["value"].Value.Equals("Cooperative Game")) {
				cooperativo=true;
			}
		}
		if(nodeMecanica.Count==0) {
			mecanica.Add("Sin mecánica");
		}


		//Agregando familia

		for(int i = 0;i<nodeFamilia.Count;i++) {
			familia.Add(nodeFamilia.Item(i).Attributes["value"].Value);
		}
		if(nodeFamilia.Count==0) {
			familia.Add("Sin familia");
		}

		//Agregando categoría

		for(int i = 0;i<nodeCategoría.Count;i++) {
			categoria.Add(nodeCategoría.Item(i).Attributes["value"].Value);
		}
		if(nodeCategoría.Count.Equals(0)) {
			categoria.Add("Sin categoría");
		}

		try {
			rutaImagen=documentoJuego.DocumentElement.SelectSingleNode("/items/item/thumbnail").InnerText;
		} catch {
		}

		try {
			cantMin=Convert.ToInt32(documentoJuego.DocumentElement.SelectSingleNode("/items/item/minplayers").Attributes["value"].Value);
		} catch {
			cantMin=1;
		}

		try {
			cantMax=Convert.ToInt32(documentoJuego.DocumentElement.SelectSingleNode("/items/item/maxplayers").Attributes["value"].Value);
		} catch {
		}


		for(int i = cantMin;i<=cantMax;i++) {
			jugadoresJuego.Add(i);
		}

		if(nombreJuego=="") {
			throw new Exception("No existe juego");
		}

		documentoJuego.Save(rutaCacheJuego+idJuego);

		if(!verificarExistenciaIMG(rutaCacheJuego,this.id)) {
			guardarImagen(this.rutaImagen,rutaCacheJuego,this.id);
		}

		img=cargarImagen(rutaCacheJuego,id);

	}

	public Juego(String idJuego,String rutaCacheJuego) {
		XmlDocument documentoJuego;
		//XmlDocument documentoUsuario;

		if(File.Exists(rutaCacheJuego+idJuego)) {
			documentoJuego=new XmlDocument();
			documentoJuego.Load(rutaCacheJuego+idJuego);
		} else {
			documentoJuego=Consultas.consultarApiJuego(idJuego);
			//System.Threading.Thread.Sleep(2000);
		}
		nombreJuego=documentoJuego.DocumentElement.SelectSingleNode("/items/item/name").Attributes["value"].Value;
		XmlNodeList nodeMecanica = documentoJuego.SelectNodes("/items/item/link[@type='boardgamemechanic']");
		for(int i = 0;i<nodeMecanica.Count;i++) {
			// mecanica.Add(nodeMecanica.Item(i).Attributes["value"].Value);
			if(nodeMecanica.Item(i).Attributes["value"].Value.Equals("Cooperative Game")) {
				cooperativo=true;
			}
		}
	}
	public Boolean verificarExistenciaIMG(String rutaJuego,String id) {
		return File.Exists(rutaJuego+"/img/thubmnail/"+id+".jpg");
	}

	public Image cargarImagen(String rutaJuego,String idJuego) {
		return Image.FromFile(rutaJuego+"img/thubmnail/"+idJuego+".jpg");
	}
	public void guardarImagen(String rutaPagina,String rutaJuego,String id) {
		asegurarExistenciaDirectorioMiniaturas(rutaJuego);
		WebClient clienteDescarga = new WebClient();
		clienteDescarga.DownloadFile(rutaPagina,rutaJuego+"img/thubmnail/"+id+".jpg");

	}
	//Creando directorio de miniaturas
	public void asegurarExistenciaDirectorioMiniaturas(String rutaJuego) {
		if(!Directory.Exists(rutaJuego+"/img/thubmnail/")) {
			Directory.CreateDirectory(rutaJuego+"img/thubmnail/");
		}
	}



}
