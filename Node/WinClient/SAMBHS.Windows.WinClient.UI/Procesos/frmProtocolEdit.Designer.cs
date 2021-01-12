namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    partial class frmProtocolEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProtocolEdit));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProtocolComponentId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_Price");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Operator");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_Age");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Gender");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_IsConditional");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentTypeName");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbConsultorio = new System.Windows.Forms.ComboBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.chkEsActivo = new System.Windows.Forms.CheckBox();
            this.txtCamaHosp = new System.Windows.Forms.TextBox();
            this.btnAddConsultorio = new System.Windows.Forms.Button();
            this.btnAgregarEmpresaContrata = new System.Windows.Forms.Button();
            this.cbEmpresaCliente = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbServicio = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbTipoServicio = new System.Windows.Forms.ComboBox();
            this.txtEps = new System.Windows.Forms.TextBox();
            this.txtFactor = new System.Windows.Forms.TextBox();
            this.txtCentroCosto = new System.Windows.Forms.TextBox();
            this.lblEps = new System.Windows.Forms.Label();
            this.lblFactor = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbEmpresaTrabajo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbGeso = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbTipoExamen = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbEmpresaEmpleadora = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNombreProtocolo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblBedHospital = new System.Windows.Forms.Label();
            this.grdProtocolComponent = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboVendedor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtComision = new System.Windows.Forms.TextBox();
            this.chkEsComisionable = new System.Windows.Forms.CheckBox();
            this.uvProtocol = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProtocolComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvProtocol)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbConsultorio);
            this.groupBox1.Controls.Add(this.txtDiscount);
            this.groupBox1.Controls.Add(this.lblDescuento);
            this.groupBox1.Controls.Add(this.chkEsActivo);
            this.groupBox1.Controls.Add(this.txtCamaHosp);
            this.groupBox1.Controls.Add(this.btnAddConsultorio);
            this.groupBox1.Controls.Add(this.btnAgregarEmpresaContrata);
            this.groupBox1.Controls.Add(this.cbEmpresaCliente);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbServicio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbTipoServicio);
            this.groupBox1.Controls.Add(this.txtEps);
            this.groupBox1.Controls.Add(this.txtFactor);
            this.groupBox1.Controls.Add(this.txtCentroCosto);
            this.groupBox1.Controls.Add(this.lblEps);
            this.groupBox1.Controls.Add(this.lblFactor);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cbEmpresaTrabajo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbGeso);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cbTipoExamen);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbEmpresaEmpleadora);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtNombreProtocolo);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblBedHospital);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(986, 181);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Protocolo";
            // 
            // cbConsultorio
            // 
            this.cbConsultorio.FormattingEnabled = true;
            this.cbConsultorio.Location = new System.Drawing.Point(386, 131);
            this.cbConsultorio.Name = "cbConsultorio";
            this.cbConsultorio.Size = new System.Drawing.Size(163, 21);
            this.cbConsultorio.TabIndex = 64;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(929, 132);
            this.txtDiscount.MaxLength = 250;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(42, 20);
            this.txtDiscount.TabIndex = 63;
            this.txtDiscount.Text = "0.00";
            // 
            // lblDescuento
            // 
            this.lblDescuento.BackColor = System.Drawing.Color.Transparent;
            this.lblDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescuento.ForeColor = System.Drawing.Color.Black;
            this.lblDescuento.Location = new System.Drawing.Point(881, 132);
            this.lblDescuento.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(43, 20);
            this.lblDescuento.TabIndex = 62;
            this.lblDescuento.Text = "Dscto";
            this.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDescuento.Visible = false;
            // 
            // chkEsActivo
            // 
            this.chkEsActivo.AutoSize = true;
            this.chkEsActivo.Checked = true;
            this.chkEsActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEsActivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEsActivo.ForeColor = System.Drawing.Color.Black;
            this.chkEsActivo.Location = new System.Drawing.Point(911, 164);
            this.chkEsActivo.Name = "chkEsActivo";
            this.chkEsActivo.Size = new System.Drawing.Size(56, 17);
            this.chkEsActivo.TabIndex = 36;
            this.chkEsActivo.Text = "Activo";
            this.chkEsActivo.UseVisualStyleBackColor = true;
            // 
            // txtCamaHosp
            // 
            this.txtCamaHosp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCamaHosp.Location = new System.Drawing.Point(834, 132);
            this.txtCamaHosp.MaxLength = 250;
            this.txtCamaHosp.Name = "txtCamaHosp";
            this.txtCamaHosp.Size = new System.Drawing.Size(42, 20);
            this.txtCamaHosp.TabIndex = 24;
            this.txtCamaHosp.Text = "0.00";
            // 
            // btnAddConsultorio
            // 
            this.btnAddConsultorio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddConsultorio.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddConsultorio.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAddConsultorio.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAddConsultorio.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAddConsultorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddConsultorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddConsultorio.ForeColor = System.Drawing.Color.Black;
            this.btnAddConsultorio.Image = global::SAMBHS.Windows.WinClient.UI.Properties.Resources.agenda1;
            this.btnAddConsultorio.Location = new System.Drawing.Point(553, 130);
            this.btnAddConsultorio.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddConsultorio.Name = "btnAddConsultorio";
            this.btnAddConsultorio.Size = new System.Drawing.Size(27, 21);
            this.btnAddConsultorio.TabIndex = 61;
            this.btnAddConsultorio.UseVisualStyleBackColor = false;
            this.btnAddConsultorio.Click += new System.EventHandler(this.btnAddConsultorio_Click);
            // 
            // btnAgregarEmpresaContrata
            // 
            this.btnAgregarEmpresaContrata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregarEmpresaContrata.BackColor = System.Drawing.SystemColors.Control;
            this.btnAgregarEmpresaContrata.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAgregarEmpresaContrata.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAgregarEmpresaContrata.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAgregarEmpresaContrata.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarEmpresaContrata.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarEmpresaContrata.ForeColor = System.Drawing.Color.Black;
            this.btnAgregarEmpresaContrata.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarEmpresaContrata.Image")));
            this.btnAgregarEmpresaContrata.Location = new System.Drawing.Point(553, 91);
            this.btnAgregarEmpresaContrata.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarEmpresaContrata.Name = "btnAgregarEmpresaContrata";
            this.btnAgregarEmpresaContrata.Size = new System.Drawing.Size(27, 21);
            this.btnAgregarEmpresaContrata.TabIndex = 61;
            this.btnAgregarEmpresaContrata.UseVisualStyleBackColor = false;
            // 
            // cbEmpresaCliente
            // 
            this.cbEmpresaCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEmpresaCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEmpresaCliente.DropDownWidth = 500;
            this.cbEmpresaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEmpresaCliente.FormattingEnabled = true;
            this.cbEmpresaCliente.Location = new System.Drawing.Point(112, 51);
            this.cbEmpresaCliente.Name = "cbEmpresaCliente";
            this.cbEmpresaCliente.Size = new System.Drawing.Size(437, 21);
            this.cbEmpresaCliente.TabIndex = 22;
            this.uvProtocol.GetValidationSettings(this.cbEmpresaCliente).IsRequired = true;
            this.cbEmpresaCliente.SelectedIndexChanged += new System.EventHandler(this.cbEmpresaCliente_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 21);
            this.label3.TabIndex = 21;
            this.label3.Text = "Emp. Cliente";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbServicio
            // 
            this.cbServicio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbServicio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbServicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbServicio.FormattingEnabled = true;
            this.cbServicio.Location = new System.Drawing.Point(686, 78);
            this.cbServicio.Name = "cbServicio";
            this.cbServicio.Size = new System.Drawing.Size(285, 21);
            this.cbServicio.TabIndex = 32;
            this.uvProtocol.GetValidationSettings(this.cbServicio).IsRequired = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(622, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 19);
            this.label1.TabIndex = 31;
            this.label1.Text = "Servicio";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(596, 52);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 20);
            this.label8.TabIndex = 30;
            this.label8.Text = "Tipo Servicio";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbTipoServicio
            // 
            this.cbTipoServicio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTipoServicio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTipoServicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoServicio.FormattingEnabled = true;
            this.cbTipoServicio.Location = new System.Drawing.Point(686, 51);
            this.cbTipoServicio.Name = "cbTipoServicio";
            this.cbTipoServicio.Size = new System.Drawing.Size(285, 21);
            this.cbTipoServicio.TabIndex = 26;
            this.uvProtocol.GetValidationSettings(this.cbTipoServicio).IsRequired = true;
            this.cbTipoServicio.SelectedValueChanged += new System.EventHandler(this.cbTipoServicio_SelectedValueChanged);
            this.cbTipoServicio.TextChanged += new System.EventHandler(this.cbTipoServicio_TextChanged);
            // 
            // txtEps
            // 
            this.txtEps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEps.Location = new System.Drawing.Point(720, 132);
            this.txtEps.MaxLength = 250;
            this.txtEps.Name = "txtEps";
            this.txtEps.Size = new System.Drawing.Size(42, 20);
            this.txtEps.TabIndex = 24;
            this.txtEps.Text = "0.00";
            // 
            // txtFactor
            // 
            this.txtFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFactor.Location = new System.Drawing.Point(626, 132);
            this.txtFactor.MaxLength = 250;
            this.txtFactor.Name = "txtFactor";
            this.txtFactor.Size = new System.Drawing.Size(42, 20);
            this.txtFactor.TabIndex = 24;
            this.txtFactor.Text = "0.00";
            // 
            // txtCentroCosto
            // 
            this.txtCentroCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCentroCosto.Location = new System.Drawing.Point(686, 105);
            this.txtCentroCosto.MaxLength = 250;
            this.txtCentroCosto.Name = "txtCentroCosto";
            this.txtCentroCosto.Size = new System.Drawing.Size(285, 20);
            this.txtCentroCosto.TabIndex = 24;
            this.txtCentroCosto.Visible = false;
            // 
            // lblEps
            // 
            this.lblEps.BackColor = System.Drawing.Color.Transparent;
            this.lblEps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEps.ForeColor = System.Drawing.Color.Black;
            this.lblEps.Location = new System.Drawing.Point(686, 132);
            this.lblEps.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEps.Name = "lblEps";
            this.lblEps.Size = new System.Drawing.Size(48, 20);
            this.lblEps.TabIndex = 23;
            this.lblEps.Text = "E.P.S";
            this.lblEps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEps.Visible = false;
            // 
            // lblFactor
            // 
            this.lblFactor.BackColor = System.Drawing.Color.Transparent;
            this.lblFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactor.ForeColor = System.Drawing.Color.Black;
            this.lblFactor.Location = new System.Drawing.Point(564, 132);
            this.lblFactor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFactor.Name = "lblFactor";
            this.lblFactor.Size = new System.Drawing.Size(48, 20);
            this.lblFactor.TabIndex = 23;
            this.lblFactor.Text = "Factor";
            this.lblFactor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFactor.Visible = false;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(624, 104);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 20);
            this.label13.TabIndex = 23;
            this.label13.Text = "C/Costo";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label13.Visible = false;
            // 
            // cbEmpresaTrabajo
            // 
            this.cbEmpresaTrabajo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEmpresaTrabajo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEmpresaTrabajo.DropDownWidth = 500;
            this.cbEmpresaTrabajo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEmpresaTrabajo.FormattingEnabled = true;
            this.cbEmpresaTrabajo.Location = new System.Drawing.Point(112, 105);
            this.cbEmpresaTrabajo.Name = "cbEmpresaTrabajo";
            this.cbEmpresaTrabajo.Size = new System.Drawing.Size(437, 21);
            this.cbEmpresaTrabajo.TabIndex = 20;
            this.uvProtocol.GetValidationSettings(this.cbEmpresaTrabajo).IsRequired = true;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(8, 105);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 21);
            this.label7.TabIndex = 19;
            this.label7.Text = "Emp. de Trabajo";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbGeso
            // 
            this.cbGeso.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbGeso.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGeso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGeso.FormattingEnabled = true;
            this.cbGeso.Location = new System.Drawing.Point(686, 24);
            this.cbGeso.Name = "cbGeso";
            this.cbGeso.Size = new System.Drawing.Size(285, 21);
            this.cbGeso.TabIndex = 18;
            this.uvProtocol.GetValidationSettings(this.cbGeso).IsRequired = true;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(622, 27);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 19);
            this.label9.TabIndex = 17;
            this.label9.Text = "GESO";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbTipoExamen
            // 
            this.cbTipoExamen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTipoExamen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTipoExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoExamen.FormattingEnabled = true;
            this.cbTipoExamen.Location = new System.Drawing.Point(112, 131);
            this.cbTipoExamen.Name = "cbTipoExamen";
            this.cbTipoExamen.Size = new System.Drawing.Size(163, 21);
            this.cbTipoExamen.TabIndex = 16;
            this.uvProtocol.GetValidationSettings(this.cbTipoExamen).IsRequired = true;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(10, 132);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 19);
            this.label5.TabIndex = 15;
            this.label5.Text = "Tipo de Examen";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(307, 132);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 19);
            this.label10.TabIndex = 15;
            this.label10.Text = "Consultorio";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbEmpresaEmpleadora
            // 
            this.cbEmpresaEmpleadora.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEmpresaEmpleadora.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEmpresaEmpleadora.DropDownWidth = 500;
            this.cbEmpresaEmpleadora.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEmpresaEmpleadora.FormattingEnabled = true;
            this.cbEmpresaEmpleadora.Location = new System.Drawing.Point(112, 78);
            this.cbEmpresaEmpleadora.Name = "cbEmpresaEmpleadora";
            this.cbEmpresaEmpleadora.Size = new System.Drawing.Size(437, 21);
            this.cbEmpresaEmpleadora.TabIndex = 14;
            this.uvProtocol.GetValidationSettings(this.cbEmpresaEmpleadora).IsRequired = true;
            this.cbEmpresaEmpleadora.SelectedIndexChanged += new System.EventHandler(this.cbEmpresaEmpleadora_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(8, 75);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 27);
            this.label11.TabIndex = 13;
            this.label11.Text = "Emp. Empleadora (Contratista)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNombreProtocolo
            // 
            this.txtNombreProtocolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreProtocolo.Location = new System.Drawing.Point(112, 25);
            this.txtNombreProtocolo.MaxLength = 100;
            this.txtNombreProtocolo.Name = "txtNombreProtocolo";
            this.txtNombreProtocolo.Size = new System.Drawing.Size(437, 20);
            this.txtNombreProtocolo.TabIndex = 11;
            this.uvProtocol.GetValidationSettings(this.txtNombreProtocolo).IsRequired = true;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(8, 25);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 21);
            this.label12.TabIndex = 12;
            this.label12.Text = "Nombre Proto.";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBedHospital
            // 
            this.lblBedHospital.BackColor = System.Drawing.Color.Transparent;
            this.lblBedHospital.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBedHospital.ForeColor = System.Drawing.Color.Black;
            this.lblBedHospital.Location = new System.Drawing.Point(772, 132);
            this.lblBedHospital.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBedHospital.Name = "lblBedHospital";
            this.lblBedHospital.Size = new System.Drawing.Size(71, 20);
            this.lblBedHospital.TabIndex = 23;
            this.lblBedHospital.Text = "Cama Hosp.";
            this.lblBedHospital.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBedHospital.Visible = false;
            // 
            // grdProtocolComponent
            // 
            this.grdProtocolComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProtocolComponent.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdProtocolComponent.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.Caption = "Componente";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 305;
            ultraGridColumn4.Header.Caption = "Precio";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.Header.Caption = "Operador";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 68;
            ultraGridColumn6.Header.Caption = "Edad";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 61;
            ultraGridColumn7.Header.Caption = "Género";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn8.Header.Caption = "Es Condic.";
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 75;
            ultraGridColumn9.Header.Caption = "Tipo";
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 99;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9});
            this.grdProtocolComponent.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdProtocolComponent.DisplayLayout.InterBandSpacing = 10;
            this.grdProtocolComponent.DisplayLayout.MaxColScrollRegions = 1;
            this.grdProtocolComponent.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdProtocolComponent.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdProtocolComponent.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdProtocolComponent.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdProtocolComponent.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdProtocolComponent.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdProtocolComponent.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdProtocolComponent.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdProtocolComponent.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdProtocolComponent.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdProtocolComponent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdProtocolComponent.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdProtocolComponent.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdProtocolComponent.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdProtocolComponent.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdProtocolComponent.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdProtocolComponent.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdProtocolComponent.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdProtocolComponent.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdProtocolComponent.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdProtocolComponent.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdProtocolComponent.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdProtocolComponent.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdProtocolComponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProtocolComponent.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdProtocolComponent.Location = new System.Drawing.Point(12, 209);
            this.grdProtocolComponent.Margin = new System.Windows.Forms.Padding(2);
            this.grdProtocolComponent.Name = "grdProtocolComponent";
            this.grdProtocolComponent.Size = new System.Drawing.Size(854, 311);
            this.grdProtocolComponent.TabIndex = 49;
            this.grdProtocolComponent.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdProtocolComponent_AfterSelectChange);
            // 
            // btnRemover
            // 
            this.btnRemover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemover.BackColor = System.Drawing.SystemColors.Control;
            this.btnRemover.Enabled = false;
            this.btnRemover.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRemover.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRemover.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRemover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemover.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemover.ForeColor = System.Drawing.Color.Black;
            this.btnRemover.Image = ((System.Drawing.Image)(resources.GetObject("btnRemover.Image")));
            this.btnRemover.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemover.Location = new System.Drawing.Point(923, 265);
            this.btnRemover.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(75, 24);
            this.btnRemover.TabIndex = 97;
            this.btnRemover.Text = "     Eliminar";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditar.Enabled = false;
            this.btnEditar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnEditar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.Black;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(923, 237);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 24);
            this.btnEditar.TabIndex = 96;
            this.btnEditar.Text = "      Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevo.BackColor = System.Drawing.SystemColors.Control;
            this.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnNuevo.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.Black;
            this.btnNuevo.Image = global::SAMBHS.Windows.WinClient.UI.Resource.add1;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(923, 209);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 24);
            this.btnNuevo.TabIndex = 95;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.SystemColors.Control;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Image = global::SAMBHS.Windows.WinClient.UI.Properties.Resources.system_save;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(923, 496);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 24);
            this.btnGuardar.TabIndex = 99;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(750, 549);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 20);
            this.label4.TabIndex = 104;
            this.label4.Text = "Vendedor";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Visible = false;
            // 
            // cboVendedor
            // 
            this.cboVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVendedor.FormattingEnabled = true;
            this.cboVendedor.Location = new System.Drawing.Point(815, 549);
            this.cboVendedor.Margin = new System.Windows.Forms.Padding(2);
            this.cboVendedor.Name = "cboVendedor";
            this.cboVendedor.Size = new System.Drawing.Size(47, 21);
            this.cboVendedor.TabIndex = 103;
            this.cboVendedor.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(636, 549);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 101;
            this.label2.Text = "% de Comisión";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Visible = false;
            // 
            // txtComision
            // 
            this.txtComision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComision.Enabled = false;
            this.txtComision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComision.Location = new System.Drawing.Point(584, 549);
            this.txtComision.MaxLength = 250;
            this.txtComision.Name = "txtComision";
            this.txtComision.Size = new System.Drawing.Size(47, 20);
            this.txtComision.TabIndex = 102;
            this.txtComision.Visible = false;
            // 
            // chkEsComisionable
            // 
            this.chkEsComisionable.AutoSize = true;
            this.chkEsComisionable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEsComisionable.ForeColor = System.Drawing.Color.Black;
            this.chkEsComisionable.Location = new System.Drawing.Point(470, 553);
            this.chkEsComisionable.Name = "chkEsComisionable";
            this.chkEsComisionable.Size = new System.Drawing.Size(108, 17);
            this.chkEsComisionable.TabIndex = 100;
            this.chkEsComisionable.Text = "Es comisionable?";
            this.chkEsComisionable.UseVisualStyleBackColor = true;
            this.chkEsComisionable.Visible = false;
            // 
            // frmProtocolEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 578);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboVendedor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtComision);
            this.Controls.Add(this.chkEsComisionable);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.grdProtocolComponent);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmProtocolEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Protocolo";
            this.Load += new System.EventHandler(this.frmProtocolEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProtocolComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvProtocol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.CheckBox chkEsActivo;
        private System.Windows.Forms.TextBox txtCamaHosp;
        private System.Windows.Forms.Button btnAgregarEmpresaContrata;
        private System.Windows.Forms.ComboBox cbEmpresaCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbServicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbTipoServicio;
        private System.Windows.Forms.TextBox txtEps;
        private System.Windows.Forms.TextBox txtFactor;
        private System.Windows.Forms.TextBox txtCentroCosto;
        private System.Windows.Forms.Label lblEps;
        private System.Windows.Forms.Label lblFactor;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbEmpresaTrabajo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbGeso;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbTipoExamen;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbEmpresaEmpleadora;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNombreProtocolo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblBedHospital;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdProtocolComponent;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboVendedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtComision;
        private System.Windows.Forms.CheckBox chkEsComisionable;
        private Infragistics.Win.Misc.UltraValidator uvProtocol;
        private System.Windows.Forms.ComboBox cbConsultorio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddConsultorio;
    }
}