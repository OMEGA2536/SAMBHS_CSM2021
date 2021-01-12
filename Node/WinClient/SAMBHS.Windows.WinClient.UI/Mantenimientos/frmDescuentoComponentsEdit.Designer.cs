namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    partial class frmDescuentoComponentsEdit
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProtocolId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Name");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.gbFilter = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtComponentName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbAddExam = new Infragistics.Win.Misc.UltraGroupBox();
            this.lblRecordCountMedicalExam = new System.Windows.Forms.Label();
            this.grdComponent = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lblRecordCount1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMontoUM = new System.Windows.Forms.Label();
            this.lblMonto = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbOperador = new System.Windows.Forms.ComboBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gbFilter)).BeginInit();
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbAddExam)).BeginInit();
            this.gbAddExam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdComponent)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnFilter);
            this.gbFilter.Controls.Add(this.txtComponentName);
            this.gbFilter.Controls.Add(this.label5);
            this.gbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.ForeColor = System.Drawing.Color.MediumBlue;
            this.gbFilter.Location = new System.Drawing.Point(12, 12);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(488, 52);
            this.gbFilter.TabIndex = 103;
            this.gbFilter.Text = "Busqueda / Filtro";
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.BackColor = System.Drawing.SystemColors.Control;
            this.btnFilter.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFilter.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFilter.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.Location = new System.Drawing.Point(406, 22);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 22);
            this.btnFilter.TabIndex = 54;
            this.btnFilter.Text = "&Buscar";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtComponentName
            // 
            this.txtComponentName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComponentName.Location = new System.Drawing.Point(71, 23);
            this.txtComponentName.MaxLength = 250;
            this.txtComponentName.Name = "txtComponentName";
            this.txtComponentName.Size = new System.Drawing.Size(328, 20);
            this.txtComponentName.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(13, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 22);
            this.label5.TabIndex = 12;
            this.label5.Text = "Examen";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbAddExam
            // 
            this.gbAddExam.Controls.Add(this.lblRecordCountMedicalExam);
            this.gbAddExam.Controls.Add(this.grdComponent);
            this.gbAddExam.Controls.Add(this.lblRecordCount1);
            this.gbAddExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAddExam.ForeColor = System.Drawing.Color.MediumBlue;
            this.gbAddExam.Location = new System.Drawing.Point(12, 70);
            this.gbAddExam.Name = "gbAddExam";
            this.gbAddExam.Size = new System.Drawing.Size(487, 254);
            this.gbAddExam.TabIndex = 104;
            this.gbAddExam.Text = "Selección de examen";
            // 
            // lblRecordCountMedicalExam
            // 
            this.lblRecordCountMedicalExam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountMedicalExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountMedicalExam.Location = new System.Drawing.Point(473, -20);
            this.lblRecordCountMedicalExam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountMedicalExam.Name = "lblRecordCountMedicalExam";
            this.lblRecordCountMedicalExam.Size = new System.Drawing.Size(10, 10);
            this.lblRecordCountMedicalExam.TabIndex = 45;
            this.lblRecordCountMedicalExam.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountMedicalExam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdComponent
            // 
            this.grdComponent.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.LightGray;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdComponent.DisplayLayout.Appearance = appearance1;
            ultraGridColumn7.Header.VisiblePosition = 0;
            ultraGridColumn5.Header.Caption = "Examen";
            ultraGridColumn5.Header.VisiblePosition = 1;
            ultraGridColumn5.Width = 395;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn7,
            ultraGridColumn5});
            this.grdComponent.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdComponent.DisplayLayout.InterBandSpacing = 10;
            this.grdComponent.DisplayLayout.MaxColScrollRegions = 1;
            this.grdComponent.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdComponent.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdComponent.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdComponent.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponent.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdComponent.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdComponent.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponent.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdComponent.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdComponent.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdComponent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdComponent.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdComponent.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdComponent.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdComponent.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdComponent.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdComponent.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdComponent.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdComponent.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdComponent.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdComponent.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdComponent.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdComponent.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdComponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdComponent.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdComponent.Location = new System.Drawing.Point(5, 20);
            this.grdComponent.Margin = new System.Windows.Forms.Padding(2);
            this.grdComponent.Name = "grdComponent";
            this.grdComponent.Size = new System.Drawing.Size(469, 229);
            this.grdComponent.TabIndex = 46;
            this.grdComponent.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.grdComponent_InitializeRow);
            // 
            // lblRecordCount1
            // 
            this.lblRecordCount1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount1.Location = new System.Drawing.Point(298, 0);
            this.lblRecordCount1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCount1.Name = "lblRecordCount1";
            this.lblRecordCount1.Size = new System.Drawing.Size(183, 18);
            this.lblRecordCount1.TabIndex = 45;
            this.lblRecordCount1.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCount1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMontoUM);
            this.groupBox1.Controls.Add(this.lblMonto);
            this.groupBox1.Controls.Add(this.txtMonto);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbOperador);
            this.groupBox1.Location = new System.Drawing.Point(547, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 129);
            this.groupBox1.TabIndex = 105;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Examen seleccionado";
            // 
            // lblMontoUM
            // 
            this.lblMontoUM.AutoSize = true;
            this.lblMontoUM.Location = new System.Drawing.Point(210, 72);
            this.lblMontoUM.Name = "lblMontoUM";
            this.lblMontoUM.Size = new System.Drawing.Size(0, 13);
            this.lblMontoUM.TabIndex = 22;
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new System.Drawing.Point(21, 66);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(0, 13);
            this.lblMonto.TabIndex = 21;
            this.lblMonto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(113, 68);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(85, 20);
            this.txtMonto.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 22);
            this.label1.TabIndex = 19;
            this.label1.Text = "Tipo de descuento";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbOperador
            // 
            this.cbOperador.AutoCompleteCustomSource.AddRange(new string[] {
            "POR PORCENTAJE",
            "POR PRECIO"});
            this.cbOperador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperador.FormattingEnabled = true;
            this.cbOperador.Items.AddRange(new object[] {
            "POR PORCENTAJE",
            "POR PRECIO"});
            this.cbOperador.Location = new System.Drawing.Point(113, 29);
            this.cbOperador.Name = "cbOperador";
            this.cbOperador.Size = new System.Drawing.Size(159, 21);
            this.cbOperador.TabIndex = 18;
            this.cbOperador.TextChanged += new System.EventHandler(this.cbOperador_TextChanged);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = global::SAMBHS.Windows.WinClient.UI.Resource.add;
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(591, 159);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(148, 40);
            this.btnAgregar.TabIndex = 106;
            this.btnAgregar.Text = "Agregar al descuento";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // frmDescuentoComponentsEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 358);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbAddExam);
            this.Controls.Add(this.gbFilter);
            this.Name = "frmDescuentoComponentsEdit";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Componentes de un descuento";
            this.Load += new System.EventHandler(this.frmDescuentoComponentsEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbFilter)).EndInit();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbAddExam)).EndInit();
            this.gbAddExam.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdComponent)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox gbFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtComponentName;
        private System.Windows.Forms.Label label5;
        private Infragistics.Win.Misc.UltraGroupBox gbAddExam;
        private System.Windows.Forms.Label lblRecordCountMedicalExam;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdComponent;
        private System.Windows.Forms.Label lblRecordCount1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbOperador;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMontoUM;
        private System.Windows.Forms.Button btnAgregar;
    }
}