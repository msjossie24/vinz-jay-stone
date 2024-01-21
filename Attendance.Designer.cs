namespace BSCS3_Prelim
{
    partial class Attendance
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
            this.components = new System.ComponentModel.Container();
            this.lblCamera = new System.Windows.Forms.Label();
            this.cmbSelectCam = new System.Windows.Forms.ComboBox();
            this.picBoxFrame = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCamera
            // 
            this.lblCamera.AutoSize = true;
            this.lblCamera.Location = new System.Drawing.Point(50, 34);
            this.lblCamera.Name = "lblCamera";
            this.lblCamera.Size = new System.Drawing.Size(43, 13);
            this.lblCamera.TabIndex = 0;
            this.lblCamera.Text = "Camera";
            // 
            // cmbSelectCam
            // 
            this.cmbSelectCam.FormattingEnabled = true;
            this.cmbSelectCam.Location = new System.Drawing.Point(117, 31);
            this.cmbSelectCam.Name = "cmbSelectCam";
            this.cmbSelectCam.Size = new System.Drawing.Size(121, 21);
            this.cmbSelectCam.TabIndex = 1;
            // 
            // picBoxFrame
            // 
            this.picBoxFrame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxFrame.Location = new System.Drawing.Point(31, 80);
            this.picBoxFrame.Name = "picBoxFrame";
            this.picBoxFrame.Size = new System.Drawing.Size(394, 335);
            this.picBoxFrame.TabIndex = 2;
            this.picBoxFrame.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(483, 31);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(599, 31);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 4;
            this.btnRead.Text = "READ";
            this.btnRead.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(461, 80);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(262, 113);
            this.textBox1.TabIndex = 5;
            // 
            // Attendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.picBoxFrame);
            this.Controls.Add(this.cmbSelectCam);
            this.Controls.Add(this.lblCamera);
            this.Name = "Attendance";
            this.Text = "Attendance";
            this.Load += new System.EventHandler(this.Attendance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCamera;
        private System.Windows.Forms.ComboBox cmbSelectCam;
        private System.Windows.Forms.PictureBox picBoxFrame;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
    }
}