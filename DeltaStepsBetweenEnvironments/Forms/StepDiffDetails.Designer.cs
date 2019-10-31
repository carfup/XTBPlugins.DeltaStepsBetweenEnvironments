namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Forms
{
    partial class StepDiffDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.dataGridViewStepDetails = new System.Windows.Forms.DataGridView();
            this.Attribute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStepDetails)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(1293, 890);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(137, 41);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Quit";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // dataGridViewStepDetails
            // 
            this.dataGridViewStepDetails.AllowUserToAddRows = false;
            this.dataGridViewStepDetails.AllowUserToDeleteRows = false;
            this.dataGridViewStepDetails.AllowUserToOrderColumns = true;
            this.dataGridViewStepDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStepDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Attribute,
            this.Value});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewStepDetails.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewStepDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewStepDetails.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewStepDetails.Name = "dataGridViewStepDetails";
            this.dataGridViewStepDetails.ReadOnly = true;
            this.dataGridViewStepDetails.RowHeadersWidth = 72;
            this.dataGridViewStepDetails.RowTemplate.Height = 31;
            this.dataGridViewStepDetails.Size = new System.Drawing.Size(1429, 878);
            this.dataGridViewStepDetails.TabIndex = 9;
            // 
            // Attribute
            // 
            this.Attribute.HeaderText = "Attribute";
            this.Attribute.MinimumWidth = 9;
            this.Attribute.Name = "Attribute";
            this.Attribute.ReadOnly = true;
            this.Attribute.Width = 200;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.HeaderText = "Value";
            this.Value.MinimumWidth = 9;
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonCancel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewStepDetails, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1435, 937);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // StepDiffDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1435, 937);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "StepDiffDetails";
            this.ShowIcon = false;
            this.Text = "Step Detail";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStepDetails)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridView dataGridViewStepDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Attribute;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}