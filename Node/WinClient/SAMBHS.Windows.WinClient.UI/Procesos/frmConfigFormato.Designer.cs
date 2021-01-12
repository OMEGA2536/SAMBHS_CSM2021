namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    partial class frmConfigFormato
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
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            this.cboFormato = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.chkEnviarANubefact = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.chkAutoEnvio = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.btnAceptar = new Infragistics.Win.Misc.UltraButton();
            this.txtToken = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.txtRuta = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            ((System.ComponentModel.ISupportInitialize)(this.cboFormato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnviarANubefact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoEnvio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToken)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuta)).BeginInit();
            this.SuspendLayout();
            // 
            // cboFormato
            // 
            this.cboFormato.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            valueListItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            valueListItem1.DataValue = "A4";
            valueListItem1.DisplayText = "A4";
            valueListItem2.DataValue = "A5";
            valueListItem2.DisplayText = "A5";
            valueListItem3.DataValue = "TICKET";
            valueListItem3.DisplayText = "TICKET";
            this.cboFormato.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2,
            valueListItem3});
            this.cboFormato.Location = new System.Drawing.Point(77, 69);
            this.cboFormato.Margin = new System.Windows.Forms.Padding(2);
            this.cboFormato.Name = "cboFormato";
            this.cboFormato.Size = new System.Drawing.Size(148, 21);
            this.cboFormato.TabIndex = 7;
            // 
            // chkEnviarANubefact
            // 
            this.chkEnviarANubefact.Location = new System.Drawing.Point(267, 91);
            this.chkEnviarANubefact.Margin = new System.Windows.Forms.Padding(2);
            this.chkEnviarANubefact.Name = "chkEnviarANubefact";
            this.chkEnviarANubefact.Size = new System.Drawing.Size(215, 16);
            this.chkEnviarANubefact.TabIndex = 15;
            this.chkEnviarANubefact.Text = "Enviar automaticamente a Nube";
            // 
            // chkAutoEnvio
            // 
            this.chkAutoEnvio.Location = new System.Drawing.Point(267, 69);
            this.chkAutoEnvio.Margin = new System.Windows.Forms.Padding(2);
            this.chkAutoEnvio.Name = "chkAutoEnvio";
            this.chkAutoEnvio.Size = new System.Drawing.Size(215, 16);
            this.chkAutoEnvio.TabIndex = 14;
            this.chkAutoEnvio.Text = "Enviar automaticamente a SUNAT";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAceptar.Location = new System.Drawing.Point(77, 100);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(103, 23);
            this.btnAceptar.TabIndex = 13;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtToken
            // 
            this.txtToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToken.Location = new System.Drawing.Point(77, 40);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(383, 21);
            this.txtToken.TabIndex = 12;
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.AutoSize = true;
            this.ultraLabel2.Location = new System.Drawing.Point(26, 44);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(39, 14);
            this.ultraLabel2.TabIndex = 11;
            this.ultraLabel2.Text = "Token:";
            // 
            // txtRuta
            // 
            this.txtRuta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRuta.Location = new System.Drawing.Point(77, 10);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(383, 21);
            this.txtRuta.TabIndex = 10;
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.AutoSize = true;
            this.ultraLabel1.Location = new System.Drawing.Point(26, 14);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(31, 14);
            this.ultraLabel1.TabIndex = 9;
            this.ultraLabel1.Text = "Ruta:";
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.AutoSize = true;
            this.ultraLabel3.Location = new System.Drawing.Point(16, 73);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(49, 14);
            this.ultraLabel3.TabIndex = 16;
            this.ultraLabel3.Text = "Formato:";
            // 
            // frmConfigFormato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 135);
            this.Controls.Add(this.ultraLabel3);
            this.Controls.Add(this.chkEnviarANubefact);
            this.Controls.Add(this.chkAutoEnvio);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.ultraLabel2);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.ultraLabel1);
            this.Controls.Add(this.cboFormato);
            this.Name = "frmConfigFormato";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración comprobantes electrónicos";
            this.Load += new System.EventHandler(this.frmConfigFormato_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboFormato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnviarANubefact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoEnvio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToken)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinEditors.UltraComboEditor cboFormato;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor chkEnviarANubefact;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor chkAutoEnvio;
        private Infragistics.Win.Misc.UltraButton btnAceptar;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtToken;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtRuta;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
    }
}