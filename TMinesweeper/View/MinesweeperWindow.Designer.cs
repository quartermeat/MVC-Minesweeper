using System.Windows.Forms;

namespace TMinesweeper.View
{
    partial class MinesweeperWindow
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
            this.mainPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.timerUpdatedLabel = new System.Windows.Forms.Label();
            this.timerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainPanel.Location = new System.Drawing.Point(12, 12);
            this.mainPanel.MaximumSize = new System.Drawing.Size(505, 505);
            this.mainPanel.MinimumSize = new System.Drawing.Size(505, 505);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(505, 505);
            this.mainPanel.TabIndex = 0;
            // 
            // timerUpdatedLabel
            // 
            this.timerUpdatedLabel.AutoSize = true;
            this.timerUpdatedLabel.Font = new System.Drawing.Font("Engravers MT", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerUpdatedLabel.Location = new System.Drawing.Point(219, 539);
            this.timerUpdatedLabel.Name = "timerUpdatedLabel";
            this.timerUpdatedLabel.Size = new System.Drawing.Size(205, 12);
            this.timerUpdatedLabel.TabIndex = 1;
            this.timerUpdatedLabel.Text = "Starts on first move";
            this.timerUpdatedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Font = new System.Drawing.Font("Engravers MT", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerLabel.Location = new System.Drawing.Point(150, 539);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(63, 12);
            this.timerLabel.TabIndex = 2;
            this.timerLabel.Text = "Timer:";
            // 
            // MinesweeperWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ClientSize = new System.Drawing.Size(529, 585);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.timerUpdatedLabel);
            this.Controls.Add(this.mainPanel);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MinesweeperWindow";
            this.Text = "Minesweeper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlowLayoutPanel mainPanel;
        private Label timerUpdatedLabel;
        private Label timerLabel;
    }
}