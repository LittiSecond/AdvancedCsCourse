namespace OurGame
{
    partial class MainGameForm
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
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.exitButton = new System.Windows.Forms.Button();
            this.recordButton = new System.Windows.Forms.Button();
            this.startGameButton = new System.Windows.Forms.Button();
            this.messageLabel = new System.Windows.Forms.Label();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonsPanel.Controls.Add(this.exitButton);
            this.buttonsPanel.Controls.Add(this.recordButton);
            this.buttonsPanel.Controls.Add(this.startGameButton);
            this.buttonsPanel.Location = new System.Drawing.Point(66, 64);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(200, 107);
            this.buttonsPanel.TabIndex = 0;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(13, 73);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(173, 23);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Выход";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(13, 43);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(173, 23);
            this.recordButton.TabIndex = 1;
            this.recordButton.Text = "Рекорды";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
            // 
            // startGameButton
            // 
            this.startGameButton.Location = new System.Drawing.Point(13, 13);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(173, 23);
            this.startGameButton.TabIndex = 0;
            this.startGameButton.Text = "Начало игры";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // messageLabel
            // 
            this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.messageLabel.AutoSize = true;
            this.messageLabel.BackColor = System.Drawing.Color.Transparent;
            this.messageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.messageLabel.Location = new System.Drawing.Point(76, 184);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(67, 17);
            this.messageLabel.TabIndex = 1;
            this.messageLabel.Text = "Автор: ...";
            // 
            // MainGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.buttonsPanel);
            this.KeyPreview = true;
            this.Name = "MainGameForm";
            this.Text = " ";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainGameForm_KeyPress);
            this.buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel buttonsPanel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.Label messageLabel;
    }
}

