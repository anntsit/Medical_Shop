namespace MedicaleShop.Forms
{
    partial class Login
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.chbEnter = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.registerForm = new System.Windows.Forms.Button();
            this.accept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chbEnter
            // 
            this.chbEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chbEnter.AutoSize = true;
            this.chbEnter.BackColor = System.Drawing.Color.Transparent;
            this.chbEnter.Font = new System.Drawing.Font("Trebuchet MS", 11.25F);
            this.chbEnter.ForeColor = System.Drawing.Color.Black;
            this.chbEnter.Location = new System.Drawing.Point(74, 109);
            this.chbEnter.Name = "chbEnter";
            this.chbEnter.Size = new System.Drawing.Size(111, 24);
            this.chbEnter.TabIndex = 35;
            this.chbEnter.Text = "Запам\'ятати";
            this.chbEnter.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(69, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 34;
            this.label1.Text = "Пароль";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(69, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "Логін";
            // 
            // tbPass
            // 
            this.tbPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPass.BackColor = System.Drawing.Color.LightGray;
            this.tbPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPass.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPass.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbPass.Location = new System.Drawing.Point(72, 82);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '*';
            this.tbPass.Size = new System.Drawing.Size(156, 23);
            this.tbPass.TabIndex = 30;
            // 
            // tbLogin
            // 
            this.tbLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogin.BackColor = System.Drawing.Color.LightGray;
            this.tbLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLogin.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbLogin.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbLogin.Location = new System.Drawing.Point(72, 35);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(156, 23);
            this.tbLogin.TabIndex = 29;
            // 
            // registerForm
            // 
            this.registerForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.registerForm.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.registerForm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(57)))), ((int)(((byte)(61)))));
            this.registerForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(57)))), ((int)(((byte)(61)))));
            this.registerForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(57)))), ((int)(((byte)(61)))));
            this.registerForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(57)))), ((int)(((byte)(61)))));
            this.registerForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registerForm.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Italic);
            this.registerForm.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.registerForm.Image = global::MedicaleShop.Properties.Resources.registr;
            this.registerForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.registerForm.Location = new System.Drawing.Point(72, 191);
            this.registerForm.Name = "registerForm";
            this.registerForm.Size = new System.Drawing.Size(156, 44);
            this.registerForm.TabIndex = 32;
            this.registerForm.Text = "       Реєстрація";
            this.registerForm.UseVisualStyleBackColor = false;
            this.registerForm.Click += new System.EventHandler(this.registerForm_Click);
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.accept.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(57)))), ((int)(((byte)(61)))));
            this.accept.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(57)))), ((int)(((byte)(61)))));
            this.accept.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(57)))), ((int)(((byte)(61)))));
            this.accept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(57)))), ((int)(((byte)(61)))));
            this.accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.accept.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Italic);
            this.accept.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.accept.Image = global::MedicaleShop.Properties.Resources.sign_in;
            this.accept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.accept.Location = new System.Drawing.Point(72, 141);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(156, 44);
            this.accept.TabIndex = 31;
            this.accept.Text = "Увійти";
            this.accept.UseVisualStyleBackColor = false;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(296, 263);
            this.Controls.Add(this.chbEnter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.registerForm);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.tbLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(312, 301);
            this.MinimumSize = new System.Drawing.Size(312, 301);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вхід";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbEnter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button registerForm;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.TextBox tbLogin;
    }
}

