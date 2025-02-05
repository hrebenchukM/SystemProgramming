namespace FileSearcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            listView1 = new ListView();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            SearchButton = new Button();
            StopButton = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            SubdirectoriesCheckBox = new CheckBox();
            comboBox1 = new ComboBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 9);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 0;
            label1.Text = "Файл";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(119, 9);
            label2.Name = "label2";
            label2.Size = new Size(149, 15);
            label2.TabIndex = 1;
            label2.Text = "Слово или фраза в файле";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(307, 12);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 2;
            label3.Text = "Диски";
            // 
            // listView1
            // 
            listView1.Location = new Point(12, 142);
            listView1.Name = "listView1";
            listView1.Size = new Size(711, 317);
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 33);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(136, 33);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 5;
            // 
            // SearchButton
            // 
            SearchButton.Location = new Point(404, 9);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(104, 63);
            SearchButton.TabIndex = 6;
            SearchButton.Text = "Найти";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // StopButton
            // 
            StopButton.Location = new Point(514, 9);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(104, 63);
            StopButton.TabIndex = 7;
            StopButton.Text = "Остановить";
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButton_Click;
            // 
            // button3
            // 
            button3.BackgroundImage = (Image)resources.GetObject("button3.BackgroundImage");
            button3.Location = new Point(12, 62);
            button3.Name = "button3";
            button3.Size = new Size(34, 34);
            button3.TabIndex = 8;
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.BackgroundImage = (Image)resources.GetObject("button4.BackgroundImage");
            button4.Location = new Point(42, 102);
            button4.Name = "button4";
            button4.Size = new Size(34, 34);
            button4.TabIndex = 9;
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.BackgroundImage = (Image)resources.GetObject("button5.BackgroundImage");
            button5.Location = new Point(78, 62);
            button5.Name = "button5";
            button5.Size = new Size(34, 34);
            button5.TabIndex = 10;
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.BackgroundImage = (Image)resources.GetObject("button6.BackgroundImage");
            button6.Location = new Point(118, 102);
            button6.Name = "button6";
            button6.Size = new Size(34, 34);
            button6.TabIndex = 11;
            button6.UseVisualStyleBackColor = true;
            // 
            // SubdirectoriesCheckBox
            // 
            SubdirectoriesCheckBox.AutoSize = true;
            SubdirectoriesCheckBox.Location = new Point(626, 35);
            SubdirectoriesCheckBox.Name = "SubdirectoriesCheckBox";
            SubdirectoriesCheckBox.Size = new Size(97, 19);
            SubdirectoriesCheckBox.TabIndex = 12;
            SubdirectoriesCheckBox.Text = "Подкаталоги";
            SubdirectoriesCheckBox.UseVisualStyleBackColor = true;
            SubdirectoriesCheckBox.CheckedChanged += SubdirectoriesCheckBox_CheckedChanged;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(268, 33);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(246, 112);
            label4.Name = "label4";
            label4.Size = new Size(214, 15);
            label4.TabIndex = 14;
            label4.Text = "Результаты поиска : файлов найдено ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(735, 471);
            Controls.Add(label4);
            Controls.Add(comboBox1);
            Controls.Add(SubdirectoriesCheckBox);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(StopButton);
            Controls.Add(SearchButton);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(listView1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "File Search";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private ListView listView1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button SearchButton;
        private Button StopButton;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private CheckBox SubdirectoriesCheckBox;
        private ComboBox comboBox1;
        private Label label4;
    }
}
