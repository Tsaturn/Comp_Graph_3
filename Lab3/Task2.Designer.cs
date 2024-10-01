namespace Lab3
{
    partial class Task2
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBoxBresenham = new System.Windows.Forms.CheckBox();
            this.checkBoxWu = new System.Windows.Forms.CheckBox();
            this.clearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(225, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(900, 623);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // checkBoxBresenham
            // 
            this.checkBoxBresenham.AutoSize = true;
            this.checkBoxBresenham.Location = new System.Drawing.Point(14, 77);
            this.checkBoxBresenham.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxBresenham.Name = "checkBoxBresenham";
            this.checkBoxBresenham.Size = new System.Drawing.Size(205, 24);
            this.checkBoxBresenham.TabIndex = 1;
            this.checkBoxBresenham.Text = "Алгоритм Брезенхема";
            this.checkBoxBresenham.UseVisualStyleBackColor = true;
            this.checkBoxBresenham.CheckedChanged += new System.EventHandler(this.checkBoxBresenham_CheckedChanged);
            // 
            // checkBoxWu
            // 
            this.checkBoxWu.AutoSize = true;
            this.checkBoxWu.Location = new System.Drawing.Point(14, 119);
            this.checkBoxWu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxWu.Name = "checkBoxWu";
            this.checkBoxWu.Size = new System.Drawing.Size(132, 24);
            this.checkBoxWu.TabIndex = 2;
            this.checkBoxWu.Text = "Алгоритм Ву";
            this.checkBoxWu.UseVisualStyleBackColor = true;
            this.checkBoxWu.CheckedChanged += new System.EventHandler(this.checkBoxWu_CheckedChanged);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(14, 13);
            this.clearButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(112, 38);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Очистить";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Task2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 649);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.checkBoxWu);
            this.Controls.Add(this.checkBoxBresenham);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Task2";
            this.Text = "Отрисовка отрезков";
            this.Load += new System.EventHandler(this.Task2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private System.Windows.Forms.CheckBox checkBoxBresenham;
        private System.Windows.Forms.CheckBox checkBoxWu;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
