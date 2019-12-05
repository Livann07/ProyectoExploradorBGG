using Final;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class Plays {
	XmlDocument registro;
	public SortedDictionary<String,String> listaGanados = new SortedDictionary<string,string>();
	public SortedDictionary<String,String> listaPerdidos = new SortedDictionary<string,string>();
	public SortedDictionary<String,SortedDictionary<String,String>> partidasGanadas = new SortedDictionary<string,SortedDictionary<string,string>>();
	public SortedDictionary<String,SortedDictionary<String,String>> partidasPerdidas = new SortedDictionary<string,SortedDictionary<string,string>>();
	//Se inserta el nombre del juego, y las veces que ganó con cada usuario
	public SortedDictionary<String,SortedDictionary<String,String>> partGanadasContraAdversario = new SortedDictionary<string,SortedDictionary<string,string>>();
	public SortedDictionary<String,SortedDictionary<String,String>> partPerdidasContraAdversario = new SortedDictionary<string,SortedDictionary<string,string>>();
	//Se inserta el usuario, y las veces que ganó en cada juego
	public SortedDictionary<String,SortedDictionary<String,String>> partAdversarioJuegosGanados = new SortedDictionary<string,SortedDictionary<string,string>>();
	public SortedDictionary<String,SortedDictionary<String,String>> partAdversarioJuegosPerdidos = new SortedDictionary<string,SortedDictionary<string,string>>();
	public SortedList<String,String> adversarios = new SortedList<string,string>();
	public SortedDictionary<String,String> juegos = new SortedDictionary<String,String>();


	public Plays(String rutaCacheUsuarios,Colecccion coleccion,String usuario,String rutaCache) {

		//Primero se verifica que los archivos estén
		try {
			verificarExistenciaJuego(rutaCacheUsuarios,coleccion,usuario);
		} catch {
		}
		//verificarExistenciaJuego(rutaCacheUsuarios, coleccion, usuario);
		try {
			//Se calculan ganadas y perdidas de cada numero de jugadores
			calcularGanadasYPerdidas(rutaCacheUsuarios,coleccion,usuario);
		} catch {
		}

		try {
			//Se calculan ganadas y perdidas, pero de cada adversario o juego
			calcularGanadasYPerdidasContraAdversario(rutaCacheUsuarios,coleccion,usuario,rutaCache);
			llenarlistaGanados();
			llenarlistaPerdidos();
		} catch {
		}


	}
	public Plays() {

	}

	//Utilizado para reemplazar el archivo de plays en dado caso tenga uno antiguo
	public void actualizarArchivosPlays(String rutaCachePlaysUsuario,XmlDocument coleccion,String usuario) {
		try {
			int totalColeccion = Convert.ToInt32(coleccion.DocumentElement.SelectSingleNode("/items").Attributes["totalitems"].Value);
			for(int x = 0;x<totalColeccion;x++) {
				String id = coleccion.DocumentElement.SelectNodes("/items/item").Item(x).Attributes[1].Value;
				String archivo = rutaCachePlaysUsuario+id;
				if(File.Exists(archivo)) {
					String fechaArchivo = Convert.ToString(File.GetLastWriteTime(archivo));
					Console.WriteLine(DateTime.Now);
					if(fechaArchivo!=Convert.ToString(DateTime.Now)) {
						try {
							XmlDocument documento = new XmlDocument();
							documento=Consultas.consultarPartidasJuego(usuario,id);
							documento.Save(rutaCachePlaysUsuario+id);
						} catch {

						}
					}
				}
			}
		} catch {
		}
	}

	//Se checa si existe el archivo xml de los juegos de las plays
	private void verificarExistenciaJuego(String rutaCacheUsuarios,Colecccion coleccion,String usuario) {
		int totalColeccion = Convert.ToInt32(coleccion.documentoJuego.DocumentElement.SelectSingleNode("/items").Attributes["totalitems"].Value);
		for(int x = 0;x<totalColeccion;x++) {
			String id = coleccion.documentoJuego.DocumentElement.SelectNodes("/items/item").Item(x).Attributes[1].Value;
			if(!File.Exists(rutaCacheUsuarios+id)) {
				registro=Consultas.consultarPartidasJuego(usuario,id);
				registro.Save(rutaCacheUsuarios+id);
			}
		}
	}

	private void calcularGanadasYPerdidas(String rutaCacheUsuarios,Colecccion coleccion,String user) {
		XmlDocument playJuego = new XmlDocument();
		int totalVecesJugadas;
		int jugadores;

		//Se cuenta el total de juegos de la colección, para calcular ganadas y perdidas de cada juego
		int totalColeccion = Convert.ToInt32(coleccion.documentoJuego.DocumentElement.SelectSingleNode("/items").Attributes["totalitems"].Value);
		for(int x = 0;x<totalColeccion;x++) {
			String id = coleccion.documentoJuego.DocumentElement.SelectNodes("/items/item").Item(x).Attributes[1].Value;
			//Se carga xml de las play del usuario
			playJuego.Load(rutaCacheUsuarios+id);
			//Total de veces jugadas de cada juego
			totalVecesJugadas=playJuego.DocumentElement.SelectNodes("/plays/play").Count;
			for(int y = 0;y<totalVecesJugadas;y++) {

				try {
					//Contar jugadores del juego especifico
					jugadores=playJuego.DocumentElement.SelectNodes("/plays/play/players").Item(y).ChildNodes.Count;
					for(int z = 0;z<jugadores;z++) {
						//Se obtiene usuario
						String usuario = playJuego.DocumentElement.SelectNodes("/plays/play/players").Item(y).ChildNodes[z].Attributes["username"].Value;
						//Se recorre hasta obtener el usuario
						if(usuario.Equals(user)) {

							String resultadoPartida = playJuego.DocumentElement.SelectNodes("/plays/play/players").Item(y).ChildNodes[z].Attributes["win"].Value;
							if(resultadoPartida.Equals("1")) {
								//Si el diccionario de partidas ganadas no tiene el id del juego
								if(!partidasGanadas.ContainsKey(id)) {
									SortedDictionary<String,String> ganados = new SortedDictionary<string,string>();
									//En partidas de tantos jugadores, ganó
									ganados.Add(Convert.ToString(jugadores),"1");
									partidasGanadas.Add(id,ganados);
								} else {
									SortedDictionary<String,String> ganados;
									partidasGanadas.TryGetValue(id,out ganados);
									//Si no contiene la cantidad de jugadores especifica, lo agrega
									if(!ganados.ContainsKey(Convert.ToString(jugadores))) {
										ganados.Add(Convert.ToString(jugadores),"1");
									} else {
										//Si ya la tiene, aumenta la cantidad y lo elimina
										String num_victorias;
										ganados.TryGetValue(Convert.ToString(jugadores),out num_victorias);
										num_victorias=Convert.ToString(Convert.ToInt32(num_victorias)+1);
										ganados.Remove(Convert.ToString(jugadores));
										ganados.Add(Convert.ToString(jugadores),num_victorias);
									}
									partidasGanadas.Remove(id);
									partidasGanadas.Add(id,ganados);
								}

							} else {

								//En dado caso haya perdido
								//Mismo proceso de ganadas
								if(!partidasPerdidas.ContainsKey(id)) {
									SortedDictionary<String,String> perdidas = new SortedDictionary<string,string>();
									perdidas.Add(Convert.ToString(jugadores),"1");
									partidasPerdidas.Add(id,perdidas);
								} else {
									SortedDictionary<String,String> perdidas;
									partidasPerdidas.TryGetValue(id,out perdidas);
									if(!perdidas.ContainsKey(Convert.ToString(jugadores))) {
										perdidas.Add(Convert.ToString(jugadores),"1");
									} else {
										String num_perdidas;
										perdidas.TryGetValue(Convert.ToString(jugadores),out num_perdidas);
										num_perdidas=Convert.ToString(Convert.ToInt32(num_perdidas)+1);
										perdidas.Remove(Convert.ToString(jugadores));
										perdidas.Add(Convert.ToString(jugadores),num_perdidas);
									}
									partidasPerdidas.Remove(id);
									partidasPerdidas.Add(id,perdidas);
								}
							}
						}
					}
				} catch {
				}

			}
		}
	}

	private void calcularGanadasYPerdidasContraAdversario(String rutaCacheUsuarios,Colecccion coleccion,String user,String rutaCache) {
		XmlDocument playJuego = new XmlDocument();
		int totalVecesJugadas;
		int jugadores;

		//Se obtiene el número total de la colección
		int totalColeccion = Convert.ToInt32(coleccion.documentoJuego.DocumentElement.SelectSingleNode("/items").Attributes["totalitems"].Value);
		for(int x = 0;x<totalColeccion;x++) {
			String id = coleccion.documentoJuego.DocumentElement.SelectNodes("/items/item").Item(x).Attributes[1].Value;
			//Se carga xml de las play del juego
			Juego obJuego = new Juego(id,rutaCache);
			playJuego.Load(rutaCacheUsuarios+id);
			//Se obtiene la cantidad de veces jugadas
			totalVecesJugadas=playJuego.DocumentElement.SelectNodes("/plays/play").Count;
			for(int y = 0;y<totalVecesJugadas;y++) {
				try {
					//Cantidad de jugadores en las partida y
					jugadores=playJuego.DocumentElement.SelectNodes("/plays/play/players").Item(y).ChildNodes.Count;
					for(int z = 0;z<jugadores;z++) {

						//Se obtiene usuario, esperando que sea igual al usuario enviado como parámetro
						String usuario = playJuego.DocumentElement.SelectNodes("/plays/play/players").Item(y).ChildNodes[z].Attributes["username"].Value;
						if(usuario.Equals(user)) {
							//Si el usuario se encuentra, se agrega el juego a la lista, para poder empezar a usarlo
							llenarListaJuegos(id,rutaCache,user,rutaCacheUsuarios);
							//Obtiene si ganó o perdió la partida
							String resultadoPartida = playJuego.DocumentElement.SelectNodes("/plays/play/players").Item(y).ChildNodes[z].Attributes["win"].Value;
							if(resultadoPartida.Equals("1")) {
								//Si ganó y no es cooperativo
								if(!obJuego.cooperativo) {
									for(int n = 0;n<jugadores;n++) {

										String usuarioGanado = playJuego.DocumentElement.SelectNodes("/plays/play/players").Item(y).ChildNodes[n].Attributes["username"].Value;
										//Si no tiene nombre, se le asigna desconocido
										if(usuarioGanado.Equals("")) {
											usuarioGanado="Desconocido";
										}

										//Si el usuario al que se le ganó es diferente del usuario normal
										if(!usuarioGanado.Equals(user)) {
											//Se agrega adversarios y además que se ganó a x usuario
											llenarDicAdversariosGanadas(id,usuarioGanado,rutaCache,rutaCacheUsuarios);
											llenarListaAdversarios(usuarioGanado);
											//Si no se tiene un juego en especifico
											//Mismo procedimiento de llenar diccionario de adversarios ganados, pero ahora
											//con cada juego
											if(!partGanadasContraAdversario.ContainsKey(id)) {
												//si no se tiene tiene el juego, solo agrega el usuario al que se le ganó, y en que juego
												SortedDictionary<String,String> ganados = new SortedDictionary<string,string>();
												ganados.Add(usuarioGanado,"1");
												partGanadasContraAdversario.Add(id,ganados);
											} else {
												//Si ya se encuentra
												SortedDictionary<String,String> ganados;
												partGanadasContraAdversario.TryGetValue(id,out ganados);
												//Si no se encuentra al usuario al que se le ganó, en el id del juego especifico, solo se
												//agrega, pero si es al revés, se aumenta las veces que se le ganó
												if(!ganados.ContainsKey(usuarioGanado)) {
													ganados.Add(usuarioGanado,"1");
												} else {
													String num_victorias;
													ganados.TryGetValue(usuarioGanado,out num_victorias);
													num_victorias=Convert.ToString(Convert.ToInt32(num_victorias)+1);
													ganados.Remove(usuarioGanado);
													ganados.Add(usuarioGanado,num_victorias);
												}
												partGanadasContraAdversario.Remove(id);
												partGanadasContraAdversario.Add(id,ganados);
											}
										}
									}
								} else {
									//En dado caso sea cooperativo, se hace solo una vez el proceso, y se usa el nombre
									//del juego como adversario
									llenarDicAdversariosGanadas(id,obJuego.nombreJuego,rutaCache,rutaCacheUsuarios);
									llenarListaAdversarios(obJuego.nombreJuego);
									if(!partGanadasContraAdversario.ContainsKey(id)) {
										SortedDictionary<String,String> ganados = new SortedDictionary<string,string>();
										ganados.Add(obJuego.nombreJuego,"1");
										partGanadasContraAdversario.Add(id,ganados);
									} else {
										SortedDictionary<String,String> ganados;
										partGanadasContraAdversario.TryGetValue(id,out ganados);
										if(!ganados.ContainsKey(obJuego.nombreJuego)) {
											ganados.Add(obJuego.nombreJuego,"1");
										} else {
											String num_victorias;
											ganados.TryGetValue(obJuego.nombreJuego,out num_victorias);
											num_victorias=Convert.ToString(Convert.ToInt32(num_victorias)+1);
											ganados.Remove(obJuego.nombreJuego);
											ganados.Add(obJuego.nombreJuego,num_victorias);
										}
										partGanadasContraAdversario.Remove(id);
										partGanadasContraAdversario.Add(id,ganados);
									}
								}


							} else {
								//Si perdió y no es cooperativo
								if(!obJuego.cooperativo) {

									for(int n = 0;n<jugadores;n++) {
										String usuarioPerdido = playJuego.DocumentElement.SelectNodes("/plays/play/players").Item(y).ChildNodes[n].Attributes["username"].Value;
										//Si no tiene nombre, se le asigna desconocido
										if(usuarioPerdido.Equals("")) {
											usuarioPerdido="Desconocido";
										}

										//Si el usuario es diferente del usuario que se dio en los parámetros
										if(!usuarioPerdido.Equals(user)) {
											//Se agrega adversarios y además que se perdió con x usuario
											llenarDicAdversariosPerdidas(id,usuarioPerdido,rutaCache,rutaCacheUsuarios);
											llenarListaAdversarios(usuarioPerdido);
											//Si no se tiene un juego en especifico
											//Mismo procedimiento de llenar diccionario de adversarios perdidos, pero ahora
											//con cada juego
											if(!partPerdidasContraAdversario.ContainsKey(id)) {
												//si no se tiene tiene el juego, solo agrega el usuario con el que se perdió, y en que juego
												SortedDictionary<String,String> perdidas = new SortedDictionary<string,string>();
												perdidas.Add(usuarioPerdido,"1");
												partPerdidasContraAdversario.Add(id,perdidas);
											} else {
												//Si ya se encuentra
												SortedDictionary<String,String> perdidas;
												partPerdidasContraAdversario.TryGetValue(id,out perdidas);
												//Si no se encuentra al usuario con el que se perdió, en el id del juego especifico, solo se
												//agrega, pero si es al revés, se aumenta las veces que se perdió con el
												if(!perdidas.ContainsKey(usuarioPerdido)) {
													perdidas.Add(usuarioPerdido,"1");
												} else {
													String num_perdidas;
													perdidas.TryGetValue(usuarioPerdido,out num_perdidas);
													num_perdidas=Convert.ToString(Convert.ToInt32(num_perdidas)+1);
													perdidas.Remove(usuarioPerdido);
													perdidas.Add(usuarioPerdido,num_perdidas);
												}
												partPerdidasContraAdversario.Remove(id);
												partPerdidasContraAdversario.Add(id,perdidas);
											}
										}
									}
								} else {
									//En dado caso sea cooperativo, se hace solo una vez el proceso, y se usa el nombre
									//del juego como adversario
									llenarDicAdversariosPerdidas(id,obJuego.nombreJuego,rutaCache,rutaCacheUsuarios);
									llenarListaAdversarios(obJuego.nombreJuego);
									if(!partPerdidasContraAdversario.ContainsKey(id)) {
										SortedDictionary<String,String> perdidas = new SortedDictionary<string,string>();
										perdidas.Add(obJuego.nombreJuego,"1");
										partPerdidasContraAdversario.Add(id,perdidas);
									} else {
										SortedDictionary<String,String> perdidas;
										partPerdidasContraAdversario.TryGetValue(id,out perdidas);
										if(!perdidas.ContainsKey(obJuego.nombreJuego)) {
											perdidas.Add(obJuego.nombreJuego,"1");
										} else {
											String num_perdidas;
											perdidas.TryGetValue(obJuego.nombreJuego,out num_perdidas);
											num_perdidas=Convert.ToString(Convert.ToInt32(num_perdidas)+1);
											perdidas.Remove(obJuego.nombreJuego);
											perdidas.Add(obJuego.nombreJuego,num_perdidas);
										}
										partPerdidasContraAdversario.Remove(id);
										partPerdidasContraAdversario.Add(id,perdidas);
									}
								}
							}
						}
					}
				} catch {

				}

			}
		}
	}


	private void llenarDicAdversariosGanadas(String id,String usuario,String rutajuegos,String rutaCacheUsuario) {
		Juego obJuego = new Juego(id,rutajuegos);
		//Si el usuario al que se le ganó no se encuentra
		if(!partAdversarioJuegosGanados.ContainsKey(usuario)) {
			SortedDictionary<String,String> ganados = new SortedDictionary<string,string>();
			//Se agrega el nombre del juego y un ganado
			ganados.Add(obJuego.nombreJuego,"1");
			partAdversarioJuegosGanados.Add(usuario,ganados);
		} else {
			//Si ya se encuentra
			SortedDictionary<String,String> ganados;
			partAdversarioJuegosGanados.TryGetValue(usuario,out ganados);
			//Si no contiene el juego (Es decir, si ya se dio de alta otro juego,o está vacío)
			if(!ganados.ContainsKey(obJuego.nombreJuego)) {
				ganados.Add(obJuego.nombreJuego,"1");
			} else {
				//Se aumentan las victorias contra ese usuario, y en que juego
				String num_victorias;
				ganados.TryGetValue(obJuego.nombreJuego,out num_victorias);
				num_victorias=Convert.ToString(Convert.ToInt32(num_victorias)+1);
				ganados.Remove(obJuego.nombreJuego);
				ganados.Add(obJuego.nombreJuego,num_victorias);
			}
			partAdversarioJuegosGanados.Remove(usuario);
			partAdversarioJuegosGanados.Add(usuario,ganados);
		}
	}


	private void llenarDicAdversariosPerdidas(String id,String usuario,String rutajuegos,String rutaCacheUsuario) {
		Juego obJuego = new Juego(id,rutajuegos);
		//Si el usuario con el que se perdió no se encuentra
		if(!partAdversarioJuegosPerdidos.ContainsKey(usuario)) {
			SortedDictionary<String,String> perdidos = new SortedDictionary<string,string>();
			//Se agrega el nombre del juego y un perdido
			perdidos.Add(obJuego.nombreJuego,"1");
			partAdversarioJuegosPerdidos.Add(usuario,perdidos);
		} else {
			//Si ya se encuentra
			SortedDictionary<String,String> perdidos;
			partAdversarioJuegosPerdidos.TryGetValue(usuario,out perdidos);
			//Si no contiene el juego (Es decir, si ya se dio de alta otro juego,o está vacío)
			if(!perdidos.ContainsKey(obJuego.nombreJuego)) {
				perdidos.Add(obJuego.nombreJuego,"1");
			} else {
				//Se aumentan las derrotas contra ese usuario, y en que juego
				String num_perdidas;
				perdidos.TryGetValue(obJuego.nombreJuego,out num_perdidas);
				num_perdidas=Convert.ToString(Convert.ToInt32(num_perdidas)+1);
				perdidos.Remove(obJuego.nombreJuego);
				perdidos.Add(obJuego.nombreJuego,num_perdidas);
			}
			partAdversarioJuegosPerdidos.Remove(usuario);
			partAdversarioJuegosPerdidos.Add(usuario,perdidos);
		}
	}


	//Para agregar cada adversario de las partidas
	private void llenarListaAdversarios(String usuario) {
		if(!adversarios.ContainsKey(usuario)) {
			adversarios.Add(usuario,usuario);
			//Console.WriteLine(usuario);
		}
	}


	//Para agregar cada juego de las partidas
	private void llenarListaJuegos(String id,String rutajuegos,String usuario,String rutaCacheUsuario) {
		Juego h = new Juego(id,rutajuegos);
		if(!juegos.ContainsKey(h.nombreJuego)) {
			juegos.Add(h.nombreJuego,id);
		}
	}

	private void llenarlistaGanados() {
		foreach(var j in partGanadasContraAdversario) {
			foreach(var nombre in j.Value) {
				if(!listaGanados.ContainsKey(nombre.Key)) {
					listaGanados.Add(nombre.Key,nombre.Value);
				} else {
					String gan;
					int gan2;
					listaGanados.TryGetValue(nombre.Key,out gan);
					gan2=Convert.ToInt32(gan);
					gan2+=Convert.ToInt32(nombre.Value);
					listaGanados.Remove(nombre.Key);
					listaGanados.Add(nombre.Key,Convert.ToString(gan2));
				}
			}
		}
		/*
		foreach(var datos in listaGanados) {
			Console.WriteLine("Nombre "+datos.Key);
			Console.WriteLine("num de victorias "+datos.Value);
		}*/
	}

	private void llenarlistaPerdidos() {
		foreach(var j in partPerdidasContraAdversario) {
			foreach(var nombre in j.Value) {
				if(!listaPerdidos.ContainsKey(nombre.Key)) {
					listaPerdidos.Add(nombre.Key,nombre.Value);
				} else {
					String per;
					int per2;
					listaPerdidos.TryGetValue(nombre.Key,out per);
					per2=Convert.ToInt32(per);
					per2+=Convert.ToInt32(nombre.Value);
					listaPerdidos.Remove(nombre.Key);
					listaPerdidos.Add(nombre.Key,Convert.ToString(per2));
				}
			}
		}
		/*
		foreach(var datos in listaPerdidos) {
			Console.WriteLine("Nombre "+datos.Key);
			Console.WriteLine("num de derrotas "+datos.Value);
		}*/
	}

	public String obtenerMasGanado() {
		String name = "";
		int ganados = 0;
		foreach(var data in listaGanados) {
			if(data.Key!="Desconocido") {
				if(Convert.ToInt32(data.Value)>ganados) {
					name=data.Key;
					ganados=Convert.ToInt32(data.Value);
				}
			}
		}
		if(name.Equals("")) {
			return "No se tienen registros de partidas";
		} else {
			return "Contra "+name+" fue el que más se ganó, con "+ganados+" Victorias";
		}
		//return "Contra " + name + " fue el que más se ganó, con " + ganados + " Victorias";
	}

	public String obtenerMasPerdido() {
		String name = "";
		int perdidos = 0;
		foreach(var data in listaPerdidos) {
			if(data.Key!="Desconocido") {
				if(Convert.ToInt32(data.Value)>perdidos) {
					name=data.Key;
					perdidos=Convert.ToInt32(data.Value);
				}
			}
		}
		if(name.Equals("")) {
			return "No se tiene registros de partidas";
		} else {
			return "Contra "+name+" fue el que más se perdió, con "+perdidos+" Derrotas";
		}
		//return "Contra "+name+" fue el que más se perdió, con "+perdidos+" Derrotas";
	}


	private void imprimir() {
		foreach(var i in partidasGanadas) {
			Console.WriteLine("Juego: "+i.Key);
			foreach(var o in i.Value) {
				Console.WriteLine("numJugadores: "+o.Key);
				Console.WriteLine("totalGanadas: "+o.Value);
			}
		}

		foreach(var i in partidasPerdidas) {
			Console.WriteLine("Juego: "+i.Key);
			foreach(var o in i.Value) {
				Console.WriteLine("numJugadores: "+o.Key);
				Console.WriteLine("totalperdidas: "+o.Value);
			}
		}

	}

	//Regresa ganadas en cada juego en específico
	public String regresarGanadas(String id) {

		String ganadas = "";
		SortedDictionary<String,String> gan;
		partidasGanadas.TryGetValue(id,out gan);
		try {
			foreach(var i in gan) {
				ganadas=ganadas+"Partidas de "+i.Key+" jugadores ganó: "+i.Value+Environment.NewLine;
			}
		} catch {
		}
		return ganadas;
	}

	//Regresa perdidas de un juego en específico
	public String regresarPerdidas(String id) {

		String perdidas = "";
		SortedDictionary<String,String> per;
		partidasPerdidas.TryGetValue(id,out per);
		try {
			foreach(var i in per) {
				perdidas=perdidas+"Partidas de "+i.Key+" jugadores perdió: "+i.Value+Environment.NewLine;
			}
		} catch {
		}
		return perdidas;
	}


}
