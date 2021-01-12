namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    partial class frmCotizacionCompletados
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CotizacionId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_CostoTotal");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_PersonId");
            this.grdDataCalendar = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnReporte = new System.Windows.Forms.Button();
            this.gbBusqueda = new System.Windows.Forms.GroupBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dptDateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDateTimeStar = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataCalendar)).BeginInit();
            this.gbBusqueda.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdDataCalendar
            // 
            this.grdDataCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataCalendar.CausesValidation = false;
            this.grdDataCalendar.DataMember = null;
            ultraGridColumn14.Header.Caption = "Paciente";
            ultraGridColumn14.Header.VisiblePosition = 3;
            ultraGridColumn14.Width = 258;
            ultraGridColumn15.Header.Caption = "DNI";
            ultraGridColumn15.Header.VisiblePosition = 4;
            ultraGridColumn30.Header.Caption = "Protocolo";
            ultraGridColumn30.Header.VisiblePosition = 5;
            ultraGridColumn30.Width = 233;
            ultraGridColumn34.Header.Caption = "Usuario Capta";
            ultraGridColumn34.Header.VisiblePosition = 1;
            ultraGridColumn34.Width = 125;
            ultraGridColumn1.Header.Caption = "Fecha de Captación";
            ultraGridColumn1.Header.VisiblePosition = 2;
            ultraGridColumn2.Header.Caption = "Cotizacion Id";
            ultraGridColumn2.Header.VisiblePosition = 0;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.Caption = "Precio Protocolo";
            ultraGridColumn3.Header.VisiblePosition = 6;
            ultraGridColumn6.Header.VisiblePosition = 7;
            ultraGridColumn6.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn30,
            ultraGridColumn34,
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
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
            this.grdDataCalendar.Location = new System.Drawing.Point(12, 95);
            this.grdDataCalendar.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataCalendar.Name = "grdDataCalendar";
            this.grdDataCalendar.Size = new System.Drawing.Size(992, 364);
            this.grdDataCalendar.TabIndex = 56;
            // 
            // btnReporte
            // 
            this.btnReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReporte.BackColor = System.Drawing.SystemColors.Control;
            this.btnReporte.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnReporte.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnReporte.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporte.Image = global::SAMBHS.Windows.WinClient.UI.Resource.page_excel1;
            this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReporte.Location = new System.Drawing.Point(11, 474);
            this.btnReporte.Margin = new System.Windows.Forms.Padding(2);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(118, 25);
            this.btnReporte.TabIndex = 154;
            this.btnReporte.Text = "Exportar Excel";
            this.btnReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReporte.UseVisualStyleBackColor = false;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // gbBusqueda
            // 
            this.gbBusqueda.Controls.Add(this.btnFilter);
            this.gbBusqueda.Controls.Add(this.dptDateTimeEnd);
            this.gbBusqueda.Controls.Add(this.label2);
            this.gbBusqueda.Controls.Add(this.dtpDateTimeStar);
            this.gbBusqueda.Controls.Add(this.label1);
            this.gbBusqueda.Location = new System.Drawing.Point(12, 21);
            this.gbBusqueda.Name = "gbBusqueda";
            this.gbBusqueda.Size = new System.Drawing.Size(990, 53);
            this.gbBusqueda.TabIndex = 155;
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
            this.btnFilter.Location = new System.Drawing.Point(391, 16);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(73, 24);
            this.btnFilter.TabIndex = 15;
            this.btnFilter.Text = "Filtrar";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
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
            // frmCotizacionCompletados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 510);
            this.Controls.Add(this.gbBusqueda);
            this.Controls.Add(this.btnReporte);
            this.Controls.Add(this.grdDataCalendar);
            this.Name = "frmCotizacionCompletados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmCotizacionCompletados";
            ((System.ComponentModel.ISupportInitialize)(this.grdDataCalendar)).EndInit();
            this.gbBusqueda.ResumeLayout(false);
            this.gbBusqueda.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataCalendar;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.GroupBox gbBusqueda;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DateTimePicker dptDateTimeEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDateTimeStar;
        private System.Windows.Forms.Label label1;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
    }
}