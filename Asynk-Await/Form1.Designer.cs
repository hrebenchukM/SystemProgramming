namespace Asynk_Await
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
            FileButtton = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            textBox2 = new TextBox();
            progressBar1 = new ProgressBar();
            radioButtonEncrypt = new RadioButton();
            radioButtonDecrypt = new RadioButton();
            StartButton = new Button();
            CancelButton = new Button();
            SuspendLayout();
            // 
            // FileButtton
            // 
            FileButtton.BackColor = SystemColors.Control;
            FileButtton.Location = new Point(27, 26);
            FileButtton.Name = "FileButtton";
            FileButtton.Size = new Size(87, 24);
            FileButtton.TabIndex = 6;
            FileButtton.Text = "Файл...";
            FileButtton.UseVisualStyleBackColor = false;
            FileButtton.Click += FileButtton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(138, 26);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(498, 23);
            textBox1.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 78);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 8;
            label1.Text = "Пароль";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(138, 70);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(101, 23);
            textBox2.TabIndex = 9;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(27, 115);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(609, 31);
            progressBar1.TabIndex = 10;
            // 
            // radioButtonEncrypt
            // 
            radioButtonEncrypt.AutoSize = true;
            radioButtonEncrypt.Location = new Point(288, 70);
            radioButtonEncrypt.Name = "radioButtonEncrypt";
            radioButtonEncrypt.Size = new Size(102, 19);
            radioButtonEncrypt.TabIndex = 11;
            radioButtonEncrypt.TabStop = true;
            radioButtonEncrypt.Text = "Зашифровать";
            radioButtonEncrypt.UseVisualStyleBackColor = true;
            // 
            // radioButtonDecrypt
            // 
            radioButtonDecrypt.AutoSize = true;
            radioButtonDecrypt.Location = new Point(427, 70);
            radioButtonDecrypt.Name = "radioButtonDecrypt";
            radioButtonDecrypt.Size = new Size(108, 19);
            radioButtonDecrypt.TabIndex = 12;
            radioButtonDecrypt.TabStop = true;
            radioButtonDecrypt.Text = "Расшифровать";
            radioButtonDecrypt.UseVisualStyleBackColor = true;
            // 
            // StartButton
            // 
            StartButton.BackColor = SystemColors.Control;
            StartButton.Location = new Point(427, 167);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(87, 24);
            StartButton.TabIndex = 13;
            StartButton.Text = "Пуск";
            StartButton.UseVisualStyleBackColor = false;
            StartButton.Click += StartButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.BackColor = SystemColors.Control;
            CancelButton.Location = new Point(549, 167);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(87, 24);
            CancelButton.TabIndex = 14;
            CancelButton.Text = "Отмена";
            CancelButton.UseVisualStyleBackColor = false;
            CancelButton.Click += CancelButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(668, 209);
            Controls.Add(CancelButton);
            Controls.Add(StartButton);
            Controls.Add(radioButtonDecrypt);
            Controls.Add(radioButtonEncrypt);
            Controls.Add(progressBar1);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(FileButtton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button FileButtton;
        private TextBox textBox1;
        private Label label1;
        private TextBox textBox2;
        private ProgressBar progressBar1;
        private RadioButton radioButtonEncrypt;
        private RadioButton radioButtonDecrypt;
        private Button StartButton;
        private Button CancelButton;
    }
}
