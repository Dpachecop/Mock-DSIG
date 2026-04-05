namespace Mock_DSIG
{
    partial class EditarUsuarioAdmin
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.form_apellidoEdit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.form_numero_tlfEdit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.form_idEdit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.combo_tipo_docEdit = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.form_correoEdit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.form_nombresEdit = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.form_password_final = new System.Windows.Forms.TextBox();
            this.form_password_inic = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bt_guardarEdit = new System.Windows.Forms.Button();
            this.bt_cancelareDIT = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(79)))), ((int)(((byte)(124)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(-2, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 63);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Editar Investigador";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.form_apellidoEdit);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.form_numero_tlfEdit);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.form_idEdit);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.combo_tipo_docEdit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.form_correoEdit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.form_nombresEdit);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(79)))), ((int)(((byte)(124)))));
            this.groupBox1.Location = new System.Drawing.Point(22, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 354);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Personales";
            // 
            // form_apellidoEdit
            // 
            this.form_apellidoEdit.Location = new System.Drawing.Point(302, 67);
            this.form_apellidoEdit.Multiline = true;
            this.form_apellidoEdit.Name = "form_apellidoEdit";
            this.form_apellidoEdit.Size = new System.Drawing.Size(212, 34);
            this.form_apellidoEdit.TabIndex = 17;
            this.form_apellidoEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.form_apellidoEdit_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(299, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Apellidos";
            // 
            // form_numero_tlfEdit
            // 
            this.form_numero_tlfEdit.Location = new System.Drawing.Point(24, 214);
            this.form_numero_tlfEdit.Multiline = true;
            this.form_numero_tlfEdit.Name = "form_numero_tlfEdit";
            this.form_numero_tlfEdit.Size = new System.Drawing.Size(490, 36);
            this.form_numero_tlfEdit.TabIndex = 15;
            this.form_numero_tlfEdit.TextChanged += new System.EventHandler(this.form_numero_tlfEdit_TextChanged);
            this.form_numero_tlfEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.form_numero_tlfEdit_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(22, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 16);
            this.label10.TabIndex = 14;
            this.label10.Text = "Número de Telefono";
            // 
            // form_idEdit
            // 
            this.form_idEdit.Location = new System.Drawing.Point(268, 306);
            this.form_idEdit.Multiline = true;
            this.form_idEdit.Name = "form_idEdit";
            this.form_idEdit.ReadOnly = true;
            this.form_idEdit.Size = new System.Drawing.Size(247, 24);
            this.form_idEdit.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(265, 286);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Número de Documento";
            // 
            // combo_tipo_docEdit
            // 
            this.combo_tipo_docEdit.FormattingEnabled = true;
            this.combo_tipo_docEdit.Items.AddRange(new object[] {
            "CEDULA CIUDADANIA",
            "CEDULA EXTRANJERA",
            "TARJETA IDENTIDAD"});
            this.combo_tipo_docEdit.Location = new System.Drawing.Point(24, 306);
            this.combo_tipo_docEdit.Name = "combo_tipo_docEdit";
            this.combo_tipo_docEdit.Size = new System.Drawing.Size(204, 24);
            this.combo_tipo_docEdit.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(21, 286);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tipo de Documento";
            // 
            // form_correoEdit
            // 
            this.form_correoEdit.Location = new System.Drawing.Point(24, 135);
            this.form_correoEdit.Multiline = true;
            this.form_correoEdit.Name = "form_correoEdit";
            this.form_correoEdit.Size = new System.Drawing.Size(490, 36);
            this.form_correoEdit.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(22, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Correo Electrónico";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(21, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nombres";
            // 
            // form_nombresEdit
            // 
            this.form_nombresEdit.Location = new System.Drawing.Point(24, 67);
            this.form_nombresEdit.Multiline = true;
            this.form_nombresEdit.Name = "form_nombresEdit";
            this.form_nombresEdit.Size = new System.Drawing.Size(220, 34);
            this.form_nombresEdit.TabIndex = 6;
            this.form_nombresEdit.TextChanged += new System.EventHandler(this.form_nombresEdit_TextChanged);
            this.form_nombresEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.form_nombresEdit_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.form_password_final);
            this.groupBox3.Controls.Add(this.form_password_inic);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(79)))), ((int)(((byte)(124)))));
            this.groupBox3.Location = new System.Drawing.Point(22, 463);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(537, 97);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Acceso al Sistema";
            // 
            // form_password_final
            // 
            this.form_password_final.Location = new System.Drawing.Point(294, 55);
            this.form_password_final.Multiline = true;
            this.form_password_final.Name = "form_password_final";
            this.form_password_final.PasswordChar = '*';
            this.form_password_final.Size = new System.Drawing.Size(220, 25);
            this.form_password_final.TabIndex = 17;
            // 
            // form_password_inic
            // 
            this.form_password_inic.Location = new System.Drawing.Point(24, 55);
            this.form_password_inic.Multiline = true;
            this.form_password_inic.Name = "form_password_inic";
            this.form_password_inic.PasswordChar = '*';
            this.form_password_inic.Size = new System.Drawing.Size(220, 25);
            this.form_password_inic.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(292, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(145, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "Confirme su Contraseña";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(22, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "Contraseña";
            // 
            // bt_guardarEdit
            // 
            this.bt_guardarEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(122)))), ((int)(((byte)(74)))));
            this.bt_guardarEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_guardarEdit.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_guardarEdit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bt_guardarEdit.Location = new System.Drawing.Point(364, 582);
            this.bt_guardarEdit.Name = "bt_guardarEdit";
            this.bt_guardarEdit.Size = new System.Drawing.Size(198, 40);
            this.bt_guardarEdit.TabIndex = 19;
            this.bt_guardarEdit.Text = "ACTUALIZAR INFORMACIÓN";
            this.bt_guardarEdit.UseVisualStyleBackColor = false;
            this.bt_guardarEdit.Click += new System.EventHandler(this.bt_guardarEdit_Click);
            // 
            // bt_cancelareDIT
            // 
            this.bt_cancelareDIT.BackColor = System.Drawing.Color.White;
            this.bt_cancelareDIT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_cancelareDIT.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_cancelareDIT.ForeColor = System.Drawing.Color.Black;
            this.bt_cancelareDIT.Location = new System.Drawing.Point(236, 582);
            this.bt_cancelareDIT.Name = "bt_cancelareDIT";
            this.bt_cancelareDIT.Size = new System.Drawing.Size(114, 40);
            this.bt_cancelareDIT.TabIndex = 20;
            this.bt_cancelareDIT.Text = "CANCELAR";
            this.bt_cancelareDIT.UseVisualStyleBackColor = false;
            this.bt_cancelareDIT.Click += new System.EventHandler(this.bt_cancelareDIT_Click);
            // 
            // EditarUsuarioAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 662);
            this.ControlBox = false;
            this.Controls.Add(this.bt_cancelareDIT);
            this.Controls.Add(this.bt_guardarEdit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "EditarUsuarioAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EDITAR USUARIO - ADMINISTRADOR";
            this.Load += new System.EventHandler(this.EditarUsuarioAdmin_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditarUsuarioAdmin_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox form_apellidoEdit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox form_numero_tlfEdit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox form_idEdit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combo_tipo_docEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox form_correoEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox form_nombresEdit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox form_password_final;
        private System.Windows.Forms.TextBox form_password_inic;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bt_guardarEdit;
        private System.Windows.Forms.Button bt_cancelareDIT;
    }
}