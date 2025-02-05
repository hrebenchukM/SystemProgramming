namespace SystemProgramming
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            progressBar1 = new ProgressBar();
            progressBar2 = new ProgressBar();
            progressBar3 = new ProgressBar();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox5 = new CheckBox();
            checkBox6 = new CheckBox();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(54, 47);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(459, 61);
            progressBar1.TabIndex = 0;
            // 
            // progressBar2
            // 
            progressBar2.Location = new Point(54, 171);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(459, 61);
            progressBar2.TabIndex = 1;
            // 
            // progressBar3
            // 
            progressBar3.Location = new Point(54, 293);
            progressBar3.Name = "progressBar3";
            progressBar3.Size = new Size(459, 61);
            progressBar3.TabIndex = 2;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(54, 124);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(137, 19);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Запустить 1-й поток";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(360, 124);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(167, 19);
            checkBox2.TabIndex = 4;
            checkBox2.Text = "Приостановить 1-й поток";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(54, 249);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(137, 19);
            checkBox3.TabIndex = 5;
            checkBox3.Text = "Запустить 2-й поток";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(360, 249);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(167, 19);
            checkBox4.TabIndex = 6;
            checkBox4.Text = "Приостановить 2-й поток";
            checkBox4.UseVisualStyleBackColor = true;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(54, 378);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(137, 19);
            checkBox5.TabIndex = 7;
            checkBox5.Text = "Запустить 3-й поток";
            checkBox5.UseVisualStyleBackColor = true;
            checkBox5.CheckedChanged += checkBox5_CheckedChanged;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(360, 378);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(167, 19);
            checkBox6.TabIndex = 8;
            checkBox6.Text = "Приостановить 3-й поток";
            checkBox6.UseVisualStyleBackColor = true;
            checkBox6.CheckedChanged += checkBox6_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(569, 450);
            Controls.Add(checkBox6);
            Controls.Add(checkBox5);
            Controls.Add(checkBox4);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(progressBar3);
            Controls.Add(progressBar2);
            Controls.Add(progressBar1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Работа с потоками";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBar1;
        private ProgressBar progressBar2;
        private ProgressBar progressBar3;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox checkBox5;
        private CheckBox checkBox6;
    }
}
