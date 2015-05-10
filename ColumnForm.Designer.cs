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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vertical Column found.\r\nHow do you want to handle\r\nthe vertical columns?";
            // 
            // button_Vertical
            // 
            this.button_Vertical.Location = new System.Drawing.Point(12, 57);
            this.button_Vertical.Name = "button_Vertical";
            this.button_Vertical.Size = new System.Drawing.Size(64, 55);
            this.button_Vertical.TabIndex = 1;
            this.button_Vertical.Text = "Break at clostest level";
            this.button_Vertical.UseVisualStyleBackColor = true;
            this.button_Vertical.Click += new System.EventHandler(this.button_Vertical_Click);
            // 
            // button_Slanted
            // 
            this.button_Slanted.Location = new System.Drawing.Point(84, 57);
            this.button_Slanted.Name = "button_Slanted";
            this.button_Slanted.Size = new System.Drawing.Size(64, 55);
            this.button_Slanted.TabIndex = 2;
            this.button_Slanted.Text = "Change to Slanted Column";
            this.button_Slanted.UseVisualStyleBackColor = true;
            this.button_Slanted.Click += new System.EventHandler(this.button_Slanted_Click);
            // 
            // Column
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 125);
            this.Controls.Add(this.button_Slanted);
            this.Controls.Add(this.button_Vertical);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Column";
            this.ShowInTaskbar = false;
            this.Text = "Vertical Column";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Vertical;
        private System.Windows.Forms.Button button_Slanted;
    }
}