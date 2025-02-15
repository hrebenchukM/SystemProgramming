namespace MutexSemaphore
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
            Start = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // Start
            // 
            Start.BackColor = SystemColors.ActiveCaption;
            Start.Cursor = Cursors.Hand;
            Start.FlatAppearance.BorderSize = 0;
            Start.FlatStyle = FlatStyle.Flat;
            Start.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Start.ForeColor = SystemColors.HighlightText;
            Start.Location = new Point(150, 250);
            Start.Name = "Start";
            Start.Size = new Size(120, 140);
            Start.TabIndex = 1;
            Start.Text = "Старт ";
            Start.UseVisualStyleBackColor = false;
            Start.Click += Start_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 100);
            label1.Name = "label1";
            label1.Size = new Size(114, 17);
            label1.TabIndex = 2;
            label1.Text = "Ожидаем запуск...";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 150);
            label2.Name = "label2";
            label2.Size = new Size(122, 17);
            label2.TabIndex = 3;
            label2.Text = "Ожидаем процесс...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 200);
            label3.Name = "label3";
            label3.Size = new Size(146, 17);
            label3.TabIndex = 4;
            label3.Text = "Ожидаем завершение...";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(465, 403);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Start);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Мьютекс и Потоки";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button Start;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
