using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Net;

public class Usuario {
	public String nombre;
	public String apellidos;
	public String usuarioBGG;
	public static String fechaRegistro;


	public Usuario(String nombreUsuario,String rutaCacheUsuarios) {

		XmlDocument documentoUsuario;
		if(File.Exists(rutaCacheUsuarios+nombreUsuario)) {
			documentoUsuario=new XmlDocument();
			documentoUsuario.Load(rutaCacheUsuarios+nombreUsuario);
		} else {
			documentoUsuario=Consultas.consultarApiUsuario(nombreUsuario);
		}


		nombre=documentoUsuario.DocumentElement.SelectSingleNode("/user/firstname").Attributes["value"].Value;
		apellidos=documentoUsuario.DocumentElement.SelectSingleNode("/user/lastname").Attributes["value"].Value;
		usuarioBGG=documentoUsuario.DocumentElement.SelectSingleNode("/user").Attributes["name"].Value;
		fechaRegistro="";
		fechaRegistro=documentoUsuario.DocumentElement.SelectSingleNode("/user/yearregistered").Attributes["value"].Value;
		if(fechaRegistro=="") {
			throw new Exception("No existe usuario");
		}
		documentoUsuario.Save(rutaCacheUsuarios+nombreUsuario);
	}

	public String getFechaRegistro() {
		return "La fecha de creación de cuenta fue: "+fechaRegistro;
	}

}
