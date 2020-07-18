namespace Multiball
{
    partial class MultiBall
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
            this.scoreArea = new System.Windows.Forms.PictureBox();
            this.gameArea = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.scoreArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameArea)).BeginInit();
            this.SuspendLayout();
            // 
            // scoreArea
            // 
            this.scoreArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.scoreArea.Location = new System.Drawing.Point(0, 0);
            this.scoreArea.Name = "scoreArea";
            this.scoreArea.Size = new System.Drawing.Size(400, 100);
            this.scoreArea.TabIndex = 0;
            this.scoreArea.TabStop = false;
            this.scoreArea.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintScoreArea);
            // 
            // gameArea
            // 
            this.gameArea.BackColor = System.Drawing.Color.Black;
            this.gameArea.Location = new System.Drawing.Point(0, 100);
            this.gameArea.Name = "gameArea";
            this.gameArea.Size = new System.Drawing.Size(400, 550);
            this.gameArea.TabIndex = 1;
            this.gameArea.TabStop = false;
            this.gameArea.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintGameArea);
            this.gameArea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DoMouseClick);
            this.gameArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DoMouseMove);
            // 
            // MultiBall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 651);
            this.Controls.Add(this.gameArea);
            this.Controls.Add(this.scoreArea);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MultiBall";
            this.Text = "Multi-Ball";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.scoreArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox scoreArea;
        private System.Windows.Forms.PictureBox gameArea;
    }
}

