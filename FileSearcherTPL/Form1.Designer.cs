namespace FileSearcherTPL
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
            SearchButton = new Button();
            StopButton = new Button();
            SubdirectoriesCheckBox = new CheckBox();
            comboBoxPath = new ComboBox();
            label3 = new Label();
            textBoxText = new TextBox();
            label2 = new Label();
            textBoxMask = new TextBox();
            label1 = new Label();
            listView1 = new ListView();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            labelRes = new Label();
            SuspendLayout();
            // 
            // SearchButton
            // 
            SearchButton.Location = new Point(412, 9);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(104, 63);
            SearchButton.TabIndex = 7;
            SearchButton.Text = "Найти";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // StopButton
            // 
            StopButton.Location = new Point(522, 12);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(104, 63);
            StopButton.TabIndex = 8;
            StopButton.Text = "Остановить";
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButton_Click;
            // 
            // SubdirectoriesCheckBox
            // 
            SubdirectoriesCheckBox.AutoSize = true;
            SubdirectoriesCheckBox.Location = new Point(632, 35);
            SubdirectoriesCheckBox.Name = "SubdirectoriesCheckBox";
            SubdirectoriesCheckBox.Size = new Size(97, 19);
            SubdirectoriesCheckBox.TabIndex = 13;
            SubdirectoriesCheckBox.Text = "Подкаталоги";
            SubdirectoriesCheckBox.UseVisualStyleBackColor = true;
            // 
            // comboBoxPath
            // 
            comboBoxPath.FormattingEnabled = true;
            comboBoxPath.Location = new Point(285, 27);
            comboBoxPath.Name = "comboBoxPath";
            comboBoxPath.Size = new Size(121, 23);
            comboBoxPath.TabIndex = 14;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(325, 9);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 15;
            label3.Text = "Диски";
            // 
            // textBoxText
            // 
            textBoxText.Location = new Point(153, 27);
            textBoxText.Name = "textBoxText";
            textBoxText.Size = new Size(100, 23);
            textBoxText.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(134, 9);
            label2.Name = "label2";
            label2.Size = new Size(149, 15);
            label2.TabIndex = 17;
            label2.Text = "Слово или фраза в файле";
            // 
            // textBoxMask
            // 
            textBoxMask.Location = new Point(12, 27);
            textBoxMask.Name = "textBoxMask";
            textBoxMask.Size = new Size(100, 23);
            textBoxMask.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 9);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 19;
            label1.Text = "Файл";
            // 
            // listView1
            // 
            listView1.Location = new Point(12, 142);
            listView1.Name = "listView1";
            listView1.Size = new Size(711, 317);
            listView1.TabIndex = 20;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // button3
            // 
            button3.BackgroundImage = (Image)resources.GetObject("button3.BackgroundImage");
            button3.Location = new Point(12, 65);
            button3.Name = "button3";
            button3.Size = new Size(34, 34);
            button3.TabIndex = 21;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackgroundImage = (Image)resources.GetObject("button4.BackgroundImage");
            button4.Location = new Point(46, 102);
            button4.Name = "button4";
            button4.Size = new Size(34, 34);
            button4.TabIndex = 22;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackgroundImage = (Image)resources.GetObject("button5.BackgroundImage");
            button5.Location = new Point(78, 65);
            button5.Name = "button5";
            button5.Size = new Size(34, 34);
            button5.TabIndex = 23;
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.BackgroundImage = (Image)resources.GetObject("button6.BackgroundImage");
            button6.Location = new Point(109, 102);
            button6.Name = "button6";
            button6.Size = new Size(34, 34);
            button6.TabIndex = 24;
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // labelRes
            // 
            labelRes.AutoSize = true;
            labelRes.Location = new Point(285, 112);
            labelRes.Name = "labelRes";
            labelRes.Size = new Size(111, 15);
            labelRes.TabIndex = 25;
            labelRes.Text = "Результаты поиска";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(735, 471);
            Controls.Add(labelRes);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(listView1);
            Controls.Add(label1);
            Controls.Add(textBoxMask);
            Controls.Add(label2);
            Controls.Add(textBoxText);
            Controls.Add(label3);
            Controls.Add(comboBoxPath);
            Controls.Add(SubdirectoriesCheckBox);
            Controls.Add(StopButton);
            Controls.Add(SearchButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SearchButton;
        private Button StopButton;
        private CheckBox SubdirectoriesCheckBox;
        private ComboBox comboBoxPath;
        private Label label3;
        private TextBox textBoxText;
        private Label label2;
        private TextBox textBoxMask;
        private Label label1;
        private ListView listView1;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Label labelRes;
    }
}
