﻿namespace CTCT
{
    partial class AuthenticationForm
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
            this.webBrowserControl = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserControl
            // 
            this.webBrowserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserControl.Location = new System.Drawing.Point(0, 0);
            this.webBrowserControl.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserControl.Name = "webBrowserControl";
            this.webBrowserControl.Size = new System.Drawing.Size(665, 436);
            this.webBrowserControl.TabIndex = 0;
            this.webBrowserControl.Url = new System.Uri("https://constantcontact.mashery.com/login/login", System.UriKind.Absolute);
            this.webBrowserControl.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowserControl_Navigated);
            // 
            // AuthenticationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 436);
            this.Controls.Add(this.webBrowserControl);
            this.Name = "AuthenticationForm";
            this.Text = "AuthenticationForm";
            this.Load += new System.EventHandler(this.AuthenticationForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserControl;
    }
}