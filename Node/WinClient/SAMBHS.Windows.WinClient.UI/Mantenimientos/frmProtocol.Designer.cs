namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    partial class frmProtocol
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtCaducidad = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.cboProtocolo = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtDescuentoId = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDescuentoId);
            this.groupBox2.Controls.Add(this.dtCaducidad);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnSalir);
            this.groupBox2.Controls.Add(this.btnAsignar);
            this.groupBox2.Controls.Add(this.cboProtocolo);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(621, 96);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos del Descuento";
            // 
            // dtCaducidad
            // 
            this.dtCaducidad.Checked = false;
            this.dtCaducidad.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCaducidad.Location = new System.Drawing.Point(101, 53);
            this.dtCaducidad.Name = "dtCaducidad";
            this.dtCaducidad.ShowCheckBox = true;
            this.dtCaducidad.Size = new System.Drawing.Size(126, 20);
            this.dtCaducidad.TabIndex = 141;
            this.dtCaducidad.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 140;
            this.label1.Text = "Fecha Caducidad";
            this.label1.Visible = false;
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::SAMBHS.Windows.WinClient.UI.Resource.cancel;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(503, 49);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(94, 32);
            this.btnSalir.TabIndex = 19;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Image = global::SAMBHS.Windows.WinClient.UI.Properties.Resources.database_yellow_start;
            this.btnAsignar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAsignar.Location = new System.Drawing.Point(403, 49);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(94, 32);
            this.btnAsignar.TabIndex = 18;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // cboProtocolo
            // 
            this.cboProtocolo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboProtocolo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboProtocolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProtocolo.FormattingEnabled = true;
            this.cboProtocolo.Location = new System.Drawing.Point(102, 18);
            this.cboProtocolo.Margin = new System.Windows.Forms.Padding(2);
            this.cboProtocolo.Name = "cboProtocolo";
            this.cboProtocolo.Size = new System.Drawing.Size(495, 21);
            this.cboProtocolo.TabIndex = 24;
            this.cboProtocolo.SelectedIndexChanged += new System.EventHandler(this.cboProtocolo_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(5, 22);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(85, 13);
            this.label20.TabIndex = 139;
            this.label20.Text = "Nombre del Plan";
            // 
            // txtDescuentoId
            // 
            this.txtDescuentoId.Location = new System.Drawing.Point(234, 52);
            this.txtDescuentoId.Name = "txtDescuentoId";
            this.txtDescuentoId.Size = new System.Drawing.Size(152, 20);
            this.txtDescuentoId.TabIndex = 142;
            this.txtDescuentoId.Visible = false;
            // 
            // frmProtocol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 119);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmProtocol";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar Descuento";
            this.Load += new System.EventHandler(this.frmProtocol_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboProtocolo;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DateTimePicker dtCaducidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescuentoId;
    }
}