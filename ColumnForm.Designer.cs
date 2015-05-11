namespace EditElements
{
    partial class ColumnForm
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
            this.button_Vertical = new System.Windows.Forms.Button();
            this.button_Slanted = new System.Windows.Forms.Button();
            this.checkBox_RememberColumns = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vertical Column found.\r\nHow do you want to\r\nhandle the vertical columns?";
            // 
            // button_Vertical
            // 
            this.button_Vertical.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_Vertical.Location = new System.Drawing.Point(12, 83);
            this.button_Vertical.Name = "button_Vertical";
            this.button_Vertical.Size = new System.Drawing.Size(210, 33);
            this.button_Vertical.TabIndex = 1;
            this.button_Vertical.Text = "Break at clostest level";
            this.button_Vertical.UseVisualStyleBackColor = false;
            this.button_Vertical.Click += new System.EventHandler(this.button_Vertical_Click);
            // 
            // button_Slanted
            // 
            this.button_Slanted.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_Slanted.Location = new System.Drawing.Point(12, 122);
            this.button_Slanted.Name = "button_Slanted";
            this.button_Slanted.Size = new System.Drawing.Size(210, 33);
            this.button_Slanted.TabIndex = 2;
            this.button_Slanted.Text = "Change to Slanted Column";
            this.button_Slanted.UseVisualStyleBackColor = false;
            this.button_Slanted.Click += new System.EventHandler(this.button_Slanted_Click);
            // 
            // checkBox_RememberColumns
            // 
            this.checkBox_RememberColumns.AutoSize = true;
            this.checkBox_RememberColumns.Checked = true;
            this.checkBox_RememberColumns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_RememberColumns.Location = new System.Drawing.Point(85, 56);
            this.checkBox_RememberColumns.Name = "checkBox_RememberColumns";
            this.checkBox_RememberColumns.Size = new System.Drawing.Size(130, 17);
            this.checkBox_RememberColumns.TabIndex = 3;
            this.checkBox_RememberColumns.Text = "Remember this setting";
            this.checkBox_RememberColumns.UseVisualStyleBackColor = true;
            this.checkBox_RememberColumns.CheckedChanged += new System.EventHandler(this.checkBox_RememberColumns_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EditElements.Properties.Resources.QuestionMark;
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // ColumnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(235, 165);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBox_RememberColumns);
            this.Controls.Add(this.button_Slanted);
            this.Controls.Add(this.button_Vertical);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColumnForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vertical Column";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Vertical;
        private System.Windows.Forms.Button button_Slanted;
        private System.Windows.Forms.CheckBox checkBox_RememberColumns;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}