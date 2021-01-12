namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    partial class frmViewProductForPackage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_IdProducto");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Descripcion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Almacen");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_PrecioVenta");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_StockActual");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.grdProduct = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdProduct)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre del producto";
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(125, 12);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(281, 20);
            this.txtProductName.TabIndex = 1;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Image = global::SAMBHS.Windows.SigesoftIntegration.UI.Properties.Resources.system_search;
            this.btnFiltrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrar.Location = new System.Drawing.Point(413, 12);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrar.TabIndex = 2;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // grdProduct
            // 
            this.grdProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProduct.CausesValidation = false;
            this.grdProduct.DataMember = null;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdProduct.DisplayLayout.Appearance = appearance1;
            ultraGridColumn2.Header.VisiblePosition = 0;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn4.Header.Caption = "Descripción";
            ultraGridColumn4.Header.VisiblePosition = 1;
            ultraGridColumn4.Width = 259;
            ultraGridColumn1.Header.Caption = "Almacén";
            ultraGridColumn1.Header.VisiblePosition = 2;
            ultraGridColumn1.Width = 150;
            ultraGridColumn5.Header.Caption = "Precio Venta";
            ultraGridColumn5.Header.VisiblePosition = 3;
            ultraGridColumn7.Header.Caption = "Stock Actual";
            ultraGridColumn7.Header.VisiblePosition = 4;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn2,
            ultraGridColumn4,
            ultraGridColumn1,
            ultraGridColumn5,
            ultraGridColumn7});
            this.grdProduct.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdProduct.DisplayLayout.InterBandSpacing = 10;
            this.grdProduct.DisplayLayout.MaxColScrollRegions = 1;
            this.grdProduct.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdProduct.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdProduct.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdProduct.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdProduct.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdProduct.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdProduct.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdProduct.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdProduct.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdProduct.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdProduct.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.Color.DarkGray;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdProduct.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdProduct.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdProduct.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdProduct.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdProduct.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdProduct.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdProduct.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdProduct.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdProduct.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdProduct.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdProduct.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdProduct.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProduct.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdProduct.Location = new System.Drawing.Point(15, 49);
            this.grdProduct.Margin = new System.Windows.Forms.Padding(2);
            this.grdProduct.Name = "grdProduct";
            this.grdProduct.Size = new System.Drawing.Size(623, 320);
            this.grdProduct.TabIndex = 57;
            this.grdProduct.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdProduct_AfterSelectChange);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Controls.Add(this.btnSalir);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPrecio);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCantidad);
            this.groupBox1.Location = new System.Drawing.Point(643, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 320);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalles";
            // 
            // btnGuardar
            // 
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Image = global::SAMBHS.Windows.SigesoftIntegration.UI.Properties.Resources.system_save;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(132, 291);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 7;
            this.btnGuardar.Text = "Agregar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Image = global::SAMBHS.Windows.SigesoftIntegration.UI.Properties.Resources.system_close1;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(9, 291);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Precio V.";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Enabled = false;
            this.txtPrecio.Location = new System.Drawing.Point(61, 63);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(100, 20);
            this.txtPrecio.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Enabled = false;
            this.txtNombre.Location = new System.Drawing.Point(61, 37);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(146, 20);
            this.txtNombre.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cantidad";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(61, 89);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(100, 20);
            this.txtCantidad.TabIndex = 0;
            // 
            // frmViewProductForPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 380);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grdProduct);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.label1);
            this.Name = "frmViewProductForPackage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bandeja de Productos";
            this.Load += new System.EventHandler(this.frmViewProductForPackage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdProduct)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Button btnFiltrar;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdProduct;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnSalir;
    }
}