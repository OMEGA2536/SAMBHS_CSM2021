namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    partial class frmCotizacion
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Pacient");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DocNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProtocolName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CreationUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_InsertDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_UpdateUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_UpdateDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("b_Seleccionar");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_Edad");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Nombres");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ApePaterno");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ApeMaterno");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CotizacionId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_CostoTotal");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_aCuenta");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Saldo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_PersonId");
            this.gbBusqueda = new System.Windows.Forms.GroupBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtPacient = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNroDocument = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dptDateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDateTimeStar = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.grdDataCalendar = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnPerson = new System.Windows.Forms.Button();
            this.btnFacturar = new System.Windows.Forms.Button();
            this.btnEditarTrabajador = new System.Windows.Forms.Button();
            this.btnCambiarProtocolo = new System.Windows.Forms.Button();
            this.btnschedule = new System.Windows.Forms.Button();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.btnPrecios = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gbBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // gbBusqueda
            // 
            this.gbBusqueda.Controls.Add(this.btnFilter);
            this.gbBusqueda.Controls.Add(this.txtPacient);
            this.gbBusqueda.Controls.Add(this.label5);
            this.gbBusqueda.Controls.Add(this.txtNroDocument);
            this.gbBusqueda.Controls.Add(this.label6);
            this.gbBusqueda.Controls.Add(this.dptDateTimeEnd);
            this.gbBusqueda.Controls.Add(this.label2);
            this.gbBusqueda.Controls.Add(this.dtpDateTimeStar);
            this.gbBusqueda.Controls.Add(this.label1);
            this.gbBusqueda.Location = new System.Drawing.Point(13, 13);
            this.gbBusqueda.Name = "gbBusqueda";
            this.gbBusqueda.Size = new System.Drawing.Size(934, 53);
            this.gbBusqueda.TabIndex = 4;
            this.gbBusqueda.TabStop = false;
            this.gbBusqueda.Text = "Búsqueda";
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.SystemColors.Control;
            this.btnFilter.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFilter.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFilter.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.Location = new System.Drawing.Point(856, 17);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(73, 24);
            this.btnFilter.TabIndex = 15;
            this.btnFilter.Text = "Filtrar";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtPacient
            // 
            this.txtPacient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPacient.Location = new System.Drawing.Point(634, 18);
            this.txtPacient.Margin = new System.Windows.Forms.Padding(2);
            this.txtPacient.MaxLength = 200;
            this.txtPacient.Name = "txtPacient";
            this.txtPacient.Size = new System.Drawing.Size(193, 20);
            this.txtPacient.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(570, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Paciente";
            // 
            // txtNroDocument
            // 
            this.txtNroDocument.Location = new System.Drawing.Point(453, 18);
            this.txtNroDocument.Margin = new System.Windows.Forms.Padding(2);
            this.txtNroDocument.MaxLength = 20;
            this.txtNroDocument.Name = "txtNroDocument";
            this.txtNroDocument.Size = new System.Drawing.Size(102, 20);
            this.txtNroDocument.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(388, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Nro Doc.";
            // 
            // dptDateTimeEnd
            // 
            this.dptDateTimeEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dptDateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptDateTimeEnd.Location = new System.Drawing.Point(271, 18);
            this.dptDateTimeEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dptDateTimeEnd.Name = "dptDateTimeEnd";
            this.dptDateTimeEnd.Size = new System.Drawing.Size(102, 20);
            this.dptDateTimeEnd.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(202, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fecha Fin";
            // 
            // dtpDateTimeStar
            // 
            this.dtpDateTimeStar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateTimeStar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTimeStar.Location = new System.Drawing.Point(85, 18);
            this.dtpDateTimeStar.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDateTimeStar.Name = "dtpDateTimeStar";
            this.dtpDateTimeStar.Size = new System.Drawing.Size(102, 20);
            this.dtpDateTimeStar.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fecha Inicio";
            // 
            // grdDataCalendar
            // 
            this.grdDataCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataCalendar.CausesValidation = false;
            ultraGridColumn14.Header.Caption = "Paciente";
            ultraGridColumn14.Header.VisiblePosition = 7;
            ultraGridColumn14.Width = 258;
            ultraGridColumn15.Header.Caption = "DNI";
            ultraGridColumn15.Header.VisiblePosition = 8;
            ultraGridColumn30.Header.Caption = "Protocolo";
            ultraGridColumn30.Header.VisiblePosition = 10;
            ultraGridColumn30.Width = 233;
            ultraGridColumn34.Header.Caption = "Usuario Capta";
            ultraGridColumn34.Header.VisiblePosition = 4;
            ultraGridColumn34.Width = 125;
            ultraGridColumn1.Header.Caption = "Fecha Crea";
            ultraGridColumn1.Header.VisiblePosition = 5;
            ultraGridColumn36.Header.Caption = "Usuario Act.";
            ultraGridColumn36.Header.VisiblePosition = 14;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn36.Width = 125;
            ultraGridColumn54.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn54.Header.Caption = "Fecha Act.";
            ultraGridColumn54.Header.VisiblePosition = 15;
            ultraGridColumn54.Hidden = true;
            ultraGridColumn54.Width = 150;
            ultraGridColumn58.Header.Caption = "Seleccione";
            ultraGridColumn58.Header.CheckBoxVisibility = Infragistics.Win.UltraWinGrid.HeaderCheckBoxVisibility.Always;
            ultraGridColumn58.Header.VisiblePosition = 0;
            ultraGridColumn58.Width = 37;
            ultraGridColumn59.Header.Caption = "Edad";
            ultraGridColumn59.Header.VisiblePosition = 9;
            ultraGridColumn62.Header.VisiblePosition = 6;
            ultraGridColumn62.Hidden = true;
            ultraGridColumn63.Header.VisiblePosition = 2;
            ultraGridColumn63.Hidden = true;
            ultraGridColumn64.Header.VisiblePosition = 3;
            ultraGridColumn64.Hidden = true;
            ultraGridColumn2.Header.Caption = "Cotizacion Id";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 11;
            ultraGridColumn4.Header.VisiblePosition = 12;
            ultraGridColumn5.Header.VisiblePosition = 13;
            ultraGridColumn6.Header.VisiblePosition = 16;
            ultraGridColumn6.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn30,
            ultraGridColumn34,
            ultraGridColumn1,
            ultraGridColumn36,
            ultraGridColumn54,
            ultraGridColumn58,
            ultraGridColumn59,
            ultraGridColumn62,
            ultraGridColumn63,
            ultraGridColumn64,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6});
            this.grdDataCalendar.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDataCalendar.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataCalendar.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataCalendar.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataCalendar.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataCalendar.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataCalendar.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataCalendar.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataCalendar.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataCalendar.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdDataCalendar.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdDataCalendar.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataCalendar.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataCalendar.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataCalendar.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDataCalendar.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataCalendar.Location = new System.Drawing.Point(13, 82);
            this.grdDataCalendar.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataCalendar.Name = "grdDataCalendar";
            this.grdDataCalendar.Size = new System.Drawing.Size(934, 334);
            this.grdDataCalendar.TabIndex = 55;
            this.grdDataCalendar.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.grdDataCalendar_ClickCell);
            // 
            // btnPerson
            // 
            this.btnPerson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPerson.BackColor = System.Drawing.SystemColors.Control;
            this.btnPerson.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnPerson.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPerson.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPerson.ForeColor = System.Drawing.Color.Black;
            this.btnPerson.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPerson.Location = new System.Drawing.Point(13, 420);
            this.btnPerson.Margin = new System.Windows.Forms.Padding(2);
            this.btnPerson.Name = "btnPerson";
            this.btnPerson.Size = new System.Drawing.Size(118, 25);
            this.btnPerson.TabIndex = 56;
            this.btnPerson.Text = "Nuevo";
            this.btnPerson.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPerson.UseVisualStyleBackColor = false;
            this.btnPerson.Click += new System.EventHandler(this.btnPerson_Click);
            // 
            // btnFacturar
            // 
            this.btnFacturar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFacturar.BackColor = System.Drawing.SystemColors.Control;
            this.btnFacturar.Enabled = false;
            this.btnFacturar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFacturar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFacturar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFacturar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFacturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFacturar.ForeColor = System.Drawing.Color.Black;
            this.btnFacturar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFacturar.Location = new System.Drawing.Point(135, 420);
            this.btnFacturar.Margin = new System.Windows.Forms.Padding(2);
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(118, 25);
            this.btnFacturar.TabIndex = 57;
            this.btnFacturar.Text = "Facturar";
            this.btnFacturar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFacturar.UseVisualStyleBackColor = false;
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // btnEditarTrabajador
            // 
            this.btnEditarTrabajador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditarTrabajador.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditarTrabajador.Enabled = false;
            this.btnEditarTrabajador.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnEditarTrabajador.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEditarTrabajador.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnEditarTrabajador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarTrabajador.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarTrabajador.ForeColor = System.Drawing.Color.Black;
            this.btnEditarTrabajador.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditarTrabajador.Location = new System.Drawing.Point(257, 421);
            this.btnEditarTrabajador.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditarTrabajador.Name = "btnEditarTrabajador";
            this.btnEditarTrabajador.Size = new System.Drawing.Size(118, 25);
            this.btnEditarTrabajador.TabIndex = 58;
            this.btnEditarTrabajador.Text = "Editar Paciente";
            this.btnEditarTrabajador.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditarTrabajador.UseVisualStyleBackColor = false;
            this.btnEditarTrabajador.Click += new System.EventHandler(this.btnEditarTrabajador_Click);
            // 
            // btnCambiarProtocolo
            // 
            this.btnCambiarProtocolo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCambiarProtocolo.BackColor = System.Drawing.SystemColors.Control;
            this.btnCambiarProtocolo.Enabled = false;
            this.btnCambiarProtocolo.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCambiarProtocolo.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCambiarProtocolo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnCambiarProtocolo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarProtocolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiarProtocolo.ForeColor = System.Drawing.Color.Black;
            this.btnCambiarProtocolo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCambiarProtocolo.Location = new System.Drawing.Point(379, 421);
            this.btnCambiarProtocolo.Margin = new System.Windows.Forms.Padding(2);
            this.btnCambiarProtocolo.Name = "btnCambiarProtocolo";
            this.btnCambiarProtocolo.Size = new System.Drawing.Size(118, 25);
            this.btnCambiarProtocolo.TabIndex = 104;
            this.btnCambiarProtocolo.Text = "Cambiar Cotización";
            this.btnCambiarProtocolo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCambiarProtocolo.UseVisualStyleBackColor = false;
            this.btnCambiarProtocolo.Click += new System.EventHandler(this.btnCambiarProtocolo_Click);
            // 
            // btnschedule
            // 
            this.btnschedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnschedule.BackColor = System.Drawing.SystemColors.Control;
            this.btnschedule.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnschedule.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnschedule.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnschedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnschedule.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnschedule.Location = new System.Drawing.Point(501, 421);
            this.btnschedule.Margin = new System.Windows.Forms.Padding(2);
            this.btnschedule.Name = "btnschedule";
            this.btnschedule.Size = new System.Drawing.Size(118, 25);
            this.btnschedule.TabIndex = 152;
            this.btnschedule.Text = " Agendar";
            this.btnschedule.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnschedule.UseVisualStyleBackColor = false;
            this.btnschedule.Click += new System.EventHandler(this.btnschedule_Click);
            // 
            // btnPrecios
            // 
            this.btnPrecios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrecios.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrecios.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnPrecios.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPrecios.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnPrecios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrecios.Image = global::SAMBHS.Windows.WinClient.UI.Resource.money;
            this.btnPrecios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrecios.Location = new System.Drawing.Point(623, 421);
            this.btnPrecios.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrecios.Name = "btnPrecios";
            this.btnPrecios.Size = new System.Drawing.Size(83, 25);
            this.btnPrecios.TabIndex = 156;
            this.btnPrecios.Text = "Precios";
            this.btnPrecios.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrecios.UseVisualStyleBackColor = false;
            this.btnPrecios.Click += new System.EventHandler(this.btnPrecios_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::SAMBHS.Windows.WinClient.UI.Resource.page_excel1;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(829, 421);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 25);
            this.button1.TabIndex = 155;
            this.button1.Text = "Exportar Excel";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 562);
            this.Controls.Add(this.btnPrecios);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnschedule);
            this.Controls.Add(this.btnCambiarProtocolo);
            this.Controls.Add(this.btnEditarTrabajador);
            this.Controls.Add(this.btnFacturar);
            this.Controls.Add(this.btnPerson);
            this.Controls.Add(this.grdDataCalendar);
            this.Controls.Add(this.gbBusqueda);
            this.Name = "frmCotizacion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cotizacion";
            this.gbBusqueda.ResumeLayout(false);
            this.gbBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataCalendar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBusqueda;
        private System.Windows.Forms.DateTimePicker dtpDateTimeStar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dptDateTimeEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNroDocument;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPacient;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFilter;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataCalendar;
        private System.Windows.Forms.Button btnPerson;
        private System.Windows.Forms.Button btnFacturar;
        private System.Windows.Forms.Button btnEditarTrabajador;
        private System.Windows.Forms.Button btnCambiarProtocolo;
        private System.Windows.Forms.Button btnschedule;
        private System.Windows.Forms.Button button1;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.Button btnPrecios;
    }
}