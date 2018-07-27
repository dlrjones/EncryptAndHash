﻿namespace EncryptAndHash
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnFileDecrypt = new System.Windows.Forms.Button();
            this.btnFileEncrypt = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbString = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbHash = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tbHashCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnHash = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.cbDelete = new System.Windows.Forms.CheckBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbDelete);
            this.groupBox4.Controls.Add(this.btnFileDecrypt);
            this.groupBox4.Controls.Add(this.btnFileEncrypt);
            this.groupBox4.Controls.Add(this.btnBrowse);
            this.groupBox4.Controls.Add(this.tbFilePath);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.btnQuit);
            this.groupBox4.Controls.Add(this.tbKey);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.tbString);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.tbHash);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.pictureBox3);
            this.groupBox4.Controls.Add(this.tbHashCount);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.btnHash);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.btnDecrypt);
            this.groupBox4.Controls.Add(this.btnEncrypt);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(529, 377);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            // 
            // btnFileDecrypt
            // 
            this.btnFileDecrypt.Location = new System.Drawing.Point(90, 286);
            this.btnFileDecrypt.Name = "btnFileDecrypt";
            this.btnFileDecrypt.Size = new System.Drawing.Size(75, 28);
            this.btnFileDecrypt.TabIndex = 59;
            this.btnFileDecrypt.Text = "Decrypt";
            this.btnFileDecrypt.UseVisualStyleBackColor = true;
            this.btnFileDecrypt.Click += new System.EventHandler(this.btnFileDecrypt_Click);
            // 
            // btnFileEncrypt
            // 
            this.btnFileEncrypt.Location = new System.Drawing.Point(9, 287);
            this.btnFileEncrypt.Name = "btnFileEncrypt";
            this.btnFileEncrypt.Size = new System.Drawing.Size(75, 28);
            this.btnFileEncrypt.TabIndex = 58;
            this.btnFileEncrypt.Text = "Encrypt";
            this.btnFileEncrypt.UseVisualStyleBackColor = true;
            this.btnFileEncrypt.Click += new System.EventHandler(this.btnFileEncrypt_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(435, 257);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 57;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(6, 258);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(423, 22);
            this.tbFilePath.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 238);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(388, 17);
            this.label6.TabIndex = 56;
            this.label6.Text = "Path to File To Encrypt    (or where to put the Decrypted file)";
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(435, 327);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 28);
            this.btnQuit.TabIndex = 20;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // tbKey
            // 
            this.tbKey.BackColor = System.Drawing.SystemColors.Window;
            this.tbKey.Location = new System.Drawing.Point(9, 93);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(488, 22);
            this.tbKey.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(439, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Text to Encrypt or Hash        (or the plain text result from Decryption)";
            // 
            // tbString
            // 
            this.tbString.Location = new System.Drawing.Point(12, 38);
            this.tbString.Name = "tbString";
            this.tbString.Size = new System.Drawing.Size(488, 22);
            this.tbString.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 54;
            this.label3.Text = "Key (optional)";
            // 
            // tbHash
            // 
            this.tbHash.Location = new System.Drawing.Point(9, 145);
            this.tbHash.Name = "tbHash";
            this.tbHash.Size = new System.Drawing.Size(488, 22);
            this.tbHash.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Encrypted Text";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(183, 193);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(26, 27);
            this.pictureBox3.TabIndex = 53;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // tbHashCount
            // 
            this.tbHashCount.Location = new System.Drawing.Point(303, 200);
            this.tbHashCount.Name = "tbHashCount";
            this.tbHashCount.Size = new System.Drawing.Size(42, 22);
            this.tbHashCount.TabIndex = 13;
            this.tbHashCount.Text = "500";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Time(s)";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(435, 193);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 28);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnHash
            // 
            this.btnHash.Location = new System.Drawing.Point(222, 194);
            this.btnHash.Name = "btnHash";
            this.btnHash.Size = new System.Drawing.Size(75, 28);
            this.btnHash.TabIndex = 11;
            this.btnHash.Text = "Hash";
            this.btnHash.UseVisualStyleBackColor = true;
            this.btnHash.Click += new System.EventHandler(this.btnHash_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-4, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 17);
            this.label4.TabIndex = 4;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(90, 193);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 28);
            this.btnDecrypt.TabIndex = 8;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(9, 194);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 28);
            this.btnEncrypt.TabIndex = 6;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // cbDelete
            // 
            this.cbDelete.AutoSize = true;
            this.cbDelete.Location = new System.Drawing.Point(9, 321);
            this.cbDelete.Name = "cbDelete";
            this.cbDelete.Size = new System.Drawing.Size(146, 21);
            this.cbDelete.TabIndex = 60;
            this.cbDelete.Text = "Delete Source File";
            this.cbDelete.UseVisualStyleBackColor = true;
            this.cbDelete.CheckedChanged += new System.EventHandler(this.cbDelete_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 553);
            this.Controls.Add(this.groupBox4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(574, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(574, 357);
            this.Name = "Form1";
            this.Text = "Encrypt&Hash";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnHash;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbHash;
        private System.Windows.Forms.TextBox tbString;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbHashCount;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnFileDecrypt;
        private System.Windows.Forms.Button btnFileEncrypt;
        private System.Windows.Forms.CheckBox cbDelete;
    }
}

