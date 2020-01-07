namespace MachDien.App
{
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox_tcp = new System.Windows.Forms.PictureBox();
            this.pictureBox_hub = new System.Windows.Forms.PictureBox();
            this.pictureBox_webpage = new System.Windows.Forms.PictureBox();
            this.label_tcpServer = new System.Windows.Forms.Label();
            this.label_hub = new System.Windows.Forms.Label();
            this.label_webpage = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_web = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tcp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_hub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_webpage)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox_tcp);
            this.panel1.Controls.Add(this.pictureBox_hub);
            this.panel1.Controls.Add(this.pictureBox_webpage);
            this.panel1.Controls.Add(this.label_tcpServer);
            this.panel1.Controls.Add(this.label_hub);
            this.panel1.Controls.Add(this.label_webpage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1054, 53);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox_tcp
            // 
            this.pictureBox_tcp.Image = global::MachDien.App.Properties.Resources.dot_yellow_50;
            this.pictureBox_tcp.Location = new System.Drawing.Point(666, 12);
            this.pictureBox_tcp.Name = "pictureBox_tcp";
            this.pictureBox_tcp.Size = new System.Drawing.Size(35, 32);
            this.pictureBox_tcp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_tcp.TabIndex = 5;
            this.pictureBox_tcp.TabStop = false;
            // 
            // pictureBox_hub
            // 
            this.pictureBox_hub.Image = global::MachDien.App.Properties.Resources.dot_yellow_50;
            this.pictureBox_hub.Location = new System.Drawing.Point(334, 12);
            this.pictureBox_hub.Name = "pictureBox_hub";
            this.pictureBox_hub.Size = new System.Drawing.Size(35, 32);
            this.pictureBox_hub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_hub.TabIndex = 5;
            this.pictureBox_hub.TabStop = false;
            // 
            // pictureBox_webpage
            // 
            this.pictureBox_webpage.Image = global::MachDien.App.Properties.Resources.dot_yellow_50;
            this.pictureBox_webpage.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_webpage.Name = "pictureBox_webpage";
            this.pictureBox_webpage.Size = new System.Drawing.Size(35, 32);
            this.pictureBox_webpage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_webpage.TabIndex = 4;
            this.pictureBox_webpage.TabStop = false;
            // 
            // label_tcpServer
            // 
            this.label_tcpServer.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_tcpServer.Location = new System.Drawing.Point(707, 12);
            this.label_tcpServer.Name = "label_tcpServer";
            this.label_tcpServer.Size = new System.Drawing.Size(275, 32);
            this.label_tcpServer.TabIndex = 2;
            this.label_tcpServer.Text = "TCPServer";
            this.label_tcpServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_hub
            // 
            this.label_hub.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_hub.Location = new System.Drawing.Point(375, 12);
            this.label_hub.Name = "label_hub";
            this.label_hub.Size = new System.Drawing.Size(275, 32);
            this.label_hub.TabIndex = 2;
            this.label_hub.Text = "Hub";
            this.label_hub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_webpage
            // 
            this.label_webpage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_webpage.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_webpage.Location = new System.Drawing.Point(53, 12);
            this.label_webpage.Name = "label_webpage";
            this.label_webpage.Size = new System.Drawing.Size(275, 32);
            this.label_webpage.TabIndex = 1;
            this.label_webpage.Text = "Web";
            this.label_webpage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_webpage.Click += new System.EventHandler(this.label_webpage_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel_web);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1054, 657);
            this.panel2.TabIndex = 1;
            // 
            // panel_web
            // 
            this.panel_web.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_web.Location = new System.Drawing.Point(0, 0);
            this.panel_web.Name = "panel_web";
            this.panel_web.Size = new System.Drawing.Size(1054, 513);
            this.panel_web.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.richTextBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 513);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1054, 144);
            this.panel3.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1054, 144);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 710);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SERVER GIAM SAT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tcp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_hub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_webpage)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_webpage;
        private System.Windows.Forms.Label label_hub;
        private System.Windows.Forms.PictureBox pictureBox_webpage;
        private System.Windows.Forms.PictureBox pictureBox_hub;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox pictureBox_tcp;
        private System.Windows.Forms.Label label_tcpServer;
        private System.Windows.Forms.Panel panel_web;
    }
}

