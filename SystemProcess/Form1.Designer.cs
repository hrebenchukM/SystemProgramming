namespace SystemProcess
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
            UpdateButton = new Button();
            label1 = new Label();
            listBox1 = new ListBox();
            textBox1 = new TextBox();
            EndButton = new Button();
            CreateButton = new Button();
            SuspendLayout();
            // 
            // UpdateButton
            // 
            UpdateButton.BackColor = SystemColors.ButtonHighlight;
            UpdateButton.FlatAppearance.BorderColor = Color.Black;
            UpdateButton.FlatAppearance.BorderSize = 2;
            UpdateButton.Location = new Point(30, 23);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(300, 35);
            UpdateButton.TabIndex = 0;
            UpdateButton.Text = "Обновить список процессов";
            UpdateButton.UseVisualStyleBackColor = false;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(123, 126);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 2;
            label1.Text = "Активные процессы";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(30, 144);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(300, 169);
            listBox1.TabIndex = 3;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(30, 328);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(300, 23);
            textBox1.TabIndex = 4;
            // 
            // EndButton
            // 
            EndButton.BackColor = SystemColors.ButtonHighlight;
            EndButton.FlatAppearance.BorderColor = Color.Black;
            EndButton.FlatAppearance.BorderSize = 2;
            EndButton.Location = new Point(30, 75);
            EndButton.Name = "EndButton";
            EndButton.Size = new Size(300, 35);
            EndButton.TabIndex = 6;
            EndButton.Text = "Завершить процесс";
            EndButton.UseVisualStyleBackColor = false;
            EndButton.Click += EndButton_Click;
            // 
            // CreateButton
            // 
            CreateButton.BackColor = SystemColors.ButtonHighlight;
            CreateButton.FlatAppearance.BorderColor = Color.Black;
            CreateButton.FlatAppearance.BorderSize = 2;
            CreateButton.Location = new Point(30, 371);
            CreateButton.Name = "CreateButton";
            CreateButton.Size = new Size(300, 35);
            CreateButton.TabIndex = 7;
            CreateButton.Text = "Создать новый процесс";
            CreateButton.UseVisualStyleBackColor = false;
            CreateButton.Click += CreateButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(364, 428);
            Controls.Add(CreateButton);
            Controls.Add(EndButton);
            Controls.Add(textBox1);
            Controls.Add(listBox1);
            Controls.Add(label1);
            Controls.Add(UpdateButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Диспетчер задач";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button UpdateButton;
        private Label label1;
        private ListBox listBox1;
        private TextBox textBox1;
        private Button EndButton;
        private Button CreateButton;
    }
}
