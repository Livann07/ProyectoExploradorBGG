namespace Final {
	partial class frmExplorador {
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing) {
			if( disposing&&( components!=null ) ) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Autores");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Juegos");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Adversarios");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExplorador));
			this.pnlBusqueda = new System.Windows.Forms.Panel();
			this.btnActualizar = new System.Windows.Forms.Button();
			this.btnBuscar = new System.Windows.Forms.Button();
			this.txtUsuario = new System.Windows.Forms.TextBox();
			this.lblCompl = new System.Windows.Forms.Label();
			this.pnlInfo = new System.Windows.Forms.Panel();
			this.lblDes = new System.Windows.Forms.Label();
			this.tvArbol = new System.Windows.Forms.TreeView();
			this.ilArbol = new System.Windows.Forms.ImageList(this.components);
			this.ilIcon = new System.Windows.Forms.ImageList(this.components);
			this.lvVista = new System.Windows.Forms.ListView();
			this.ilImagenes = new System.Windows.Forms.ImageList(this.components);
			this.pnlDetalles = new System.Windows.Forms.Panel();
			this.dgvTabla = new System.Windows.Forms.DataGridView();
			this.AdvJuego = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Ganados = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Perdidos = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label2 = new System.Windows.Forms.Label();
			this.rtbJuego = new System.Windows.Forms.RichTextBox();
			this.rtbJugadoL = new System.Windows.Forms.RichTextBox();
			this.rtbJugadoW = new System.Windows.Forms.RichTextBox();
			this.rtbIlustradores = new System.Windows.Forms.RichTextBox();
			this.rtbAutores = new System.Windows.Forms.RichTextBox();
			this.lblJugado = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pbImagen = new System.Windows.Forms.PictureBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.pnlBusqueda.SuspendLayout();
			this.pnlInfo.SuspendLayout();
			this.pnlDetalles.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvTabla)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlBusqueda
			// 
			this.pnlBusqueda.BackColor = System.Drawing.Color.MediumAquamarine;
			this.pnlBusqueda.Controls.Add(this.btnActualizar);
			this.pnlBusqueda.Controls.Add(this.btnBuscar);
			this.pnlBusqueda.Controls.Add(this.txtUsuario);
			this.pnlBusqueda.Controls.Add(this.lblCompl);
			this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlBusqueda.Location = new System.Drawing.Point(0, 0);
			this.pnlBusqueda.Name = "pnlBusqueda";
			this.pnlBusqueda.Size = new System.Drawing.Size(1030, 51);
			this.pnlBusqueda.TabIndex = 0;
			// 
			// btnActualizar
			// 
			this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnActualizar.Location = new System.Drawing.Point(436, 12);
			this.btnActualizar.Name = "btnActualizar";
			this.btnActualizar.Size = new System.Drawing.Size(114, 37);
			this.btnActualizar.TabIndex = 3;
			this.btnActualizar.Text = "Actualizar";
			this.btnActualizar.UseVisualStyleBackColor = true;
			this.btnActualizar.Click += new System.EventHandler(this.BtnActualizar_Click);
			// 
			// btnBuscar
			// 
			this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBuscar.Location = new System.Drawing.Point(299, 12);
			this.btnBuscar.Name = "btnBuscar";
			this.btnBuscar.Size = new System.Drawing.Size(114, 37);
			this.btnBuscar.TabIndex = 2;
			this.btnBuscar.Text = "Buscar";
			this.btnBuscar.UseVisualStyleBackColor = true;
			this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
			// 
			// txtUsuario
			// 
			this.txtUsuario.Location = new System.Drawing.Point(167, 19);
			this.txtUsuario.Name = "txtUsuario";
			this.txtUsuario.Size = new System.Drawing.Size(111, 20);
			this.txtUsuario.TabIndex = 1;
			// 
			// lblCompl
			// 
			this.lblCompl.AutoSize = true;
			this.lblCompl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCompl.Location = new System.Drawing.Point(55, 19);
			this.lblCompl.Name = "lblCompl";
			this.lblCompl.Size = new System.Drawing.Size(71, 20);
			this.lblCompl.TabIndex = 0;
			this.lblCompl.Text = "Usuario";
			this.lblCompl.Click += new System.EventHandler(this.Label1_Click);
			// 
			// pnlInfo
			// 
			this.pnlInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.pnlInfo.Controls.Add(this.lblDes);
			this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlInfo.Location = new System.Drawing.Point(0, 589);
			this.pnlInfo.Name = "pnlInfo";
			this.pnlInfo.Size = new System.Drawing.Size(1030, 45);
			this.pnlInfo.TabIndex = 2;
			// 
			// lblDes
			// 
			this.lblDes.AutoSize = true;
			this.lblDes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDes.Location = new System.Drawing.Point(70, 13);
			this.lblDes.Name = "lblDes";
			this.lblDes.Size = new System.Drawing.Size(142, 15);
			this.lblDes.TabIndex = 0;
			this.lblDes.Text = "Datos del Explorador";
			// 
			// tvArbol
			// 
			this.tvArbol.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvArbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tvArbol.ImageIndex = 0;
			this.tvArbol.ImageList = this.ilArbol;
			this.tvArbol.Location = new System.Drawing.Point(0, 51);
			this.tvArbol.Name = "tvArbol";
			treeNode1.ImageIndex = 0;
			treeNode1.Name = "Nodo0";
			treeNode1.Tag = "A";
			treeNode1.Text = "Autores";
			treeNode2.ImageIndex = 1;
			treeNode2.Name = "Nodo1";
			treeNode2.SelectedImageKey = "pla.png";
			treeNode2.Tag = "J";
			treeNode2.Text = "Juegos";
			treeNode3.ImageIndex = 2;
			treeNode3.Name = "Nodo0";
			treeNode3.SelectedImageKey = "adv.jpg";
			treeNode3.Tag = "Ad";
			treeNode3.Text = "Adversarios";
			this.tvArbol.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
			this.tvArbol.SelectedImageIndex = 0;
			this.tvArbol.Size = new System.Drawing.Size(236, 538);
			this.tvArbol.TabIndex = 3;
			this.tvArbol.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvArbol_NodeMouseClick);
			// 
			// ilArbol
			// 
			this.ilArbol.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilArbol.ImageStream")));
			this.ilArbol.TransparentColor = System.Drawing.Color.Transparent;
			this.ilArbol.Images.SetKeyName(0, "autor.jpg");
			this.ilArbol.Images.SetKeyName(1, "pla.png");
			this.ilArbol.Images.SetKeyName(2, "adv.jpg");
			this.ilArbol.Images.SetKeyName(3, "familia.png");
			this.ilArbol.Images.SetKeyName(4, "mecanica.jpg");
			this.ilArbol.Images.SetKeyName(5, "categoria.jpg");
			// 
			// ilIcon
			// 
			this.ilIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcon.ImageStream")));
			this.ilIcon.TransparentColor = System.Drawing.Color.Transparent;
			this.ilIcon.Images.SetKeyName(0, "autor.jpg");
			this.ilIcon.Images.SetKeyName(1, "pla.png");
			this.ilIcon.Images.SetKeyName(2, "adv.jpg");
			this.ilIcon.Images.SetKeyName(3, "familia.png");
			this.ilIcon.Images.SetKeyName(4, "mecanica.jpg");
			this.ilIcon.Images.SetKeyName(5, "categoria.jpg");
			// 
			// lvVista
			// 
			this.lvVista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvVista.HideSelection = false;
			this.lvVista.LargeImageList = this.ilImagenes;
			this.lvVista.Location = new System.Drawing.Point(236, 51);
			this.lvVista.Name = "lvVista";
			this.lvVista.Size = new System.Drawing.Size(497, 538);
			this.lvVista.SmallImageList = this.ilArbol;
			this.lvVista.TabIndex = 4;
			this.lvVista.UseCompatibleStateImageBehavior = false;
			this.lvVista.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LvVista_ItemSelectionChanged);
			// 
			// ilImagenes
			// 
			this.ilImagenes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilImagenes.ImageStream")));
			this.ilImagenes.TransparentColor = System.Drawing.Color.Transparent;
			this.ilImagenes.Images.SetKeyName(0, "autor.jpg");
			this.ilImagenes.Images.SetKeyName(1, "pla.png");
			this.ilImagenes.Images.SetKeyName(2, "adv.jpg");
			this.ilImagenes.Images.SetKeyName(3, "familia.png");
			this.ilImagenes.Images.SetKeyName(4, "mecanica.jpg");
			this.ilImagenes.Images.SetKeyName(5, "categoria.jpg");
			// 
			// pnlDetalles
			// 
			this.pnlDetalles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.pnlDetalles.Controls.Add(this.dgvTabla);
			this.pnlDetalles.Controls.Add(this.label2);
			this.pnlDetalles.Controls.Add(this.rtbJuego);
			this.pnlDetalles.Controls.Add(this.rtbJugadoL);
			this.pnlDetalles.Controls.Add(this.rtbJugadoW);
			this.pnlDetalles.Controls.Add(this.rtbIlustradores);
			this.pnlDetalles.Controls.Add(this.rtbAutores);
			this.pnlDetalles.Controls.Add(this.lblJugado);
			this.pnlDetalles.Controls.Add(this.label1);
			this.pnlDetalles.Controls.Add(this.pbImagen);
			this.pnlDetalles.Controls.Add(this.label4);
			this.pnlDetalles.Controls.Add(this.label3);
			this.pnlDetalles.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlDetalles.Location = new System.Drawing.Point(731, 51);
			this.pnlDetalles.Name = "pnlDetalles";
			this.pnlDetalles.Size = new System.Drawing.Size(299, 538);
			this.pnlDetalles.TabIndex = 5;
			// 
			// dgvTabla
			// 
			this.dgvTabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AdvJuego,
            this.Ganados,
            this.Perdidos});
			this.dgvTabla.Location = new System.Drawing.Point(22, 275);
			this.dgvTabla.Name = "dgvTabla";
			this.dgvTabla.ReadOnly = true;
			this.dgvTabla.Size = new System.Drawing.Size(235, 110);
			this.dgvTabla.TabIndex = 22;
			// 
			// AdvJuego
			// 
			this.AdvJuego.HeaderText = "Adversario o Juego";
			this.AdvJuego.Name = "AdvJuego";
			this.AdvJuego.ReadOnly = true;
			this.AdvJuego.Width = 90;
			// 
			// Ganados
			// 
			this.Ganados.HeaderText = "Ganados";
			this.Ganados.Name = "Ganados";
			this.Ganados.ReadOnly = true;
			this.Ganados.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Ganados.Width = 60;
			// 
			// Perdidos
			// 
			this.Perdidos.HeaderText = "Perdidos";
			this.Perdidos.Name = "Perdidos";
			this.Perdidos.ReadOnly = true;
			this.Perdidos.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Perdidos.Width = 60;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(18, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 15);
			this.label2.TabIndex = 15;
			this.label2.Text = "Juego:";
			// 
			// rtbJuego
			// 
			this.rtbJuego.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbJuego.Location = new System.Drawing.Point(20, 35);
			this.rtbJuego.Name = "rtbJuego";
			this.rtbJuego.ReadOnly = true;
			this.rtbJuego.Size = new System.Drawing.Size(236, 30);
			this.rtbJuego.TabIndex = 14;
			this.rtbJuego.Text = "";
			// 
			// rtbJugadoL
			// 
			this.rtbJugadoL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbJugadoL.Location = new System.Drawing.Point(21, 233);
			this.rtbJugadoL.Name = "rtbJugadoL";
			this.rtbJugadoL.ReadOnly = true;
			this.rtbJugadoL.Size = new System.Drawing.Size(236, 36);
			this.rtbJugadoL.TabIndex = 13;
			this.rtbJugadoL.Text = "";
			// 
			// rtbJugadoW
			// 
			this.rtbJugadoW.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbJugadoW.Location = new System.Drawing.Point(20, 192);
			this.rtbJugadoW.Name = "rtbJugadoW";
			this.rtbJugadoW.ReadOnly = true;
			this.rtbJugadoW.Size = new System.Drawing.Size(237, 35);
			this.rtbJugadoW.TabIndex = 12;
			this.rtbJugadoW.Text = "";
			// 
			// rtbIlustradores
			// 
			this.rtbIlustradores.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbIlustradores.Location = new System.Drawing.Point(20, 137);
			this.rtbIlustradores.Name = "rtbIlustradores";
			this.rtbIlustradores.ReadOnly = true;
			this.rtbIlustradores.Size = new System.Drawing.Size(237, 34);
			this.rtbIlustradores.TabIndex = 11;
			this.rtbIlustradores.Text = "";
			// 
			// rtbAutores
			// 
			this.rtbAutores.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbAutores.Location = new System.Drawing.Point(21, 86);
			this.rtbAutores.Name = "rtbAutores";
			this.rtbAutores.ReadOnly = true;
			this.rtbAutores.Size = new System.Drawing.Size(237, 30);
			this.rtbAutores.TabIndex = 10;
			this.rtbAutores.Text = "";
			// 
			// lblJugado
			// 
			this.lblJugado.AutoSize = true;
			this.lblJugado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblJugado.Location = new System.Drawing.Point(123, 174);
			this.lblJugado.Name = "lblJugado";
			this.lblJugado.Size = new System.Drawing.Size(12, 15);
			this.lblJugado.TabIndex = 8;
			this.lblJugado.Text = "-";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(17, 174);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 15);
			this.label1.TabIndex = 7;
			this.label1.Text = "Veces Jugado:";
			// 
			// pbImagen
			// 
			this.pbImagen.Location = new System.Drawing.Point(22, 391);
			this.pbImagen.Name = "pbImagen";
			this.pbImagen.Size = new System.Drawing.Size(236, 117);
			this.pbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbImagen.TabIndex = 3;
			this.pbImagen.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(17, 119);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 15);
			this.label4.TabIndex = 2;
			this.label4.Text = "Ilustrador:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(18, 68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 15);
			this.label3.TabIndex = 1;
			this.label3.Text = "Autor:";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// frmExplorador
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1030, 634);
			this.Controls.Add(this.pnlDetalles);
			this.Controls.Add(this.lvVista);
			this.Controls.Add(this.tvArbol);
			this.Controls.Add(this.pnlInfo);
			this.Controls.Add(this.pnlBusqueda);
			this.Name = "frmExplorador";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Explorador";
			this.Load += new System.EventHandler(this.FrmExplorador_Load);
			this.pnlBusqueda.ResumeLayout(false);
			this.pnlBusqueda.PerformLayout();
			this.pnlInfo.ResumeLayout(false);
			this.pnlInfo.PerformLayout();
			this.pnlDetalles.ResumeLayout(false);
			this.pnlDetalles.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvTabla)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlBusqueda;
		private System.Windows.Forms.Label lblCompl;
		private System.Windows.Forms.Panel pnlInfo;
		private System.Windows.Forms.TreeView tvArbol;
		private System.Windows.Forms.ListView lvVista;
		private System.Windows.Forms.Button btnBuscar;
		private System.Windows.Forms.TextBox txtUsuario;
		private System.Windows.Forms.Button btnActualizar;
		private System.Windows.Forms.Panel pnlDetalles;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pbImagen;
		private System.Windows.Forms.ImageList ilArbol;
		private System.Windows.Forms.ImageList ilImagenes;
		private System.Windows.Forms.ImageList ilIcon;
		private System.Windows.Forms.Label lblDes;
		private System.Windows.Forms.Label lblJugado;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox rtbAutores;
		private System.Windows.Forms.RichTextBox rtbIlustradores;
		private System.Windows.Forms.RichTextBox rtbJugadoW;
		private System.Windows.Forms.RichTextBox rtbJugadoL;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RichTextBox rtbJuego;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataGridView dgvTabla;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdvJuego;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ganados;
        private System.Windows.Forms.DataGridViewTextBoxColumn Perdidos;
    }
}

