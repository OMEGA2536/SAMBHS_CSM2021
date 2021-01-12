namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    partial class frmSolicitud
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtParent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkHistoria = new System.Windows.Forms.CheckBox();
            this.chkExamenes = new System.Windows.Forms.CheckBox();
            this.chkCertificado = new System.Windows.Forms.CheckBox();
            this.chkInforme = new System.Windows.Forms.CheckBox();
            this.chkOtro = new System.Windows.Forms.CheckBox();
            this.txtOtro = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtProposito = new System.Windows.Forms.TextBox();
            this.dtpServiceDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHistoria = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDomicilio = new System.Windows.Forms.Label();
            this.lblDistrito = new System.Windows.Forms.Label();
            this.lblDepartamento = new System.Windows.Forms.Label();
            this.lblEdad = new System.Windows.Forms.Label();
            this.lblDNI = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nro. DNI";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(71, 9);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(100, 20);
            this.txtDNI.TabIndex = 1;
            this.txtDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDNI_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nombre y apellidos";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(286, 9);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(293, 20);
            this.txtName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Parentesco";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtParent
            // 
            this.txtParent.Location = new System.Drawing.Point(71, 38);
            this.txtParent.Name = "txtParent";
            this.txtParent.Size = new System.Drawing.Size(100, 20);
            this.txtParent.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(180, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Teléfono";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(238, 38);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(100, 20);
            this.txtPhone.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(349, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "E-mail";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(390, 38);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(189, 20);
            this.txtEmail.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkOtro);
            this.groupBox1.Controls.Add(this.txtOtro);
            this.groupBox1.Controls.Add(this.chkInforme);
            this.groupBox1.Controls.Add(this.chkCertificado);
            this.groupBox1.Controls.Add(this.chkExamenes);
            this.groupBox1.Controls.Add(this.chkHistoria);
            this.groupBox1.Location = new System.Drawing.Point(13, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(566, 89);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SOLICITUD";
            // 
            // chkHistoria
            // 
            this.chkHistoria.AutoSize = true;
            this.chkHistoria.Location = new System.Drawing.Point(24, 20);
            this.chkHistoria.Name = "chkHistoria";
            this.chkHistoria.Size = new System.Drawing.Size(127, 17);
            this.chkHistoria.TabIndex = 0;
            this.chkHistoria.Text = "Copia Historia Clínica";
            this.chkHistoria.UseVisualStyleBackColor = true;
            // 
            // chkExamenes
            // 
            this.chkExamenes.AutoSize = true;
            this.chkExamenes.Location = new System.Drawing.Point(191, 20);
            this.chkExamenes.Name = "chkExamenes";
            this.chkExamenes.Size = new System.Drawing.Size(165, 17);
            this.chkExamenes.TabIndex = 0;
            this.chkExamenes.Text = "Copia de exámenes auxiliares";
            this.chkExamenes.UseVisualStyleBackColor = true;
            // 
            // chkCertificado
            // 
            this.chkCertificado.AutoSize = true;
            this.chkCertificado.Location = new System.Drawing.Point(409, 20);
            this.chkCertificado.Name = "chkCertificado";
            this.chkCertificado.Size = new System.Drawing.Size(135, 17);
            this.chkCertificado.TabIndex = 0;
            this.chkCertificado.Text = "Certificado de atención";
            this.chkCertificado.UseVisualStyleBackColor = true;
            // 
            // chkInforme
            // 
            this.chkInforme.AutoSize = true;
            this.chkInforme.Location = new System.Drawing.Point(24, 58);
            this.chkInforme.Name = "chkInforme";
            this.chkInforme.Size = new System.Drawing.Size(99, 17);
            this.chkInforme.TabIndex = 0;
            this.chkInforme.Text = "Informe Medico";
            this.chkInforme.UseVisualStyleBackColor = true;
            // 
            // chkOtro
            // 
            this.chkOtro.AutoSize = true;
            this.chkOtro.Location = new System.Drawing.Point(191, 58);
            this.chkOtro.Name = "chkOtro";
            this.chkOtro.Size = new System.Drawing.Size(46, 17);
            this.chkOtro.TabIndex = 0;
            this.chkOtro.Text = "Otro";
            this.chkOtro.UseVisualStyleBackColor = true;
            // 
            // txtOtro
            // 
            this.txtOtro.Location = new System.Drawing.Point(263, 56);
            this.txtOtro.Name = "txtOtro";
            this.txtOtro.Size = new System.Drawing.Size(281, 20);
            this.txtOtro.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Propósito de la solicitud: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProposito
            // 
            this.txtProposito.Location = new System.Drawing.Point(136, 168);
            this.txtProposito.Name = "txtProposito";
            this.txtProposito.Size = new System.Drawing.Size(443, 20);
            this.txtProposito.TabIndex = 1;
            // 
            // dtpServiceDate
            // 
            this.dtpServiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpServiceDate.Location = new System.Drawing.Point(136, 198);
            this.dtpServiceDate.Name = "dtpServiceDate";
            this.dtpServiceDate.Size = new System.Drawing.Size(96, 20);
            this.dtpServiceDate.TabIndex = 3;
            this.dtpServiceDate.ValueChanged += new System.EventHandler(this.dtpServiceDate_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Fecha de la atención:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(283, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Nro de Historia";
            // 
            // txtHistoria
            // 
            this.txtHistoria.Location = new System.Drawing.Point(366, 198);
            this.txtHistoria.Name = "txtHistoria";
            this.txtHistoria.Size = new System.Drawing.Size(213, 20);
            this.txtHistoria.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lblDNI);
            this.groupBox2.Controls.Add(this.lblEdad);
            this.groupBox2.Controls.Add(this.lblDepartamento);
            this.groupBox2.Controls.Add(this.lblDistrito);
            this.groupBox2.Controls.Add(this.lblDomicilio);
            this.groupBox2.Controls.Add(this.lblNombre);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(13, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(566, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos del paciente";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Nombres y apellidos ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(424, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Nro. DNI";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(441, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Edad";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(63, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Domicilio";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(73, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Distrito";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(267, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Departemento";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Image = global::SAMBHS.Windows.WinClient.UI.Resource.analytics;
            this.btnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerar.Location = new System.Drawing.Point(457, 336);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(121, 24);
            this.btnGenerar.TabIndex = 5;
            this.btnGenerar.Text = "Generar Solicitud";
            this.btnGenerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::SAMBHS.Windows.WinClient.UI.Resource.cancel;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(328, 336);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(121, 24);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(120, 21);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(39, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Name";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDomicilio
            // 
            this.lblDomicilio.AutoSize = true;
            this.lblDomicilio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDomicilio.Location = new System.Drawing.Point(118, 47);
            this.lblDomicilio.Name = "lblDomicilio";
            this.lblDomicilio.Size = new System.Drawing.Size(45, 13);
            this.lblDomicilio.TabIndex = 0;
            this.lblDomicilio.Text = "Adress";
            this.lblDomicilio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDistrito
            // 
            this.lblDistrito.AutoSize = true;
            this.lblDistrito.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistrito.Location = new System.Drawing.Point(118, 72);
            this.lblDistrito.Name = "lblDistrito";
            this.lblDistrito.Size = new System.Drawing.Size(43, 13);
            this.lblDistrito.TabIndex = 0;
            this.lblDistrito.Text = "Distric";
            this.lblDistrito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDepartamento
            // 
            this.lblDepartamento.AutoSize = true;
            this.lblDepartamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartamento.Location = new System.Drawing.Point(350, 72);
            this.lblDepartamento.Name = "lblDepartamento";
            this.lblDepartamento.Size = new System.Drawing.Size(28, 13);
            this.lblDepartamento.TabIndex = 0;
            this.lblDepartamento.Text = "City";
            this.lblDepartamento.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEdad.Location = new System.Drawing.Point(479, 47);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(29, 13);
            this.lblEdad.TabIndex = 0;
            this.lblEdad.Text = "Age";
            this.lblEdad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI.Location = new System.Drawing.Point(479, 21);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(30, 13);
            this.lblDNI.TabIndex = 0;
            this.lblDNI.Text = "Doc";
            this.lblDNI.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmSolicitud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 374);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dtpServiceDate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtProposito);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHistoria);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtParent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.label1);
            this.Name = "frmSolicitud";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario de Solicitud";
            this.Load += new System.EventHandler(this.frmSolicitud_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtParent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkOtro;
        private System.Windows.Forms.TextBox txtOtro;
        private System.Windows.Forms.CheckBox chkInforme;
        private System.Windows.Forms.CheckBox chkCertificado;
        private System.Windows.Forms.CheckBox chkExamenes;
        private System.Windows.Forms.CheckBox chkHistoria;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProposito;
        private System.Windows.Forms.DateTimePicker dtpServiceDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtHistoria;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label lblDepartamento;
        private System.Windows.Forms.Label lblDistrito;
        private System.Windows.Forms.Label lblDomicilio;
        private System.Windows.Forms.Label lblNombre;
    }
}