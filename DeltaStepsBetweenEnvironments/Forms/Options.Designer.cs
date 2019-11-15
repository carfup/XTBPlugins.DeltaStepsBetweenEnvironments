namespace Carfup.XTBPlugins.Forms
{
    partial class Options
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
            this.bgStats = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkboxAllowStats = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtoradioButtonSortingOrderDesc = new System.Windows.Forms.RadioButton();
            this.radioButtonSortingOrderAsc = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxSkipHidden = new System.Windows.Forms.CheckBox();
            this.bgStats.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bgStats
            // 
            this.bgStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bgStats.Controls.Add(this.label1);
            this.bgStats.Controls.Add(this.checkboxAllowStats);
            this.bgStats.Location = new System.Drawing.Point(24, 350);
            this.bgStats.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.bgStats.Name = "bgStats";
            this.bgStats.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.bgStats.Size = new System.Drawing.Size(761, 216);
            this.bgStats.TabIndex = 5;
            this.bgStats.TabStop = false;
            this.bgStats.Text = "Statistics";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(737, 126);
            this.label1.TabIndex = 6;
            this.label1.Text = "This plugin collects ONLY anonymous usage statistics. \r\nNo information related yo" +
    "ur CRM / Organization will be retrieve. \r\n\r\nThis will help us to improve the mos" +
    "t used features !";
            // 
            // checkboxAllowStats
            // 
            this.checkboxAllowStats.AutoSize = true;
            this.checkboxAllowStats.Checked = true;
            this.checkboxAllowStats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxAllowStats.Location = new System.Drawing.Point(18, 168);
            this.checkboxAllowStats.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.checkboxAllowStats.Name = "checkboxAllowStats";
            this.checkboxAllowStats.Size = new System.Drawing.Size(164, 29);
            this.checkboxAllowStats.TabIndex = 5;
            this.checkboxAllowStats.Text = "Allow statistics";
            this.checkboxAllowStats.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(499, 578);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(137, 42);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(647, 578);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(137, 42);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButtoradioButtonSortingOrderDesc);
            this.groupBox1.Controls.Add(this.radioButtonSortingOrderAsc);
            this.groupBox1.Location = new System.Drawing.Point(24, 22);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(761, 152);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result sorting";
            // 
            // radioButtoradioButtonSortingOrderDesc
            // 
            this.radioButtoradioButtonSortingOrderDesc.AutoSize = true;
            this.radioButtoradioButtonSortingOrderDesc.Location = new System.Drawing.Point(11, 85);
            this.radioButtoradioButtonSortingOrderDesc.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.radioButtoradioButtonSortingOrderDesc.Name = "radioButtoradioButtonSortingOrderDesc";
            this.radioButtoradioButtonSortingOrderDesc.Size = new System.Drawing.Size(141, 29);
            this.radioButtoradioButtonSortingOrderDesc.TabIndex = 1;
            this.radioButtoradioButtonSortingOrderDesc.Text = "Descending";
            this.radioButtoradioButtonSortingOrderDesc.UseVisualStyleBackColor = true;
            // 
            // radioButtonSortingOrderAsc
            // 
            this.radioButtonSortingOrderAsc.AutoSize = true;
            this.radioButtonSortingOrderAsc.Checked = true;
            this.radioButtonSortingOrderAsc.Location = new System.Drawing.Point(11, 42);
            this.radioButtonSortingOrderAsc.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.radioButtonSortingOrderAsc.Name = "radioButtonSortingOrderAsc";
            this.radioButtonSortingOrderAsc.Size = new System.Drawing.Size(130, 29);
            this.radioButtonSortingOrderAsc.TabIndex = 0;
            this.radioButtonSortingOrderAsc.TabStop = true;
            this.radioButtonSortingOrderAsc.Text = "Ascending";
            this.radioButtonSortingOrderAsc.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBoxSkipHidden);
            this.groupBox2.Location = new System.Drawing.Point(24, 186);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Size = new System.Drawing.Size(761, 152);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Compare Settings";
            // 
            // checkBoxSkipHidden
            // 
            this.checkBoxSkipHidden.AutoSize = true;
            this.checkBoxSkipHidden.Checked = true;
            this.checkBoxSkipHidden.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSkipHidden.Location = new System.Drawing.Point(18, 32);
            this.checkBoxSkipHidden.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSkipHidden.Name = "checkBoxSkipHidden";
            this.checkBoxSkipHidden.Size = new System.Drawing.Size(259, 29);
            this.checkBoxSkipHidden.TabIndex = 0;
            this.checkBoxSkipHidden.Text = "Skip Hidden Plugin Steps";
            this.checkBoxSkipHidden.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 636);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.bgStats);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Options";
            this.ShowIcon = false;
            this.Text = "Options";
            this.bgStats.ResumeLayout(false);
            this.bgStats.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox bgStats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkboxAllowStats;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtoradioButtonSortingOrderDesc;
        private System.Windows.Forms.RadioButton radioButtonSortingOrderAsc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxSkipHidden;
    }
}