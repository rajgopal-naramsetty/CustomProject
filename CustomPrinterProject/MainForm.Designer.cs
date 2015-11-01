namespace CustomPrinterProject
{
    partial class MainForm
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
            this.cbPrinter = new System.Windows.Forms.ComboBox();
            this.lblSelectPrinter = new System.Windows.Forms.Label();
            this.btnCheckStatus = new System.Windows.Forms.Button();
            this.rtbDetails = new System.Windows.Forms.RichTextBox();
            this.btnReadMagData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbPrinter
            // 
            this.cbPrinter.FormattingEnabled = true;
            this.cbPrinter.Location = new System.Drawing.Point(163, 52);
            this.cbPrinter.Name = "cbPrinter";
            this.cbPrinter.Size = new System.Drawing.Size(301, 21);
            this.cbPrinter.TabIndex = 0;
            // 
            // lblSelectPrinter
            // 
            this.lblSelectPrinter.AutoSize = true;
            this.lblSelectPrinter.Location = new System.Drawing.Point(28, 60);
            this.lblSelectPrinter.Name = "lblSelectPrinter";
            this.lblSelectPrinter.Size = new System.Drawing.Size(70, 13);
            this.lblSelectPrinter.TabIndex = 1;
            this.lblSelectPrinter.Text = "Select Printer";
            // 
            // btnCheckStatus
            // 
            this.btnCheckStatus.Location = new System.Drawing.Point(503, 47);
            this.btnCheckStatus.Name = "btnCheckStatus";
            this.btnCheckStatus.Size = new System.Drawing.Size(185, 35);
            this.btnCheckStatus.TabIndex = 2;
            this.btnCheckStatus.Text = "Check Status";
            this.btnCheckStatus.UseVisualStyleBackColor = true;
            this.btnCheckStatus.Click += new System.EventHandler(this.btnCheckStatus_Click);
            // 
            // rtbDetails
            // 
            this.rtbDetails.Location = new System.Drawing.Point(31, 422);
            this.rtbDetails.Name = "rtbDetails";
            this.rtbDetails.Size = new System.Drawing.Size(657, 325);
            this.rtbDetails.TabIndex = 3;
            this.rtbDetails.Text = "";
            // 
            // btnReadMagData
            // 
            this.btnReadMagData.Location = new System.Drawing.Point(503, 105);
            this.btnReadMagData.Name = "btnReadMagData";
            this.btnReadMagData.Size = new System.Drawing.Size(185, 35);
            this.btnReadMagData.TabIndex = 4;
            this.btnReadMagData.Text = "Read Magnetic Data";
            this.btnReadMagData.UseVisualStyleBackColor = true;
            this.btnReadMagData.Click += new System.EventHandler(this.btnReadMagData_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 759);
            this.Controls.Add(this.btnReadMagData);
            this.Controls.Add(this.rtbDetails);
            this.Controls.Add(this.btnCheckStatus);
            this.Controls.Add(this.lblSelectPrinter);
            this.Controls.Add(this.cbPrinter);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPrinter;
        private System.Windows.Forms.Label lblSelectPrinter;
        private System.Windows.Forms.Button btnCheckStatus;
        private System.Windows.Forms.RichTextBox rtbDetails;
        private System.Windows.Forms.Button btnReadMagData;
    }
}

