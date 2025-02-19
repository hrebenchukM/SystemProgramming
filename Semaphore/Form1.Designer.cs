namespace SemaphoreHW
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            listBox1 = new ListBox();
            listBox2 = new ListBox();
            listBox3 = new ListBox();
            label4 = new Label();
            numericUpDown1 = new NumericUpDown();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 28);
            label1.Name = "label1";
            label1.Size = new Size(121, 15);
            label1.TabIndex = 0;
            label1.Text = "Работающие потоки";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(190, 28);
            label2.Name = "label2";
            label2.Size = new Size(120, 15);
            label2.TabIndex = 1;
            label2.Text = "Ожидающие потоки";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(351, 28);
            label3.Name = "label3";
            label3.Size = new Size(110, 15);
            label3.TabIndex = 2;
            label3.Text = "Созданные потоки";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(22, 61);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(120, 334);
            listBox1.TabIndex = 3;
            listBox1.MouseDoubleClick += listBox1_MouseDoubleClick;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(190, 61);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(120, 334);
            listBox2.TabIndex = 4;
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.ItemHeight = 15;
            listBox3.Location = new Point(351, 61);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(120, 334);
            listBox3.TabIndex = 5;
            listBox3.MouseDoubleClick += listBox3_MouseDoubleClick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 415);
            label4.Name = "label4";
            label4.Size = new Size(172, 15);
            label4.TabIndex = 6;
            label4.Text = "Количество мест в семафоре:";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(200, 407);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(100, 23);
            numericUpDown1.TabIndex = 7;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // button1
            // 
            button1.Location = new Point(351, 411);
            button1.Name = "button1";
            button1.Size = new Size(120, 23);
            button1.TabIndex = 8;
            button1.Text = "Создать поток";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(496, 469);
            Controls.Add(button1);
            Controls.Add(numericUpDown1);
            Controls.Add(label4);
            Controls.Add(listBox3);
            Controls.Add(listBox2);
            Controls.Add(listBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Тест семафора";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private ListBox listBox1;
        private ListBox listBox2;
        private ListBox listBox3;
        private Label label4;
        private NumericUpDown numericUpDown1;
        private Button button1;
    }
}
