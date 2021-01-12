namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    partial class frmProduccion
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Area");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Tipo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_Price");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_MedicoTratante");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Fecha");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ServiceId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_PersonName");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.DataVisualization.CategoryXAxis categoryXAxis1 = new Infragistics.Win.DataVisualization.CategoryXAxis();
            Infragistics.Win.DataVisualization.NumericYAxis numericYAxis1 = new Infragistics.Win.DataVisualization.NumericYAxis();
            Infragistics.Win.DataVisualization.ColumnSeries columnSeries1 = new Infragistics.Win.DataVisualization.ColumnSeries();
            this.grdProduccion = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbConsultorio = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpF_Fin = new System.Windows.Forms.DateTimePicker();
            this.dtpF_Inicio = new System.Windows.Forms.DateTimePicker();
            this.ultraDataChart1 = new Infragistics.Win.DataVisualization.UltraDataChart();
            this.btnExportarBandeja = new System.Windows.Forms.Button();
            this.btnProdRubro = new System.Windows.Forms.Button();
            this.btnDetallado = new System.Windows.Forms.Button();
            this.btnReporte = new System.Windows.Forms.Button();
            this.lbl_xls = new System.Windows.Forms.Label();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            ((System.ComponentModel.ISupportInitialize)(this.grdProduccion)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataChart1)).BeginInit();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdProduccion
            // 
            this.grdProduccion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProduccion.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdProduccion.DisplayLayout.Appearance = appearance1;
            ultraGridColumn10.Header.VisiblePosition = 0;
            ultraGridColumn11.Header.VisiblePosition = 1;
            ultraGridColumn3.Header.Caption = "Componente";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 305;
            ultraGridColumn4.Header.Caption = "Precio";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn12.Header.VisiblePosition = 4;
            ultraGridColumn13.Header.VisiblePosition = 5;
            ultraGridColumn14.Header.VisiblePosition = 6;
            ultraGridColumn15.Header.VisiblePosition = 7;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15});
            this.grdProduccion.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdProduccion.DisplayLayout.InterBandSpacing = 10;
            this.grdProduccion.DisplayLayout.MaxColScrollRegions = 1;
            this.grdProduccion.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdProduccion.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdProduccion.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdProduccion.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdProduccion.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdProduccion.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdProduccion.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdProduccion.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdProduccion.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdProduccion.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdProduccion.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdProduccion.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdProduccion.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdProduccion.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdProduccion.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdProduccion.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdProduccion.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdProduccion.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdProduccion.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdProduccion.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdProduccion.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdProduccion.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdProduccion.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdProduccion.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.grdProduccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProduccion.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdProduccion.Location = new System.Drawing.Point(11, 83);
            this.grdProduccion.Margin = new System.Windows.Forms.Padding(2);
            this.grdProduccion.Name = "grdProduccion";
            this.grdProduccion.Size = new System.Drawing.Size(1160, 154);
            this.grdProduccion.TabIndex = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.cbUsers);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbConsultorio);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpF_Fin);
            this.groupBox1.Controls.Add(this.dtpF_Inicio);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(825, 54);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Búsqueda";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::SAMBHS.Windows.SigesoftIntegration.UI.Properties.Resources.accept;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(743, 19);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cbUsers
            // 
            this.cbUsers.FormattingEnabled = true;
            this.cbUsers.Location = new System.Drawing.Point(626, 20);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(99, 21);
            this.cbUsers.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(565, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Usuario";
            // 
            // cbConsultorio
            // 
            this.cbConsultorio.FormattingEnabled = true;
            this.cbConsultorio.Location = new System.Drawing.Point(426, 20);
            this.cbConsultorio.Name = "cbConsultorio";
            this.cbConsultorio.Size = new System.Drawing.Size(121, 21);
            this.cbConsultorio.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(349, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Consultorio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hasta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Desde";
            // 
            // dtpF_Fin
            // 
            this.dtpF_Fin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpF_Fin.Location = new System.Drawing.Point(232, 20);
            this.dtpF_Fin.Name = "dtpF_Fin";
            this.dtpF_Fin.Size = new System.Drawing.Size(99, 20);
            this.dtpF_Fin.TabIndex = 0;
            // 
            // dtpF_Inicio
            // 
            this.dtpF_Inicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpF_Inicio.Location = new System.Drawing.Point(62, 20);
            this.dtpF_Inicio.Name = "dtpF_Inicio";
            this.dtpF_Inicio.Size = new System.Drawing.Size(99, 20);
            this.dtpF_Inicio.TabIndex = 0;
            // 
            // ultraDataChart1
            // 
            categoryXAxis1.Id = new System.Guid("a5a31d19-54ca-49ca-8bd5-a60be5ab4b84");
            categoryXAxis1.LabelMargin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            categoryXAxis1.TitleHorizontalAlignment = Infragistics.Portable.Components.UI.HorizontalAlignment.Center;
            numericYAxis1.Id = new System.Guid("36b7b4f3-c54c-4ea5-9e8b-ee84297f8aec");
            numericYAxis1.LabelHorizontalAlignment = Infragistics.Portable.Components.UI.HorizontalAlignment.Right;
            numericYAxis1.LabelLocation = Infragistics.Win.DataVisualization.AxisLabelsLocation.OutsideLeft;
            numericYAxis1.LabelMargin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            numericYAxis1.TitleHorizontalAlignment = Infragistics.Portable.Components.UI.HorizontalAlignment.Center;
            this.ultraDataChart1.Axes.Add(categoryXAxis1);
            this.ultraDataChart1.Axes.Add(numericYAxis1);
            this.ultraDataChart1.BackColor = System.Drawing.Color.White;
            this.ultraDataChart1.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.ultraDataChart1.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.ultraDataChart1.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.ultraDataChart1.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.ultraDataChart1.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.ultraDataChart1.CrosshairPoint = new Infragistics.Win.DataVisualization.Point(double.NaN, double.NaN);
            this.ultraDataChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraDataChart1.GridMode = Infragistics.Win.DataVisualization.GridMode.BeforeSeries;
            this.ultraDataChart1.HorizontalZoomable = true;
            this.ultraDataChart1.Location = new System.Drawing.Point(0, 0);
            this.ultraDataChart1.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.ultraDataChart1.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.ultraDataChart1.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.ultraDataChart1.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.ultraDataChart1.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.ultraDataChart1.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.ultraDataChart1.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.ultraDataChart1.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.ultraDataChart1.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.ultraDataChart1.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.ultraDataChart1.Name = "ultraDataChart1";
            this.ultraDataChart1.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.ultraDataChart1.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.ultraDataChart1.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.ultraDataChart1.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.ultraDataChart1.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.ultraDataChart1.PreviewRect = new Infragistics.Win.DataVisualization.Rectangle(0D, 0D, double.NegativeInfinity, double.NegativeInfinity);
            columnSeries1.AreaFillOpacity = double.NaN;
            columnSeries1.Resolution = 4D;
            columnSeries1.Title = "Series Title";
            columnSeries1.XAxisId = new System.Guid("a5a31d19-54ca-49ca-8bd5-a60be5ab4b84");
            columnSeries1.YAxisId = new System.Guid("36b7b4f3-c54c-4ea5-9e8b-ee84297f8aec");
            this.ultraDataChart1.Series.Add(columnSeries1);
            this.ultraDataChart1.Size = new System.Drawing.Size(1341, 454);
            this.ultraDataChart1.TabIndex = 54;
            this.ultraDataChart1.Text = "ultraDataChart1";
            this.ultraDataChart1.TitleFontSize = 12D;
            this.ultraDataChart1.WindowResponse = Infragistics.Win.DataVisualization.WindowResponse.Immediate;
            this.ultraDataChart1.TooltipShowing += new Infragistics.Win.DataVisualization.TooltipShowingEventHandler(this.ultraDataChart1_TooltipShowing);
            this.ultraDataChart1.Layout += new System.Windows.Forms.LayoutEventHandler(this.ultraDataChart1_Layout);
            // 
            // btnExportarBandeja
            // 
            this.btnExportarBandeja.Image = global::SAMBHS.Windows.SigesoftIntegration.UI.Properties.Resources.xls__1_;
            this.btnExportarBandeja.Location = new System.Drawing.Point(1182, 203);
            this.btnExportarBandeja.Name = "btnExportarBandeja";
            this.btnExportarBandeja.Size = new System.Drawing.Size(34, 34);
            this.btnExportarBandeja.TabIndex = 56;
            this.btnExportarBandeja.UseVisualStyleBackColor = true;
            this.btnExportarBandeja.Click += new System.EventHandler(this.btnExportarBandeja_Click);
            // 
            // btnProdRubro
            // 
            this.btnProdRubro.Image = global::SAMBHS.Windows.SigesoftIntegration.UI.Properties.Resources.home_printer_512;
            this.btnProdRubro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProdRubro.Location = new System.Drawing.Point(1182, 163);
            this.btnProdRubro.Name = "btnProdRubro";
            this.btnProdRubro.Size = new System.Drawing.Size(140, 34);
            this.btnProdRubro.TabIndex = 55;
            this.btnProdRubro.Text = "Producción por rubro";
            this.btnProdRubro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProdRubro.UseVisualStyleBackColor = true;
            this.btnProdRubro.Click += new System.EventHandler(this.btnProdRubro_Click);
            // 
            // btnDetallado
            // 
            this.btnDetallado.Image = global::SAMBHS.Windows.SigesoftIntegration.UI.Properties.Resources.facturar;
            this.btnDetallado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetallado.Location = new System.Drawing.Point(1182, 123);
            this.btnDetallado.Name = "btnDetallado";
            this.btnDetallado.Size = new System.Drawing.Size(140, 34);
            this.btnDetallado.TabIndex = 53;
            this.btnDetallado.Text = "Reporte Detallado";
            this.btnDetallado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDetallado.UseVisualStyleBackColor = true;
            this.btnDetallado.Click += new System.EventHandler(this.btnDetallado_Click);
            // 
            // btnReporte
            // 
            this.btnReporte.Image = global::SAMBHS.Windows.SigesoftIntegration.UI.Properties.Resources.ruta;
            this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReporte.Location = new System.Drawing.Point(1182, 83);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(140, 34);
            this.btnReporte.TabIndex = 52;
            this.btnReporte.Text = "Reporte Acumulado";
            this.btnReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReporte.UseVisualStyleBackColor = true;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // lbl_xls
            // 
            this.lbl_xls.AutoSize = true;
            this.lbl_xls.Location = new System.Drawing.Point(1221, 208);
            this.lbl_xls.Name = "lbl_xls";
            this.lbl_xls.Size = new System.Drawing.Size(0, 13);
            this.lbl_xls.TabIndex = 57;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.ultraDataChart1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1341, 454);
            this.toolStripContainer1.Location = new System.Drawing.Point(1, 242);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1341, 479);
            this.toolStripContainer1.TabIndex = 58;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // frmProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 721);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.lbl_xls);
            this.Controls.Add(this.btnExportarBandeja);
            this.Controls.Add(this.btnProdRubro);
            this.Controls.Add(this.btnDetallado);
            this.Controls.Add(this.btnReporte);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grdProduccion);
            this.Name = "frmProduccion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Produccion";
            this.Load += new System.EventHandler(this.frmProduccion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdProduccion)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataChart1)).EndInit();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdProduccion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpF_Fin;
        private System.Windows.Forms.DateTimePicker dtpF_Inicio;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbConsultorio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Button btnDetallado;
        private Infragistics.Win.DataVisualization.UltraDataChart ultraDataChart1;
        private System.Windows.Forms.Button btnProdRubro;
        private System.Windows.Forms.Button btnExportarBandeja;
        private System.Windows.Forms.Label lbl_xls;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    }
}