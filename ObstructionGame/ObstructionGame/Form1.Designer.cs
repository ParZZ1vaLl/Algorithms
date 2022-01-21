namespace ObstructionGame
{
    using System.Windows.Forms;

    partial class Form1
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
            buttons = new Button[8,8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    buttons[i, j] = new System.Windows.Forms.Button();
                }
            }
            // 
            // button1
            // 

            var spawnX = 25;
            var spawnY = 25;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var currentButton = buttons[i, j];
                    currentButton.Location = new System.Drawing.Point(spawnX, spawnY);
                    currentButton.Name = $"button{i}{j}";
                    currentButton.Size = new System.Drawing.Size(50, 50);
                    currentButton.TabIndex = 0;
                    currentButton.UseVisualStyleBackColor = true;
                    currentButton.Click += ButtonClick;

                    spawnX += 55;
                }

                spawnX = 25;
                spawnY += 55;
            }

    
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 500);

            foreach (var button in buttons)
            {
                Controls.Add(button);
            }
            this.Name = "Form1";
            this.Text = "GameWindow";
            this.ResumeLayout(false);
        }

       
        private System.Windows.Forms.Button[,] buttons;

        #endregion
    }
}