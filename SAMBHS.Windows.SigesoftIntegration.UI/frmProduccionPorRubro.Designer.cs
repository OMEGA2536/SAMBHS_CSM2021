namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    partial class frmProduccionPorRubro
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("userName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("hoursJob");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("monthPayment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("nroAtx");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_priceTotal");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("moneyGross");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("factorProdxhora");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("factorAtxHora");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.DataVisualization.CategoryXAxis categoryXAxis1 = new Infragistics.Win.DataVisualization.CategoryXAxis();
            Infragistics.Win.DataVisualization.NumericYAxis numericYAxis1 = new Infragistics.Win.DataVisualization.NumericYAxis();
            Infragistics.Win.DataVisualization.ColumnSeries columnSeries1 = new Infragistics.Win.DataVisualization.ColumnSeries();
            this.grdProduccionByItem = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraDataChart1 = new Infragistics.Win.DataVisualization.UltraDataChart();
            this.btnExportarBandeja = new System.Windows.Forms.Button();
            this.lbl_xls = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdProduccionByItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdProduccionByItem
            // 
            this.grdProduccionByItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProduccionByItem.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdProduccionByItem.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "MÉDICO";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 296;
            ultraGridColumn2.Header.Caption = "HORAS ATENDIDAS";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn5.Header.Caption = "HONORARIOS";
            ultraGridColumn5.Header.VisiblePosition = 2;
            ultraGridColumn6.Header.Caption = "NRO DE  ATENCIONES";
            ultraGridColumn6.Header.VisiblePosition = 3;
            ultraGridColumn7.Header.Caption = "PRODUCCIÓN GENERADA";
            ultraGridColumn7.Header.VisiblePosition = 4;
            ultraGridColumn8.Header.Caption = "PRODUCCION - HONORARIOS";
            ultraGridColumn8.Header.VisiblePosition = 5;
            ultraGridColumn9.Header.Caption = "FACTOR PRODUCCION X HORA";
            ultraGridColumn9.Header.VisiblePosition = 6;
            ultraGridColumn16.Header.Caption = "FACTOR PACIENTES ATENDIDOS X HORA";
            ultraGridColumn16.Header.VisiblePosition = 7;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn16});
            this.grdProduccionByItem.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdProduccionByItem.DisplayLayout.InterBandSpacing = 10;
            this.grdProduccionByItem.DisplayLayout.MaxColScrollRegions = 1;
            this.grdProduccionByItem.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdProduccionByItem.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdProduccionByItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdProduccionByItem.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdProduccionByItem.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdProduccionByItem.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdProduccionByItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdProduccionByItem.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdProduccionByItem.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdProduccionByItem.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdProduccionByItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdProduccionByItem.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdProduccionByItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdProduccionByItem.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdProduccionByItem.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdProduccionByItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdProduccionByItem.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdProduccionByItem.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdProduccionByItem.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdProduccionByItem.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdProduccionByItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdProduccionByItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdProduccionByItem.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdProduccionByItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.grdProduccionByItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProduccionByItem.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdProduccionByItem.Location = new System.Drawing.Point(11, 11);
            this.grdProduccionByItem.Margin = new System.Windows.Forms.Padding(2);
            this.grdProduccionByItem.Name = "grdProduccionByItem";
            this.grdProduccionByItem.Size = new System.Drawing.Size(1322, 298);
            this.grdProduccionByItem.TabIndex = 51;
            // 
            // ultraDataChart1
            // 
            categoryXAxis1.Id = new System.Guid("64f47d6b-bef9-4258-8ea0-6042b03a8333");
            categoryXAxis1.LabelMargin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            categoryXAxis1.TitleHorizontalAlignment = Infragistics.Portable.Components.UI.HorizontalAlignment.Center;
            numericYAxis1.Id = new System.Guid("f0526ab7-021d-4c3b-97b7-df9dde06d4a0");
            numericYAxis1.LabelHorizontalAlignment = Infragistics.Portable.Components.UI.HorizontalAlignment.Right;
            numericYAxis1.LabelLocation = Infragistics.Win.DataVisualization.AxisLabelsLocation.OutsideLeft;
            numericYAxis1.LabelMargin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            numericYAxis1.TitleHorizontalAlignment = Infragistics.Portable.Components.UI.HorizontalAlignment.Center;
            this.ultraDataChart1.Axes.Add(categoryXAxis1);
            this.ultraDataChart1.Axes.Add(numericYAxis1);
            this.ultraDataChart1.BackColor = System.Drawing.Color.White;
            this.ultraDataChart1.CrosshairPoint = new Infragistics.Win.DataVisualization.Point(double.NaN, double.NaN);
            this.ultraDataChart1.Location = new System.Drawing.Point(12, 314);
            this.ultraDataChart1.Name = "ultraDataChart1";
            this.ultraDataChart1.PreviewRect = new Infragistics.Win.DataVisualization.Rectangle(double.PositiveInfinity, double.PositiveInfinity, double.NegativeInfinity, double.NegativeInfinity);
            columnSeries1.AreaFillOpacity = double.NaN;
            columnSeries1.Resolution = 4D;
            columnSeries1.Title = "Series Title";
            columnSeries1.XAxisId = new System.Guid("64f47d6b-bef9-4258-8ea0-6042b03a8333");
            columnSeries1.YAxisId = new System.Guid("f0526ab7-021d-4c3b-97b7-df9dde06d4a0");
            this.ultraDataChart1.Series.Add(columnSeries1);
            this.ultraDataChart1.Size = new System.Drawing.Size(1320, 366);
            this.ultraDataChart1.TabIndex = 52;
            this.ultraDataChart1.Text = "ultraDataChart1";
            this.ultraDataChart1.TitleFontSize = 12D;
            // 
            // btnExportarBandeja
            // 
            this.btnExportarBandeja.Image = global::SAMBHS.Windows.SigesoftIntegration.UI.Properties.Resources.xls__1_;
            this.btnExportarBandeja.Location = new System.Drawing.Point(12, 686);
            this.btnExportarBandeja.Name = "btnExportarBandeja";
            this.btnExportarBandeja.Size = new System.Drawing.Size(34, 34);
            this.btnExportarBandeja.TabIndex = 57;
            this.btnExportarBandeja.UseVisualStyleBackColor = true;
            this.btnExportarBandeja.Click += new System.EventHandler(this.btnExportarBandeja_Click);
            // 
            // lbl_xls
            // 
            this.lbl_xls.AutoSize = true;
            this.lbl_xls.Location = new System.Drawing.Point(53, 703);
            this.lbl_xls.Name = "lbl_xls";
            this.lbl_xls.Size = new System.Drawing.Size(0, 13);
            this.lbl_xls.TabIndex = 58;
            // 
            // frmProduccionPorRubro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 721);
            this.Controls.Add(this.lbl_xls);
            this.Controls.Add(this.btnExportarBandeja);
            this.Controls.Add(this.ultraDataChart1);
            this.Controls.Add(this.grdProduccionByItem);
            this.Name = "frmProduccionPorRubro";
            this.ShowIcon = false;
            this.Text = "Produccion por rubro";
            this.Load += new System.EventHandler(this.frmProduccionPorRubro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdProduccionByItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataChart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdProduccionByItem;
        private Infragistics.Win.DataVisualization.UltraDataChart ultraDataChart1;
        private System.Windows.Forms.Button btnExportarBandeja;
        private System.Windows.Forms.Label lbl_xls;
    }
}