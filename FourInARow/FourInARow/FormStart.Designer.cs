namespace FourInARow
{
    partial class FormStart
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
            this.labePlayer1 = new System.Windows.Forms.Label();
            this.labePlayer2 = new System.Windows.Forms.Label();
            this.textboxPlayer1 = new System.Windows.Forms.TextBox();
            this.textboxPlayer2 = new System.Windows.Forms.TextBox();
            this.m_LabelHeight = new System.Windows.Forms.Label();
            this.m_LabelWidth = new System.Windows.Forms.Label();
            this.numericUpDownRows = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCols = new System.Windows.Forms.NumericUpDown();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCols)).BeginInit();
            this.SuspendLayout();
            // 
            // labePlayer1
            // 
            this.labePlayer1.AutoSize = true;
            this.labePlayer1.Location = new System.Drawing.Point(38, 39);
            this.labePlayer1.Name = "labePlayer1";
            this.labePlayer1.Size = new System.Drawing.Size(48, 13);
            this.labePlayer1.TabIndex = 0;
            this.labePlayer1.Text = "Player 1:";
            // 
            // labePlayer2
            // 
            this.labePlayer2.AutoSize = true;
            this.labePlayer2.Location = new System.Drawing.Point(38, 76);
            this.labePlayer2.Name = "labePlayer2";
            this.labePlayer2.Size = new System.Drawing.Size(48, 13);
            this.labePlayer2.TabIndex = 1;
            this.labePlayer2.Text = "Player 2:";
            // 
            // textboxPlayer1
            // 
            this.textboxPlayer1.Location = new System.Drawing.Point(92, 36);
            this.textboxPlayer1.MaxLength = 10;
            this.textboxPlayer1.Name = "textboxPlayer1";
            this.textboxPlayer1.Size = new System.Drawing.Size(100, 20);
            this.textboxPlayer1.TabIndex = 2;
            // 
            // textboxPlayer2
            // 
            this.textboxPlayer2.Location = new System.Drawing.Point(92, 73);
            this.textboxPlayer2.MaxLength = 10;
            this.textboxPlayer2.Name = "textboxPlayer2";
            this.textboxPlayer2.Size = new System.Drawing.Size(100, 20);
            this.textboxPlayer2.TabIndex = 3;
            // 
            // m_LabelHeight
            // 
            this.m_LabelHeight.AutoSize = true;
            this.m_LabelHeight.Location = new System.Drawing.Point(38, 147);
            this.m_LabelHeight.Name = "m_LabelHeight";
            this.m_LabelHeight.Size = new System.Drawing.Size(113, 13);
            this.m_LabelHeight.TabIndex = 4;
            this.m_LabelHeight.Text = "Board Height (in cells):";
            // 
            // m_LabelWidth
            // 
            this.m_LabelWidth.AutoSize = true;
            this.m_LabelWidth.Location = new System.Drawing.Point(38, 186);
            this.m_LabelWidth.Name = "m_LabelWidth";
            this.m_LabelWidth.Size = new System.Drawing.Size(110, 13);
            this.m_LabelWidth.TabIndex = 5;
            this.m_LabelWidth.Text = "Board Width (in cells):";
            // 
            // numericUpDownRows
            // 
            this.numericUpDownRows.Location = new System.Drawing.Point(158, 145);
            this.numericUpDownRows.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRows.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownRows.Name = "numericUpDownRows";
            this.numericUpDownRows.ReadOnly = true;
            this.numericUpDownRows.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownRows.TabIndex = 6;
            this.numericUpDownRows.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // numericUpDownCols
            // 
            this.numericUpDownCols.Location = new System.Drawing.Point(158, 184);
            this.numericUpDownCols.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCols.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownCols.Name = "numericUpDownCols";
            this.numericUpDownCols.ReadOnly = true;
            this.numericUpDownCols.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownCols.TabIndex = 7;
            this.numericUpDownCols.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(41, 218);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 8;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(123, 218);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 262);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.numericUpDownCols);
            this.Controls.Add(this.numericUpDownRows);
            this.Controls.Add(this.m_LabelWidth);
            this.Controls.Add(this.m_LabelHeight);
            this.Controls.Add(this.textboxPlayer2);
            this.Controls.Add(this.textboxPlayer1);
            this.Controls.Add(this.labePlayer2);
            this.Controls.Add(this.labePlayer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Properties";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCols)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label labePlayer1;
        private System.Windows.Forms.Label labePlayer2;
        private System.Windows.Forms.TextBox textboxPlayer1;
        private System.Windows.Forms.TextBox textboxPlayer2;
        private System.Windows.Forms.Label m_LabelHeight;
        private System.Windows.Forms.Label m_LabelWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownRows;
        private System.Windows.Forms.NumericUpDown numericUpDownCols;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}

