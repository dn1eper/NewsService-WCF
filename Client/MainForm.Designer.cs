namespace Client
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.categoryTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // categoryTextBox
            // 
            this.categoryTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.categoryTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.categoryTextBox.Location = new System.Drawing.Point(0, 0);
            this.categoryTextBox.Name = "categoryTextBox";
            this.categoryTextBox.Size = new System.Drawing.Size(968, 28);
            this.categoryTextBox.TabIndex = 0;
            this.categoryTextBox.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.categoryTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // okButton
            // 
            this.okButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.okButton.Enabled = false;
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.okButton.Location = new System.Drawing.Point(968, 0);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(94, 25);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OnSubscribe);
            // 
            // listBox
            // 
            this.listBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 22;
            this.listBox.Location = new System.Drawing.Point(12, 34);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(553, 400);
            this.listBox.TabIndex = 3;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.OnSelectionChanged);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(571, 34);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(479, 400);
            this.webBrowser.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 433);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.categoryTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1080, 480);
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "News";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox categoryTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}

