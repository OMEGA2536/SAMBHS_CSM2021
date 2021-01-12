namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    partial class frmPrecioExamenes
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("b_Seleccionar");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_BasePrice");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("b_Seleccionar");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_BasePrice");
            this.grdComponents = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdComponentDetail = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdComponents)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdComponentDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // grdComponents
            // 
            this.grdComponents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdComponents.CausesValidation = false;
            ultraGridColumn1.Header.Caption = "Seleccione";
            ultraGridColumn1.Header.CheckBoxVisibility = Infragistics.Win.UltraWinGrid.HeaderCheckBoxVisibility.Always;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 37;
            ultraGridColumn2.Header.Caption = "Examen";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 197;
            ultraGridColumn3.Header.Caption = "Precio";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3});
            this.grdComponents.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdComponents.DisplayLayout.MaxColScrollRegions = 1;
            this.grdComponents.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdComponents.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdComponents.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdComponents.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponents.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdComponents.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdComponents.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponents.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdComponents.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdComponents.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdComponents.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdComponents.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdComponents.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdComponents.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdComponents.Location = new System.Drawing.Point(11, 38);
            this.grdComponents.Margin = new System.Windows.Forms.Padding(2);
            this.grdComponents.Name = "grdComponents";
            this.grdComponents.Size = new System.Drawing.Size(369, 330);
            this.grdComponents.TabIndex = 56;
            this.grdComponents.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.grdComponents_ClickCell);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Nombre";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(64, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(225, 20);
            this.txtName.TabIndex = 58;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Location = new System.Drawing.Point(305, 13);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrar.TabIndex = 59;
            this.btnFiltrar.Text = "Buscar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdComponentDetail);
            this.groupBox1.Location = new System.Drawing.Point(444, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 328);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalles";
            // 
            // grdComponentDetail
            // 
            this.grdComponentDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdComponentDetail.CausesValidation = false;
            ultraGridColumn58.Header.Caption = "Seleccione";
            ultraGridColumn58.Header.CheckBoxVisibility = Infragistics.Win.UltraWinGrid.HeaderCheckBoxVisibility.Always;
            ultraGridColumn58.Header.VisiblePosition = 0;
            ultraGridColumn58.Width = 37;
            ultraGridColumn7.Header.Caption = "Examen";
            ultraGridColumn7.Header.VisiblePosition = 1;
            ultraGridColumn7.Width = 201;
            ultraGridColumn8.Header.Caption = "Precio";
            ultraGridColumn8.Header.VisiblePosition = 2;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn58,
            ultraGridColumn7,
            ultraGridColumn8});
            this.grdComponentDetail.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdComponentDetail.DisplayLayout.MaxColScrollRegions = 1;
            this.grdComponentDetail.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdComponentDetail.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdComponentDetail.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdComponentDetail.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponentDetail.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdComponentDetail.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdComponentDetail.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponentDetail.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdComponentDetail.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdComponentDetail.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdComponentDetail.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdComponentDetail.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdComponentDetail.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdComponentDetail.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdComponentDetail.Location = new System.Drawing.Point(5, 25);
            this.grdComponentDetail.Margin = new System.Windows.Forms.Padding(2);
            this.grdComponentDetail.Name = "grdComponentDetail";
            this.grdComponentDetail.Size = new System.Drawing.Size(413, 298);
            this.grdComponentDetail.TabIndex = 57;
            this.grdComponentDetail.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.grdComponentDetail_ClickCell);
            // 
            // btnAgregar
            // 
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Image = global::SAMBHS.Windows.WinClient.UI.Resource.play_button;
            this.btnAgregar.Location = new System.Drawing.Point(385, 105);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(53, 23);
            this.btnAgregar.TabIndex = 61;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemover.Image = global::SAMBHS.Windows.WinClient.UI.Resource.control_remove;
            this.btnRemover.Location = new System.Drawing.Point(385, 146);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(53, 23);
            this.btnRemover.TabIndex = 62;
            this.btnRemover.UseVisualStyleBackColor = true;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(751, 347);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(116, 20);
            this.txtTotal.TabIndex = 64;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(685, 350);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Total";
            // 
            // frmPrecioExamenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 379);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdComponents);
            this.Name = "frmPrecioExamenes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmPrecioExamenes";
            this.Load += new System.EventHandler(this.frmPrecioExamenes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdComponents)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdComponentDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdComponents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.GroupBox groupBox1;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdComponentDetail;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label2;
    }
}