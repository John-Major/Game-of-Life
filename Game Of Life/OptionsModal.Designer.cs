
namespace Game_Of_Life
{
    partial class OptionsModal
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
            this.buttonOKOptions = new System.Windows.Forms.Button();
            this.buttonCancelOptions = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timerIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.widthUniverseNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightUniverseNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.timerIntervalNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthUniverseNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUniverseNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOKOptions
            // 
            this.buttonOKOptions.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOKOptions.Location = new System.Drawing.Point(120, 179);
            this.buttonOKOptions.Name = "buttonOKOptions";
            this.buttonOKOptions.Size = new System.Drawing.Size(75, 23);
            this.buttonOKOptions.TabIndex = 0;
            this.buttonOKOptions.Text = "OK";
            this.buttonOKOptions.UseVisualStyleBackColor = true;
            // 
            // buttonCancelOptions
            // 
            this.buttonCancelOptions.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancelOptions.Location = new System.Drawing.Point(231, 179);
            this.buttonCancelOptions.Name = "buttonCancelOptions";
            this.buttonCancelOptions.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelOptions.TabIndex = 1;
            this.buttonCancelOptions.Text = "Cancel";
            this.buttonCancelOptions.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Timer Interval in Milliseconds";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Width of Universe in Cells";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Height of Universe in Cells";
            // 
            // timerIntervalNumericUpDown
            // 
            this.timerIntervalNumericUpDown.Location = new System.Drawing.Point(224, 46);
            this.timerIntervalNumericUpDown.Name = "timerIntervalNumericUpDown";
            this.timerIntervalNumericUpDown.Size = new System.Drawing.Size(92, 20);
            this.timerIntervalNumericUpDown.TabIndex = 8;
            // 
            // widthUniverseNumericUpDown
            // 
            this.widthUniverseNumericUpDown.Location = new System.Drawing.Point(224, 78);
            this.widthUniverseNumericUpDown.Name = "widthUniverseNumericUpDown";
            this.widthUniverseNumericUpDown.Size = new System.Drawing.Size(92, 20);
            this.widthUniverseNumericUpDown.TabIndex = 9;
            // 
            // heightUniverseNumericUpDown
            // 
            this.heightUniverseNumericUpDown.Location = new System.Drawing.Point(224, 108);
            this.heightUniverseNumericUpDown.Name = "heightUniverseNumericUpDown";
            this.heightUniverseNumericUpDown.Size = new System.Drawing.Size(92, 20);
            this.heightUniverseNumericUpDown.TabIndex = 10;
            // 
            // OptionsModal
            // 
            this.AcceptButton = this.buttonOKOptions;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancelOptions;
            this.ClientSize = new System.Drawing.Size(430, 244);
            this.Controls.Add(this.heightUniverseNumericUpDown);
            this.Controls.Add(this.widthUniverseNumericUpDown);
            this.Controls.Add(this.timerIntervalNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancelOptions);
            this.Controls.Add(this.buttonOKOptions);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options Dialog";
            ((System.ComponentModel.ISupportInitialize)(this.timerIntervalNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthUniverseNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUniverseNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOKOptions;
        private System.Windows.Forms.Button buttonCancelOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown timerIntervalNumericUpDown;
        private System.Windows.Forms.NumericUpDown widthUniverseNumericUpDown;
        private System.Windows.Forms.NumericUpDown heightUniverseNumericUpDown;
    }
}