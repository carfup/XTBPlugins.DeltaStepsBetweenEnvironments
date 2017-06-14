namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments
{
	partial class DeltaStepsBetweenEnvironments
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxSolutionsList = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelSourceEnvironment = new System.Windows.Forms.Label();
			this.labelTargetEnvironment = new System.Windows.Forms.Label();
			this.btnChangeTargetEnvironment = new System.Windows.Forms.Button();
			this.buttonCompare = new System.Windows.Forms.Button();
			this.listBoxTargetSource = new System.Windows.Forms.ListBox();
			this.buttonLoadSolutions = new System.Windows.Forms.Button();
			this.buttonChangeSource = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listBoxSourceTarget = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.labelSourceTargetMatch = new System.Windows.Forms.Label();
			this.labelTargetSourceMatch = new System.Windows.Forms.Label();
			this.toolStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonClose});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1038, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButtonClose
			// 
			this.toolStripButtonClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonClose.Image = global::Carfup.XTBPlugins.Properties.Resources.close;
			this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonClose.Name = "toolStripButtonClose";
			this.toolStripButtonClose.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonClose.Text = "toolStripButton1";
			this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(400, 37);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(156, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Select the solution to compare :";
			// 
			// comboBoxSolutionsList
			// 
			this.comboBoxSolutionsList.FormattingEnabled = true;
			this.comboBoxSolutionsList.Location = new System.Drawing.Point(588, 32);
			this.comboBoxSolutionsList.Name = "comboBoxSolutionsList";
			this.comboBoxSolutionsList.Size = new System.Drawing.Size(273, 21);
			this.comboBoxSolutionsList.TabIndex = 11;
			this.comboBoxSolutionsList.SelectedIndexChanged += new System.EventHandler(this.comboBoxSolutionsList_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "Source Environment : ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(109, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Target Environment : ";
			// 
			// labelSourceEnvironment
			// 
			this.labelSourceEnvironment.AutoSize = true;
			this.labelSourceEnvironment.Location = new System.Drawing.Point(134, 40);
			this.labelSourceEnvironment.Name = "labelSourceEnvironment";
			this.labelSourceEnvironment.Size = new System.Drawing.Size(33, 13);
			this.labelSourceEnvironment.TabIndex = 17;
			this.labelSourceEnvironment.Text = "None";
			// 
			// labelTargetEnvironment
			// 
			this.labelTargetEnvironment.AutoSize = true;
			this.labelTargetEnvironment.BackColor = System.Drawing.SystemColors.Control;
			this.labelTargetEnvironment.Location = new System.Drawing.Point(134, 67);
			this.labelTargetEnvironment.Name = "labelTargetEnvironment";
			this.labelTargetEnvironment.Size = new System.Drawing.Size(33, 13);
			this.labelTargetEnvironment.TabIndex = 18;
			this.labelTargetEnvironment.Text = "None";
			// 
			// btnChangeTargetEnvironment
			// 
			this.btnChangeTargetEnvironment.Location = new System.Drawing.Point(266, 62);
			this.btnChangeTargetEnvironment.Name = "btnChangeTargetEnvironment";
			this.btnChangeTargetEnvironment.Size = new System.Drawing.Size(104, 23);
			this.btnChangeTargetEnvironment.TabIndex = 19;
			this.btnChangeTargetEnvironment.Text = "Change Target";
			this.btnChangeTargetEnvironment.UseVisualStyleBackColor = true;
			this.btnChangeTargetEnvironment.Click += new System.EventHandler(this.btnChangeTargetEnvironment_Click);
			// 
			// buttonCompare
			// 
			this.buttonCompare.Location = new System.Drawing.Point(403, 62);
			this.buttonCompare.Name = "buttonCompare";
			this.buttonCompare.Size = new System.Drawing.Size(246, 23);
			this.buttonCompare.TabIndex = 20;
			this.buttonCompare.Text = "Compare the two environments";
			this.buttonCompare.UseVisualStyleBackColor = true;
			this.buttonCompare.Click += new System.EventHandler(this.buttonCompare_Click);
			// 
			// listBoxTargetSource
			// 
			this.listBoxTargetSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxTargetSource.FormattingEnabled = true;
			this.listBoxTargetSource.Location = new System.Drawing.Point(6, 19);
			this.listBoxTargetSource.Name = "listBoxTargetSource";
			this.listBoxTargetSource.Size = new System.Drawing.Size(486, 472);
			this.listBoxTargetSource.TabIndex = 22;
			// 
			// buttonLoadSolutions
			// 
			this.buttonLoadSolutions.Location = new System.Drawing.Point(870, 30);
			this.buttonLoadSolutions.Name = "buttonLoadSolutions";
			this.buttonLoadSolutions.Size = new System.Drawing.Size(136, 23);
			this.buttonLoadSolutions.TabIndex = 25;
			this.buttonLoadSolutions.Text = "Load Solutions";
			this.buttonLoadSolutions.UseVisualStyleBackColor = true;
			this.buttonLoadSolutions.Click += new System.EventHandler(this.buttonLoadSolutions_Click);
			// 
			// buttonChangeSource
			// 
			this.buttonChangeSource.Location = new System.Drawing.Point(266, 35);
			this.buttonChangeSource.Name = "buttonChangeSource";
			this.buttonChangeSource.Size = new System.Drawing.Size(104, 23);
			this.buttonChangeSource.TabIndex = 26;
			this.buttonChangeSource.Text = "Change Source";
			this.buttonChangeSource.UseVisualStyleBackColor = true;
			this.buttonChangeSource.Click += new System.EventHandler(this.buttonChangeSource_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.labelSourceTargetMatch);
			this.groupBox1.Controls.Add(this.listBoxSourceTarget);
			this.groupBox1.Location = new System.Drawing.Point(3, 100);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(503, 501);
			this.groupBox1.TabIndex = 27;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Steps which are on Source but Target environment";
			// 
			// listBoxSourceTarget
			// 
			this.listBoxSourceTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxSourceTarget.FormattingEnabled = true;
			this.listBoxSourceTarget.Location = new System.Drawing.Point(9, 19);
			this.listBoxSourceTarget.Name = "listBoxSourceTarget";
			this.listBoxSourceTarget.Size = new System.Drawing.Size(488, 472);
			this.listBoxSourceTarget.TabIndex = 22;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.labelTargetSourceMatch);
			this.groupBox2.Controls.Add(this.listBoxTargetSource);
			this.groupBox2.Location = new System.Drawing.Point(530, 100);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(498, 501);
			this.groupBox2.TabIndex = 28;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Steps which are on Target but Source environment";
			// 
			// labelSourceTargetMatch
			// 
			this.labelSourceTargetMatch.AutoSize = true;
			this.labelSourceTargetMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelSourceTargetMatch.ForeColor = System.Drawing.Color.Green;
			this.labelSourceTargetMatch.Location = new System.Drawing.Point(82, 154);
			this.labelSourceTargetMatch.Name = "labelSourceTargetMatch";
			this.labelSourceTargetMatch.Size = new System.Drawing.Size(302, 31);
			this.labelSourceTargetMatch.TabIndex = 23;
			this.labelSourceTargetMatch.Text = "This is a perfect match !";
			this.labelSourceTargetMatch.Visible = false;
			// 
			// labelTargetSourceMatch
			// 
			this.labelTargetSourceMatch.AutoSize = true;
			this.labelTargetSourceMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTargetSourceMatch.ForeColor = System.Drawing.Color.Green;
			this.labelTargetSourceMatch.Location = new System.Drawing.Point(83, 154);
			this.labelTargetSourceMatch.Name = "labelTargetSourceMatch";
			this.labelTargetSourceMatch.Size = new System.Drawing.Size(302, 31);
			this.labelTargetSourceMatch.TabIndex = 24;
			this.labelTargetSourceMatch.Text = "This is a perfect match !";
			this.labelTargetSourceMatch.Visible = false;
			// 
			// DeltaStepsBetweenEnvironments
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonChangeSource);
			this.Controls.Add(this.buttonLoadSolutions);
			this.Controls.Add(this.buttonCompare);
			this.Controls.Add(this.btnChangeTargetEnvironment);
			this.Controls.Add(this.labelTargetEnvironment);
			this.Controls.Add(this.labelSourceEnvironment);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboBoxSolutionsList);
			this.Controls.Add(this.toolStrip1);
			this.Name = "DeltaStepsBetweenEnvironments";
			this.Size = new System.Drawing.Size(1038, 613);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButtonClose;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxSolutionsList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelSourceEnvironment;
		private System.Windows.Forms.Label labelTargetEnvironment;
		private System.Windows.Forms.Button btnChangeTargetEnvironment;
		private System.Windows.Forms.Button buttonCompare;
		private System.Windows.Forms.ListBox listBoxTargetSource;
		private System.Windows.Forms.Button buttonLoadSolutions;
		private System.Windows.Forms.Button buttonChangeSource;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox listBoxSourceTarget;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label labelSourceTargetMatch;
		private System.Windows.Forms.Label labelTargetSourceMatch;
	}
}
