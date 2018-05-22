namespace Carfup.XTBPlugins.Forms
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.buttonCloseHelp = new System.Windows.Forms.Button();
            this.checkBoxDontShowHelpStartup = new System.Windows.Forms.CheckBox();
            this.pictureBoxHelp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCloseHelp
            // 
            this.buttonCloseHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCloseHelp.Location = new System.Drawing.Point(720, 452);
            this.buttonCloseHelp.Name = "buttonCloseHelp";
            this.buttonCloseHelp.Size = new System.Drawing.Size(75, 23);
            this.buttonCloseHelp.TabIndex = 1;
            this.buttonCloseHelp.Text = "Close";
            this.buttonCloseHelp.UseVisualStyleBackColor = true;
            // 
            // checkBoxDontShowHelpStartup
            // 
            this.checkBoxDontShowHelpStartup.AutoSize = true;
            this.checkBoxDontShowHelpStartup.Checked = true;
            this.checkBoxDontShowHelpStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDontShowHelpStartup.Location = new System.Drawing.Point(15, 453);
            this.checkBoxDontShowHelpStartup.Name = "checkBoxDontShowHelpStartup";
            this.checkBoxDontShowHelpStartup.Size = new System.Drawing.Size(197, 17);
            this.checkBoxDontShowHelpStartup.TabIndex = 2;
            this.checkBoxDontShowHelpStartup.Text = "Don\'t show this on startup anymore !";
            this.checkBoxDontShowHelpStartup.UseVisualStyleBackColor = true;
            // 
            // pictureBoxHelp
            // 
            this.pictureBoxHelp.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxHelp.Image")));
            this.pictureBoxHelp.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxHelp.Name = "pictureBoxHelp";
            this.pictureBoxHelp.Size = new System.Drawing.Size(783, 435);
            this.pictureBoxHelp.TabIndex = 3;
            this.pictureBoxHelp.TabStop = false;
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCloseHelp;
            this.ClientSize = new System.Drawing.Size(807, 480);
            this.Controls.Add(this.pictureBoxHelp);
            this.Controls.Add(this.checkBoxDontShowHelpStartup);
            this.Controls.Add(this.buttonCloseHelp);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpForm";
            this.ShowIcon = false;
            this.Text = "Help";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCloseHelp;
        private System.Windows.Forms.CheckBox checkBoxDontShowHelpStartup;
        private System.Windows.Forms.PictureBox pictureBoxHelp;
    }
}