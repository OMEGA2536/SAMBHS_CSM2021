namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    partial class frmPopUp_MedicoTratante
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
            this.cboMedicoTratante = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboMedicoTratante
            // 
            this.cboMedicoTratante.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboMedicoTratante.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboMedicoTratante.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMedicoTratante.FormattingEnabled = true;
            this.cboMedicoTratante.Location = new System.Drawing.Point(100, 12);
            this.cboMedicoTratante.Margin = new System.Windows.Forms.Padding(2);
            this.cboMedicoTratante.Name = "cboMedicoTratante";
            this.cboMedicoTratante.Size = new System.Drawing.Size(222, 21);
            this.cboMedicoTratante.TabIndex = 162;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(5, 15);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 13);
            this.label19.TabIndex = 161;
            this.label19.Text = "Personal Tratante";
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(174, 50);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(75, 23);
            this.btnAsignar.TabIndex = 163;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // frmPopUp_MedicoTratante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 83);
            this.ControlBox = false;
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.cboMedicoTratante);
            this.Controls.Add(this.label19);
            this.Name = "frmPopUp_MedicoTratante";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Load += new System.EventHandler(this.frmPopUp_MedicoTratante_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboMedicoTratante;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnAsignar;
    }
}