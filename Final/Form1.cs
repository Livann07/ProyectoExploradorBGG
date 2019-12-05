using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final {
	public partial class frmExplorador : Form {

		String directorioCache;
		String directorioCacheUsuarios;
		String directorioCacheJuego;
		String directorioCacheUsuariosGames;
		private Image imgAutor;
		private Image imgJug;
		Colecccion coleccion;
		Plays play;
		Usuario user;
		public frmExplorador() {
			InitializeComponent();
		}

		private void TreeView1_AfterSelect(object sender,TreeViewEventArgs e) {

		}

		private void Label1_Click(object sender,EventArgs e) {

		}


		private void BtnBuscar_Click(object sender,EventArgs e) {

			if(txtUsuario.Text.Equals("")) {
				MessageBox.Show("Falta ingresar un usuario");
				txtUsuario.Focus();
			} else {
				try {
					reiniciarComponentes();
					asegurarExistenciaDirectorioUser();
					try {
						coleccion=new Colecccion(txtUsuario.Text,directorioCacheJuego,directorioCacheUsuarios);
					} catch {
						Reiniciados();
					}

					user=new Usuario(txtUsuario.Text,directorioCacheJuego);


					try {
						play=new Plays(directorioCacheUsuariosGames,coleccion,txtUsuario.Text,directorioCacheJuego);
					} catch {
					}
					// play =new Plays(directorioCacheUsuariosGames,coleccion,txtUsuario.Text,directorioCacheJuego);


					tvArbol.Nodes.Clear();
					TreeNode nodoPrincipal = new TreeNode("Autores");
					nodoPrincipal.Tag="A";
					tvArbol.Nodes.Add(nodoPrincipal);
					foreach(var item in coleccion.coleccionUsuario) {
						TreeNode nodoAgregar = tvArbol.GetNodeAt(0,0);
						TreeNode agregarNodo = new TreeNode(item.Key);
						agregarNodo.Tag="0";
						agregarNodo.ImageIndex=0;
						foreach(var juego in item.Value) {
							TreeNode nodoJuego = new TreeNode(juego.nombreJuego,0,0);
							nodoJuego.Tag=juego.id;
							//ilArbol.Images.Add(juego.cargarImagen(directorioCacheJuego,juego.id));
							ilArbol.Images.Add(juego.id,juego.cargarImagen(directorioCacheJuego,juego.id));
							ilImagenes.Images.Add(juego.id,juego.cargarImagen(directorioCacheJuego,juego.id));
							nodoJuego.ImageIndex=ilArbol.Images.Count-1;
							nodoJuego.SelectedImageIndex=ilArbol.Images.Count-1;
							agregarNodo.Nodes.Add(nodoJuego);
						}
						//agregarAutorListView(item.Key);
						nodoAgregar.Nodes.Add(agregarNodo);
					}


					TreeNode nodoPrincipal2 = new TreeNode("Juegos");
					nodoPrincipal2.Tag="J";
					nodoPrincipal2.ImageIndex=1;
					nodoPrincipal2.SelectedImageIndex=1;
					tvArbol.Nodes.Add(nodoPrincipal2);

					TreeNode nodosss = new TreeNode("#juegos");
					nodosss.ImageIndex=1;
					nodosss.SelectedImageIndex=1;
					nodosss.Tag="#J";
					tvArbol.Nodes[1].Nodes.Add(nodosss);
					foreach(var item in coleccion.cantidadJugJuegos) {
						TreeNode nodoAgregar = tvArbol.GetNodeAt(0,0).NextVisibleNode;
						TreeNode agregarNodo = new TreeNode(Convert.ToString(item.Key));
						agregarNodo.Tag="00";
						agregarNodo.ImageIndex=1;
						agregarNodo.SelectedImageIndex=1;
						foreach(var juego in item.Value) {
							TreeNode nodoJuego = new TreeNode(juego.nombreJuego,0,0);
							nodoJuego.Tag=juego.id;
							//ilArbol.Images.Add(juego.cargarImagen(directorioCacheJuego,juego.id));
							//ilArbol.Images.Add(juego.id,juego.img);
							nodoJuego.ImageIndex=ilArbol.Images.IndexOfKey(juego.id);
							nodoJuego.SelectedImageIndex=ilArbol.Images.IndexOfKey(juego.id);
							agregarNodo.Nodes.Add(nodoJuego);
							//control++;
						}
						//agregarCantidadJugadoresListView(item.Key);
						tvArbol.Nodes[1].Nodes[0].Nodes.Add(agregarNodo);
					}

					añadirFamilasAlArbol();
					añadirMecanicasAlArbol();
					añadirCategoriasAlArbol();
					añadirAdversariosArbol(coleccion);
					añadirResumenNodo();
					//CalcularMasVictoriasContr(txtUsuario.Text);
				} catch {
					//MessageBox.Show("No existe usuario");
					tvArbol.Nodes.Clear();
					txtUsuario.Text="";
					rtbJuego.Text="";
					rtbAutores.Text="";
					rtbIlustradores.Text="";
					lblJugado.Text="";
					rtbJugadoW.Text="";
					rtbJugadoL.Text="";
					//rtbTitulo.Text="";
					//rtbGanado.Text="";
					//rtbPerdido.Text="";
					pbImagen.Image=null;
					dgvTabla.Rows.Clear();
					lblDes.Text="Explorador";
				}
			}
			//tvArbol.Sort();
		}

		void Reiniciados() {
			tvArbol.Nodes.Clear();
			txtUsuario.Text="";
			rtbJuego.Text="";
			rtbAutores.Text="";
			rtbIlustradores.Text="";
			lblJugado.Text="";
			rtbJugadoW.Text="";
			rtbJugadoL.Text="";
			//rtbTitulo.Text="";
			//rtbGanado.Text="";
			//rtbPerdido.Text="";
			pbImagen.Image=null;
			dgvTabla.Rows.Clear();
			lblDes.Text="Explorador";
		}

		void ReiniciarProceso() {
			rtbJuego.Text="";
			rtbAutores.Text="";
			rtbIlustradores.Text="";
			lblJugado.Text="";
			rtbJugadoW.Text="";
			rtbJugadoL.Text="";
			pbImagen.Image=null;
			dgvTabla.Rows.Clear();
		}

		//Añade rama familia
		void añadirFamilasAlArbol() {
			TreeNode nodoSub = new TreeNode("Familias");
			nodoSub.ImageIndex=3;
			nodoSub.SelectedImageIndex=3;
			nodoSub.Tag="F";
			tvArbol.Nodes[1].Nodes.Add(nodoSub);

			foreach(var item in coleccion.familias) {
				TreeNode nodoAgregar = tvArbol.GetNodeAt(0,0).NextVisibleNode;
				//TreeNode nodoAgregar = tvArbol.GetNodeAt(1).NextVisibleNode;
				TreeNode agregarNodo = new TreeNode(Convert.ToString(item.Key));
				agregarNodo.Tag="000";
				agregarNodo.ImageIndex=3;
				agregarNodo.SelectedImageIndex=3;
				foreach(var juego in item.Value) {
					TreeNode nodoJuego = new TreeNode(juego.nombreJuego,0,0);
					nodoJuego.Tag=juego.id;
					//ilArbol.Images.Add(juego.id,juego.img);
					nodoJuego.ImageIndex=ilArbol.Images.IndexOfKey(juego.id);
					nodoJuego.SelectedImageIndex=ilArbol.Images.IndexOfKey(juego.id);
					agregarNodo.Nodes.Add(nodoJuego);
				}
				tvArbol.Nodes[1].Nodes[1].Nodes.Add(agregarNodo);
			}
		}

		//Añade rama mecánica
		void añadirMecanicasAlArbol() {
			TreeNode nodoSub = new TreeNode("Mecánicas");
			nodoSub.ImageIndex=4;
			nodoSub.SelectedImageIndex=4;
			nodoSub.Tag="M";
			tvArbol.Nodes[1].Nodes.Add(nodoSub);

			foreach(var item in coleccion.mecanicas) {
				TreeNode nodoAgregar = tvArbol.GetNodeAt(0,0).NextVisibleNode;
				//TreeNode nodoAgregar = tvArbol.GetNodeAt(1).NextVisibleNode;
				TreeNode agregarNodo = new TreeNode(Convert.ToString(item.Key));
				agregarNodo.Tag="0000";
				agregarNodo.ImageIndex=4;
				agregarNodo.SelectedImageIndex=4;
				foreach(var juego in item.Value) {
					TreeNode nodoJuego = new TreeNode(juego.nombreJuego,0,0);
					nodoJuego.Tag=juego.id;
					//ilArbol.Images.Add(juego.cargarImagen(directorioCacheJuego,juego.id));
					//ilArbol.Images.Add(juego.id,juego.img);
					nodoJuego.ImageIndex=ilArbol.Images.IndexOfKey(juego.id);
					nodoJuego.SelectedImageIndex=ilArbol.Images.IndexOfKey(juego.id);
					agregarNodo.Nodes.Add(nodoJuego);
				}
				tvArbol.Nodes[1].Nodes[2].Nodes.Add(agregarNodo);
			}
		}

		//Añade rama categoría
		void añadirCategoriasAlArbol() {
			TreeNode nodoSub = new TreeNode("Categorías");
			nodoSub.ImageIndex=5;
			nodoSub.SelectedImageIndex=5;
			nodoSub.Tag="C";
			tvArbol.Nodes[1].Nodes.Add(nodoSub);

			foreach(var item in coleccion.categorías) {
				TreeNode nodoAgregar = tvArbol.GetNodeAt(0,0).NextVisibleNode;
				//TreeNode nodoAgregar = tvArbol.GetNodeAt(1).NextVisibleNode;
				TreeNode agregarNodo = new TreeNode(Convert.ToString(item.Key));
				agregarNodo.Tag="00000";
				agregarNodo.ImageIndex=5;
				agregarNodo.SelectedImageIndex=5;
				foreach(var juego in item.Value) {
					TreeNode nodoJuego = new TreeNode(juego.nombreJuego,0,0);
					nodoJuego.Tag=juego.id;
					//ilArbol.Images.Add(juego.id,juego.img);
					nodoJuego.ImageIndex=ilArbol.Images.IndexOfKey(juego.id);
					nodoJuego.SelectedImageIndex=ilArbol.Images.IndexOfKey(juego.id);
					agregarNodo.Nodes.Add(nodoJuego);
				}
				tvArbol.Nodes[1].Nodes[3].Nodes.Add(agregarNodo);
			}
		}

		//Añade adversarios al arbol
		void añadirAdversariosArbol(Colecccion cole) {
			TreeNode nodoPrincipal2 = new TreeNode("Adversarios");
			nodoPrincipal2.Tag="raizAdversarios";
			nodoPrincipal2.Name="Adversarios";
			nodoPrincipal2.ImageIndex=2;
			nodoPrincipal2.SelectedImageIndex=2;
			tvArbol.Nodes.Add(nodoPrincipal2);

			TreeNode nodosss = new TreeNode("Juego");
			nodosss.ImageIndex=1;
			nodosss.SelectedImageIndex=1;
			nodosss.Tag="Secciones Adversario";
			nodosss.Name="Juego";
			tvArbol.Nodes[2].Nodes.Add(nodosss);


			foreach(var item in play.juegos) {
				Juego jueg = new Juego(Convert.ToString(item.Value),directorioCacheJuego,directorioCacheUsuariosGames,txtUsuario.Text);
				ilArbol.Images.Add(item.Value,jueg.img);
				ilImagenes.Images.Add(item.Value,jueg.img);
				TreeNode agregarNodo = new TreeNode(jueg.nombreJuego);
				agregarNodo.Tag="JuegoAdversario";
				agregarNodo.Name=jueg.nombreJuego;
				agregarNodo.ImageIndex=ilArbol.Images.IndexOfKey(jueg.id);
				agregarNodo.SelectedImageIndex=ilArbol.Images.IndexOfKey(jueg.id);
				//agregarCantidadJugadoresListView(item.Key);
				tvArbol.Nodes[2].Nodes[0].Nodes.Add(agregarNodo);
			}
			agregarNombresAdversarios(cole);

		}

		//Añade la subrama de nombres de adversarios
		private void agregarNombresAdversarios(Colecccion cole) {
			TreeNode nodosss = new TreeNode("Nombres");
			nodosss.ImageIndex=1;
			nodosss.SelectedImageIndex=1;
			nodosss.Tag="Secciones Adversario";
			nodosss.Name="Nombres";
			tvArbol.Nodes[2].Nodes.Add(nodosss);

			foreach(var item in play.adversarios) {
				TreeNode agregarNodo = new TreeNode(Convert.ToString(item.Key));
				agregarNodo.Tag="NombreAdversario";
				agregarNodo.Name=Convert.ToString(item.Key);
				agregarNodo.ImageIndex=2;
				agregarNodo.SelectedImageIndex=2;
				//agregarCantidadJugadoresListView(item.Key);
				tvArbol.Nodes[2].Nodes[1].Nodes.Add(agregarNodo);
				//Console.WriteLine("hay " + play.adversarios.Count);
			}
		}

		void añadirResumenNodo() {
			TreeNode nodoPrincipal2 = new TreeNode("Resumen");
			nodoPrincipal2.Tag="resumen";
			nodoPrincipal2.Name="Resumen";
			nodoPrincipal2.ImageIndex=2;
			nodoPrincipal2.SelectedImageIndex=2;
			tvArbol.Nodes.Add(nodoPrincipal2);
		}

		//Se verifica la existencia del directorio caché, así como de usuarios y juegos
		void asegurarExistenciaDirectorioCache() {
			directorioCache=Application.LocalUserAppDataPath;
			directorioCacheUsuarios=directorioCache+"/usuarios/";
			directorioCacheJuego=directorioCache+"/juegos/";

			if(!Directory.Exists(directorioCacheUsuarios)) {
				Directory.CreateDirectory(directorioCacheUsuarios);
			}
			if(!Directory.Exists(directorioCacheJuego)) {
				Directory.CreateDirectory(directorioCacheJuego);
			}
		}


		//Se asegura de la existencia del directorio de cada usuario de las plays
		void asegurarExistenciaDirectorioUser() {
			directorioCacheUsuariosGames=directorioCache+"/usuarios/"+txtUsuario.Text+"/";
			if(!Directory.Exists(directorioCacheUsuariosGames)) {
				Directory.CreateDirectory(directorioCacheUsuariosGames);
			}
		}


		private void FrmExplorador_Load(object sender,EventArgs e) {
			asegurarExistenciaDirectorioCache();
			//imgAutor=Image.FromFile(directorioCacheJuego+"img/thubmnail/autor.jpg");
			//ilArbol.Images.Add(imgAutor);
			//imgJug=Image.FromFile(directorioCacheJuego+"img/thubmnail/pla.png");
			//ilArbol.Images.Add(imgJug);
			//ilArbol.Images.Add(2);
		}

		private void TvArbol_NodeMouseClick(object sender,TreeNodeMouseClickEventArgs e) {

			TreeNode nodo = e.Node;
			String tag = Convert.ToString(nodo.Tag);
			//Console.WriteLine("Es el tag: "+tag);
			try {

				if(tag.Equals("0")) {
					mostrarJuegosAutorListView(e.Node.Text);
				} else if(tag.Equals("00")) {
					mostrarJuegosCantidadListView(Convert.ToInt32(e.Node.Text));
				} else if(tag.Equals("000")) {
					mostrarFamiliaListView(e.Node.Text);
				} else if(tag.Equals("0000")) {
					mostrarMecanicaListView(e.Node.Text);
				} else if(tag.Equals("00000")) {
					mostrarCategoriasListView(e.Node.Text);
				} else if(tag.Equals("J")) {
					reiniciarComponentes();
					agregarTodoListView();

					lblDes.Text="Menús de juegos";
				} else if(tag.Equals("Adversarios")) {
					reiniciarComponentes();
					agregarTotalJuegosAd(e.Node);
				} else if(tag.Equals("raizAdversarios")) {
					reiniciarComponentes();
					nodoRaizAdversarios(e.Node);
				} else if(tag.Equals("Secciones Adversario")) {
					reiniciarComponentes();
					nodoSeccionAdversarios(e.Node);

				} else if(tag.Equals("JuegoAdversario")) {
					reiniciarComponentes();
					nodoJuegoAdversario(e.Node);

				} else if(tag.Equals("NombreAdversario")) {
					reiniciarComponentes();
					nodoNombreAdversario(e.Node);

				} else if(tag.Equals("A")) {
					reiniciarComponentes();
					foreach(var item in coleccion.coleccionUsuario) {
						agregarAutorListView(item.Key);
					}
					lblDes.Text="Total de Autores: "+coleccion.coleccionUsuario.Count;
				} else if(tag.Equals("#J")) {
					reiniciarComponentes();
					foreach(var item in coleccion.cantidadJugJuegos) {
						agregarCantidadJugadoresListView(item.Key);
					}
					lblDes.Text="Total de #Jugadores Disponible: "+coleccion.cantidadJugJuegos.Count;
				} else if(tag.Equals("F")) {
					reiniciarComponentes();
					foreach(var item in coleccion.familias) {
						agregarFamiliaListView(item.Key);
					}
					lblDes.Text="Total de Familias: "+coleccion.familias.Count();
				} else if(tag.Equals("M")) {
					reiniciarComponentes();
					foreach(var item in coleccion.mecanicas) {
						agregarMecanicasListView(item.Key);
					}
					lblDes.Text="Total de Mecánicas: "+coleccion.mecanicas.Count;
				} else if(tag.Equals("C")) {
					reiniciarComponentes();
					foreach(var item in coleccion.categorías) {
						agregarCategoriasListView(item.Key);
					}
					lblDes.Text="Total de Categorías: "+coleccion.categorías.Count;
				} else if(tag.Equals("resumen")) {
					//ReiniciarProceso();
					MessageBox.Show(coleccion.totalJuegos+Environment.NewLine+play.obtenerMasGanado()+Environment.NewLine+play.obtenerMasPerdido()+Environment.NewLine+user.getFechaRegistro()+Environment.NewLine+coleccion.getJuegoMasJugado()+Environment.NewLine+coleccion.getJuegoMenosJugado()+Environment.NewLine+coleccion.obtenerMasAutores(),"Resumen de "+txtUsuario.Text);
					//ReiniciarProceso(); 

				} else {
					//Console.WriteLine("el tag es"+e.Node.Parent.Text);
					if(nodo.Parent.Tag.Equals("00")) {
						mostrarJuegosCantidadListView(Convert.ToInt32(nodo.Parent.Text));
					} else if(tag.Equals("000")) {
						mostrarFamiliaListView(nodo.Parent.Text);
					} else if(tag.Equals("0000")) {
						mostrarMecanicaListView(nodo.Parent.Text);
					} else if(tag.Equals("00000")) {
						mostrarCategoriasListView(nodo.Parent.Text);
					} else if(tag.Equals("0")) {
						mostrarJuegosAutorListView(e.Node.Parent.Text);
					}

					//Console.WriteLine(e.Node.Parent.Text);
					mostrarDatosJuego((String)e.Node.Tag);
					lblDes.Text="Juego: "+e.Node.Text;
				}

			} catch(Exception ex) {
			}

		}

		//Se muestran los juegos que tiene cada autor
		private void mostrarJuegosAutorListView(String autor) {
			reiniciarComponentes();
			List<Juego> listaJuegos = new List<Juego>();
			coleccion.coleccionUsuario.TryGetValue(autor,out listaJuegos);
			if(listaJuegos!=null) {
				foreach(Juego juego in listaJuegos) {
					agregarJuegoListView(juego);
				}
			}

			lblDes.Text="Total de Juegos de autor: "+listaJuegos.Count();
		}


		//Se muestran los juegos de acuerdo al numero de jugadores
		private void mostrarJuegosCantidadListView(int ident) {
			reiniciarComponentes();
			List<Juego> listaJuegos = new List<Juego>();
			coleccion.cantidadJugJuegos.TryGetValue(ident,out listaJuegos);
			if(listaJuegos!=null) {
				foreach(Juego juego in listaJuegos) {
					agregarJuegoListView(juego);
				}
			}

			lblDes.Text="Numero de Juegos de "+ident+" jugadores: "+listaJuegos.Count();
		}

		//Se muestran los juegos de cada familia
		private void mostrarFamiliaListView(String ident) {
			reiniciarComponentes();
			List<Juego> listaJuegos = new List<Juego>();
			coleccion.familias.TryGetValue(ident,out listaJuegos);
			if(listaJuegos!=null) {
				foreach(Juego juego in listaJuegos) {
					agregarJuegoListView(juego);
				}
			}

			lblDes.Text="Cantidad de Juegos de la familia "+ident+": "+listaJuegos.Count();
		}

		//Se muestran los juegos de cada mecánica
		private void mostrarMecanicaListView(String ident) {
			reiniciarComponentes();
			List<Juego> listaJuegos = new List<Juego>();
			coleccion.mecanicas.TryGetValue(ident,out listaJuegos);
			if(listaJuegos!=null) {
				foreach(Juego juego in listaJuegos) {
					agregarJuegoListView(juego);
				}
			}
			lblDes.Text="Cantidad de Juegos de la mecánica "+ident+": "+listaJuegos.Count();
		}


		//Se muestran los juegos de cada categoría

		private void mostrarCategoriasListView(String ident) {
			reiniciarComponentes();
			List<Juego> listaJuegos = new List<Juego>();
			coleccion.categorías.TryGetValue(ident,out listaJuegos);
			if(listaJuegos!=null) {
				foreach(Juego juego in listaJuegos) {
					agregarJuegoListView(juego);
				}
			}
			lblDes.Text="Cantidad de Juegos de la categoría "+ident+": "+listaJuegos.Count();
		}



		//Agrega en el list view las dos ramas de adversarios
		private void nodoRaizAdversarios(TreeNode node) {
			lblDes.Text="Total de elementos encontrados: "+Convert.ToString(node.Nodes.Count);
			//int num = 7;
			foreach(var todos in node.Nodes) {
				String numero = Convert.ToString(todos);
				numero=numero.Substring(10);
				if(numero.Equals("Juego")) {
					//num=1;
				}
				ListViewItem item = new ListViewItem(numero,1);
				//Identificador de las dos ramas de adversarios
				item.SubItems.Add("11");
				item.SubItems.Add(Convert.ToString(node.Index));
				lvVista.Items.Add(item);
				//num=7;
			}
		}

		//Agrega en el listview la parte de adversarios, las dos ramas
		private void nodoSeccionAdversarios(TreeNode node) {
			lblDes.Text="Total de elementos encontrados: "+Convert.ToString(node.Nodes.Count);
			foreach(TreeNode todos in node.Nodes) {
				String numero = Convert.ToString(todos);
				numero=numero.Substring(10);
				ListViewItem item = new ListViewItem();
				if(node.Text.Equals("Juego")) {
					String id;
					play.juegos.TryGetValue(todos.Name,out id);
					item=new ListViewItem(numero,ilImagenes.Images.IndexOfKey(id));
					item.SubItems.Add("111");
					item.Tag=node.Index;
				} else if(node.Text.Equals("Nombres")) {
					item=new ListViewItem(numero,2);
					item.SubItems.Add("112");
					item.Tag=node.Index;
				}

				lvVista.Items.Add(item);
			}
		}


		//Cuando se selecciona un nodo juego de la rama de adversarios
		//Se agrega en el listview todos los nombres de los juegos, pero se muestran 
		//ganadas y perdidas del adversario específico
		private void nodoJuegoAdversario(TreeNode node) {
			String id;
			play.juegos.TryGetValue(node.Name,out id);
			Juego juego = new Juego(id,directorioCacheJuego,directorioCacheUsuariosGames,txtUsuario.Text);
			rtbJuego.Text=juego.nombreJuego;
			for(int i = 0;i<juego.autores.Count;i++) {
				rtbAutores.Text+=juego.autores[i]+Environment.NewLine;
			}

			for(int i = 0;i<juego.ilustradores.Count;i++) {
				rtbIlustradores.Text+=juego.ilustradores[i]+Environment.NewLine;
			}
			pbImagen.Image=juego.img;
			lblJugado.Text=coleccion.obtenerVecesJugado(id);
			ganadasYPerdidas(id);
			pnlInfo.Visible=true;
			for(int x = 0;x<node.Parent.Nodes.Count;x++) {
				String idd;
				play.juegos.TryGetValue(node.Parent.Nodes[x].Name,out idd);
				String nombre = Convert.ToString(node.Parent.Nodes[x].Text);
				ListViewItem item = new ListViewItem(nombre,Convert.ToInt32(ilImagenes.Images.IndexOfKey(idd)));
				item.SubItems.Add("111");
				item.Tag=node.Parent.Index;
				lvVista.Items.Add(item);
			}
			mostrarAdversarioGanadasPerdidas(id);
		}

		//Se agregan los juegos del adversario
		private void agregarTotalJuegosAd(TreeNode node) {
			for(int x = 0;x<node.Parent.Nodes.Count;x++) {
				String idd;
				play.juegos.TryGetValue(node.Parent.Nodes[x].Name,out idd);
				String nombre = Convert.ToString(node.Parent.Nodes[x].Text);
				ListViewItem item = new ListViewItem(nombre,Convert.ToInt32(ilArbol.Images.IndexOfKey(idd)));
				item.Tag=node.Parent.Index;
				lvVista.Items.Add(item);
			}
		}

		//Cuando se selecciona un nodo nombre Adversario
		//Se agrega en el listview todos los nombres de los adversarios, pero se muestran 
		//ganadas y perdidas del juego específico
		private void nodoNombreAdversario(TreeNode node) {
			//rtbTitulo.Visible=true;
			//rtbGanado.Visible=true;
			//rtbPerdido.Visible=true;
			pnlInfo.Visible=true;
			rtbJuego.Text="";
			rtbAutores.Text="";
			rtbIlustradores.Text="";
			pbImagen.Image=null;
			lblJugado.Text="";
			rtbJugadoW.Text="";
			rtbJugadoL.Text="";
			for(int x = 0;x<node.Parent.Nodes.Count;x++) {
				String idd;
				play.juegos.TryGetValue(node.Parent.Nodes[x].Name,out idd);
				String nombre = Convert.ToString(node.Parent.Nodes[x].Text);
				ListViewItem item = new ListViewItem(nombre,2);
				item.Tag=node.Parent.Index;
				item.SubItems.Add("112");
				lvVista.Items.Add(item);
			}
			mostrarJuegosGanadasPerdidas(node.Name);
		}

		private void nodoNombreAdversarioLV(TreeNode node) {
			// rtbTitulo.Visible = true;
			//rtbGanado.Visible = true;
			//rtbPerdido.Visible = true;
			pnlInfo.Visible=true;
			rtbJuego.Text="";
			rtbAutores.Text="";
			rtbIlustradores.Text="";
			pbImagen.Image=null;
			lblJugado.Text="";
			rtbJugadoW.Text="";
			rtbJugadoL.Text="";
			mostrarJuegosGanadasPerdidas(node.Name);
		}

		//Se muestra la cantidad de ganadas y perdidas de cada juego
		//con su respectiva cantida de jugadores
		public void ganadasYPerdidas(String id) {
			String ganadas = "";
			String perdidas = "";
			ganadas=play.regresarGanadas(id);
			perdidas=play.regresarPerdidas(id);
			rtbJugadoW.Text=ganadas;
			rtbJugadoL.Text=perdidas;
		}

		//Se realiza para mostrar las veces que ganó y perdió con cada adversario
		//
		public void mostrarAdversarioGanadasPerdidas(String id) {

			String adversarios = "";
			String wins = "";
			String lose = "";
			String adv = "";
			String gan = "";
			String pe = "";
			//  Console.WriteLine(play.partGanadasContraAdversario.Count);
			//Console.WriteLine(play.partPerdidasContraAdversario.Count);
			SortedDictionary<String,String> ganadas;
			SortedDictionary<String,String> perdidas;
			play.partGanadasContraAdversario.TryGetValue(id,out ganadas);
			play.partPerdidasContraAdversario.TryGetValue(id,out perdidas);
			try {
				foreach(var l in ganadas) {
					adversarios=adversarios+l.Key+Environment.NewLine;
					adv=l.Key;
					wins=wins+l.Value+Environment.NewLine;
					gan=l.Value;
					try {
						if(perdidas.ContainsKey(l.Key)) {
							String num = "";
							perdidas.TryGetValue(l.Key,out num);
							lose=lose+num+Environment.NewLine;
							pe=num;
							dgvTabla.Rows.Add(adv,gan,pe);
						} else {
							lose=lose+0+Environment.NewLine;
							pe="0";
							dgvTabla.Rows.Add(adv,gan,pe);
						}
					} catch {
						lose=lose+0+Environment.NewLine;
						pe="0";
						dgvTabla.Rows.Add(adv,gan,pe);
					}
					//Console.WriteLine(l.Key+" "+l.Value);

				}
			} catch {
			}

			try {
				foreach(var j in perdidas) {
					try {
						if(!ganadas.ContainsKey(j.Key)) {
							adversarios=adversarios+j.Key+Environment.NewLine;
							adv=j.Key;
							wins=wins+0+Environment.NewLine;
							gan="0";
							lose=lose+j.Value+Environment.NewLine;
							pe=j.Value;
							dgvTabla.Rows.Add(adv,gan,pe);
						}
					} catch {
						adversarios=adversarios+j.Key+Environment.NewLine;
						adv=j.Key;
						wins=wins+0+Environment.NewLine;
						gan="0";
						lose=lose+j.Value+Environment.NewLine;
						pe=j.Value;
						dgvTabla.Rows.Add(adv,gan,pe);
					}
					// dgvTabla.Rows.Add(adv, gan, pe);
					//Console.WriteLine("lose "+j.Key+" "+j.Value);
				}
			} catch {
			}

			//rtbTitulo.Text=adversarios;
			//rtbGanado.Text=wins;
			//rtbPerdido.Text=lose;
		}



		//Se realiza para mostrar las veces que ganó y perdió con cada juego
		//
		private void mostrarJuegosGanadasPerdidas(String id) {
			String juegos = "";
			String wins = "";
			String lose = "";
			String jue = "";
			String gan = "";
			String pe = "";
			SortedDictionary<String,String> ganadas;
			SortedDictionary<String,String> perdidas;
			play.partAdversarioJuegosGanados.TryGetValue(id,out ganadas);
			play.partAdversarioJuegosPerdidos.TryGetValue(id,out perdidas);
			try {
				foreach(var l in ganadas) {
					juegos=juegos+l.Key+Environment.NewLine;
					jue=l.Key;
					wins=wins+l.Value+Environment.NewLine;
					gan=l.Value;
					try {
						if(perdidas.ContainsKey(l.Key)) {
							String num = "";
							perdidas.TryGetValue(l.Key,out num);
							lose=lose+num+Environment.NewLine;
							pe=num;
							dgvTabla.Rows.Add(jue,gan,pe);
						} else {
							lose=lose+0+Environment.NewLine;
							pe="0";
							dgvTabla.Rows.Add(jue,gan,pe);
						}
					} catch {
						lose=lose+0+Environment.NewLine;
						pe="0";
						dgvTabla.Rows.Add(jue,gan,pe);
					}
					//Console.WriteLine(l.Key+" "+l.Value);
				}
			} catch {
			}
			try {
				foreach(var j in perdidas) {
					try {
						if(!ganadas.ContainsKey(j.Key)) {
							juegos=juegos+j.Key+Environment.NewLine;
							jue=j.Key;
							wins=wins+0+Environment.NewLine;
							gan="0";
							lose=lose+j.Value+Environment.NewLine;
							pe=j.Value;
							dgvTabla.Rows.Add(jue,gan,pe);
						}
					} catch {
						juegos=juegos+j.Key+Environment.NewLine;
						jue=j.Key;
						wins=wins+0+Environment.NewLine;
						gan="0";
						lose=lose+j.Value+Environment.NewLine;
						pe=j.Value;
						dgvTabla.Rows.Add(jue,gan,pe);
					}
					//Console.WriteLine("lose "+j.Key+" "+j.Value);
				}
			} catch {

			}

			//rtbTitulo.Text=juegos;
			//rtbGanado.Text=wins;
			//rtbPerdido.Text=lose;
		}


		private void agregarJuegoListView(Juego juego) {
			//ilIcon.Images.Add(juego.id,juego.img);
			ListViewItem item = new ListViewItem(juego.nombreJuego,ilArbol.Images.IndexOfKey(juego.id));
			//ilImagenes.Images.Add(juego.img);
			item.SubItems.Add(juego.id);
			lvVista.Items.Add(item);
		}
		private void agregarAutorListView(String autor) {
			ListViewItem item = new ListViewItem(autor,0);
			item.SubItems.Add("0");
			lvVista.Items.Add(item);
		}

		private void agregarFamiliaListView(String familia) {
			ListViewItem item = new ListViewItem(familia,3);
			item.SubItems.Add("000");
			lvVista.Items.Add(item);
		}

		private void agregarMecanicasListView(String mecanica) {
			ListViewItem item = new ListViewItem(mecanica,4);
			item.SubItems.Add("0000");
			lvVista.Items.Add(item);
		}

		private void agregarCategoriasListView(String categorias) {
			ListViewItem item = new ListViewItem(categorias,5);
			item.SubItems.Add("00000");
			lvVista.Items.Add(item);
		}

		private void agregarCantidadJugadoresListView(int cantidad) {
			ListViewItem item = new ListViewItem(Convert.ToString(cantidad),1);
			item.SubItems.Add("00");
			lvVista.Items.Add(item);
		}

		private void agregarTodoListView() {
			ListViewItem item;
			item=new ListViewItem("#Jugadores",1);
			item.SubItems.Add("#J");
			lvVista.Items.Add(item);

			item=new ListViewItem("Familia",3);
			item.SubItems.Add("F");
			lvVista.Items.Add(item);

			item=new ListViewItem("Mecánicas",4);
			item.SubItems.Add("M");
			lvVista.Items.Add(item);

			item=new ListViewItem("Categorías",5);
			item.SubItems.Add("C");
			lvVista.Items.Add(item);
		}


		private void reiniciarComponentes() {
			//pnlDetalles.Visible=false;
			lvVista.Items.Clear();
			dgvTabla.Rows.Clear();
			ReiniciarProceso();


		}

		private void LvVista_ItemSelectionChanged(object sender,ListViewItemSelectionChangedEventArgs e) {
			if(e.Item.SubItems[1].Text.Equals("0")) {
				// Console.WriteLine("El autor es:" + e.Item.Text);
				mostrarJuegosAutorListView(e.Item.Text);
			} else if(e.Item.SubItems[1].Text.Equals("00")) {
				//Console.WriteLine("La cantidad de jugadores es " + e.Item.Text);
				mostrarJuegosCantidadListView(Convert.ToInt32(e.Item.Text));
			} else if(e.Item.SubItems[1].Text.Equals("000")) {
				//Console.WriteLine("La cantidad de familias es " + e.Item.Text);
				mostrarFamiliaListView(e.Item.Text);
			} else if(e.Item.SubItems[1].Text.Equals("0000")) {
				//    Console.WriteLine("La cantidad de mecánicas es " + e.Item.Text);
				mostrarMecanicaListView(e.Item.Text);
			} else if(e.Item.SubItems[1].Text.Equals("00000")) {
				//  Console.WriteLine("La cantidad de categorías es " + e.Item.Text);
				mostrarCategoriasListView(e.Item.Text);
			} else if(e.Item.SubItems[1].Text.Equals("11")) {

				reiniciarComponentes();
				TreeNode[] tn = tvArbol.Nodes.Find(e.Item.Text,true);
				nodoSeccionAdversarios(tn[0]);
			} else if(e.Item.SubItems[1].Text.Equals("111")) {
				reiniciarComponentes();
				TreeNode[] tn = tvArbol.Nodes.Find(e.Item.Text,true);
				nodoJuegoAdversario(tn[0]);
			} else if(e.Item.SubItems[1].Text.Equals("112")) {
				reiniciarComponentes();
				//  dgvTabla.Rows.Clear();
				TreeNode[] tn = tvArbol.Nodes.Find(e.Item.Text,true);
				int num = 0;
				while(tn[num].Parent.Text!="Nombres") {
					num++;
				}
				nodoNombreAdversario(tn[num]);

			} else {

				if(e.Item.SubItems[1].Text.Equals("#J")) {
					reiniciarComponentes();
					foreach(var item in coleccion.cantidadJugJuegos) {
						agregarCantidadJugadoresListView(item.Key);
					}

				} else if(e.Item.SubItems[1].Text.Equals("F")) {

					reiniciarComponentes();
					foreach(var item in coleccion.familias) {
						agregarFamiliaListView(item.Key);
					}

				} else if(e.Item.SubItems[1].Text.Equals("M")) {
					reiniciarComponentes();
					foreach(var item in coleccion.mecanicas) {
						agregarMecanicasListView(item.Key);
					}

				} else if(e.Item.SubItems[1].Text.Equals("C")) {
					reiniciarComponentes();
					foreach(var item in coleccion.categorías) {
						agregarCategoriasListView(item.Key);
					}
				} else {
					//Console.WriteLine("Es "+e.Item.SubItems[1].Text);
					mostrarDatosJuego(e.Item.SubItems[1].Text);
				}

			}
		}


		//Se muestran los datos principales del juego
		//Se utiliza en la mayoría de los casos, cuando no se trata
		//de los adversarios
		private void mostrarDatosJuego(String id) {
			Juego juego = new Juego(id,directorioCacheJuego,directorioCacheUsuarios,txtUsuario.Text);
			rtbJugadoW.Text="";
			rtbAutores.Text="";
			rtbIlustradores.Text="";
			rtbJuego.Text="";
			rtbJuego.Text=juego.nombreJuego;
			for(int i = 0;i<juego.autores.Count;i++) {
				rtbAutores.Text+=juego.autores[i]+Environment.NewLine;
			}

			for(int i = 0;i<juego.ilustradores.Count;i++) {
				rtbIlustradores.Text+=juego.ilustradores[i]+Environment.NewLine;
			}
			rtbJugadoW.Text=play.regresarGanadas(id);
			rtbJugadoL.Text=play.regresarPerdidas(id);
			lblJugado.Text=coleccion.obtenerVecesJugado(id);
			pbImagen.Image=juego.cargarImagen(directorioCacheJuego,juego.id);
			pnlDetalles.Visible=true;
		}


		//Se actualizan los archivos de colección y de las partidas
		private void BtnActualizar_Click(object sender,EventArgs e) {

			if(txtUsuario.Text.Equals("")) {
				MessageBox.Show("Falta ingresar un usuario");
				txtUsuario.Focus();
			} else {
				Colecccion co = new Colecccion();
				co.actualizarArchivoColeccion(txtUsuario.Text,directorioCacheJuego+"colecciones/");
				Plays pl = new Plays();
				pl.actualizarArchivosPlays(directorioCacheUsuarios+"/"+txtUsuario.Text+"/",co.documentoJuego,txtUsuario.Text);
				txtUsuario.Focus();
				txtUsuario.Text="";
			}
		}

		private void actualizarColeccion() {
			coleccion.actualizarColeccion(txtUsuario.Text,directorioCacheJuego);
		}

	}
}
