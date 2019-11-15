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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeltaStepsBetweenEnvironments));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonChangeSource = new System.Windows.Forms.Button();
            this.btnChangeTargetEnvironment = new System.Windows.Forms.Button();
            this.labelTargetEnvironment = new System.Windows.Forms.Label();
            this.labelSourceEnvironment = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxSolutionComparing = new System.Windows.Forms.GroupBox();
            this.radioButtonCompareOrg = new System.Windows.Forms.RadioButton();
            this.radioButtonCompareAssembly = new System.Windows.Forms.RadioButton();
            this.radioButtonCompareSolution = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonLoadSolutionsAssemblies = new System.Windows.Forms.Button();
            this.buttonCompare = new System.Windows.Forms.Button();
            this.labelComparing = new System.Windows.Forms.Label();
            this.comboBoxSolutionsAssembliesList = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStepDetailsSourceToTarget = new System.Windows.Forms.Button();
            this.labelSourceTargetMatch = new System.Windows.Forms.Label();
            this.listViewSourceTarget = new System.Windows.Forms.ListView();
            this.columnHeaderSTStepName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSTEntity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSTMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSTModifiedOn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSTCreatedOn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStepDetailsTargetToSource = new System.Windows.Forms.Button();
            this.labelTargetSourceMatch = new System.Windows.Forms.Label();
            this.listViewTargetSource = new System.Windows.Forms.ListView();
            this.columnHeaderTSStepName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTSEntity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTSMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTSModifiedOn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTSCreatedOn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteStep = new System.Windows.Forms.Button();
            this.buttonCopySourceToTarget = new System.Windows.Forms.Button();
            this.buttonCopyTargetToSource = new System.Windows.Forms.Button();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipWarning = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.checkBoxCompareByName = new System.Windows.Forms.CheckBox();
            this.checkBoxCompareByGuid = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxSolutionComparing.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonClose,
            this.toolStripSeparator1,
            this.toolStripButtonOptions,
            this.toolStripSeparator3,
            this.toolStripButtonExport,
            this.toolStripSeparator2,
            this.toolStripButtonHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1903, 40);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonClose
            // 
            this.toolStripButtonClose.Image = global::Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Properties.Resources.close;
            this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClose.Name = "toolStripButtonClose";
            this.toolStripButtonClose.Size = new System.Drawing.Size(95, 34);
            this.toolStripButtonClose.Text = "Close";
            this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButtonOptions
            // 
            this.toolStripButtonOptions.Image = global::Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Properties.Resources.gear;
            this.toolStripButtonOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOptions.Name = "toolStripButtonOptions";
            this.toolStripButtonOptions.Size = new System.Drawing.Size(118, 34);
            this.toolStripButtonOptions.Text = "Options";
            this.toolStripButtonOptions.Click += new System.EventHandler(this.toolStripButtonOptions_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButtonExport
            // 
            this.toolStripButtonExport.Enabled = false;
            this.toolStripButtonExport.Image = global::Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Properties.Resources.Export;
            this.toolStripButtonExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExport.Name = "toolStripButtonExport";
            this.toolStripButtonExport.Size = new System.Drawing.Size(104, 34);
            this.toolStripButtonExport.Text = "Export";
            this.toolStripButtonExport.Click += new System.EventHandler(this.ToolStripButtonExport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButtonHelp
            // 
            this.toolStripButtonHelp.Image = global::Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Properties.Resources.help;
            this.toolStripButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHelp.Name = "toolStripButtonHelp";
            this.toolStripButtonHelp.Size = new System.Drawing.Size(88, 34);
            this.toolStripButtonHelp.Text = "Help";
            this.toolStripButtonHelp.Click += new System.EventHandler(this.toolStripButtonHelp_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.78049F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.21951F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSolutionComparing, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 68);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1879, 212);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonChangeSource);
            this.groupBox3.Controls.Add(this.btnChangeTargetEnvironment);
            this.groupBox3.Controls.Add(this.labelTargetEnvironment);
            this.groupBox3.Controls.Add(this.labelSourceEnvironment);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox3.Size = new System.Drawing.Size(679, 200);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Environments configuration";
            // 
            // buttonChangeSource
            // 
            this.buttonChangeSource.Location = new System.Drawing.Point(466, 50);
            this.buttonChangeSource.Margin = new System.Windows.Forms.Padding(6);
            this.buttonChangeSource.Name = "buttonChangeSource";
            this.buttonChangeSource.Size = new System.Drawing.Size(191, 42);
            this.buttonChangeSource.TabIndex = 32;
            this.buttonChangeSource.Text = "Change Source";
            this.buttonChangeSource.UseVisualStyleBackColor = true;
            this.buttonChangeSource.Click += new System.EventHandler(this.buttonChangeSource_Click);
            // 
            // btnChangeTargetEnvironment
            // 
            this.btnChangeTargetEnvironment.Location = new System.Drawing.Point(466, 100);
            this.btnChangeTargetEnvironment.Margin = new System.Windows.Forms.Padding(6);
            this.btnChangeTargetEnvironment.Name = "btnChangeTargetEnvironment";
            this.btnChangeTargetEnvironment.Size = new System.Drawing.Size(191, 42);
            this.btnChangeTargetEnvironment.TabIndex = 31;
            this.btnChangeTargetEnvironment.Text = "Change Target";
            this.btnChangeTargetEnvironment.UseVisualStyleBackColor = true;
            this.btnChangeTargetEnvironment.Click += new System.EventHandler(this.btnChangeTargetEnvironment_Click);
            // 
            // labelTargetEnvironment
            // 
            this.labelTargetEnvironment.AutoSize = true;
            this.labelTargetEnvironment.BackColor = System.Drawing.SystemColors.Control;
            this.labelTargetEnvironment.ForeColor = System.Drawing.Color.Red;
            this.labelTargetEnvironment.Location = new System.Drawing.Point(224, 109);
            this.labelTargetEnvironment.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelTargetEnvironment.Name = "labelTargetEnvironment";
            this.labelTargetEnvironment.Size = new System.Drawing.Size(59, 25);
            this.labelTargetEnvironment.TabIndex = 30;
            this.labelTargetEnvironment.Text = "None";
            // 
            // labelSourceEnvironment
            // 
            this.labelSourceEnvironment.AutoSize = true;
            this.labelSourceEnvironment.Location = new System.Drawing.Point(224, 59);
            this.labelSourceEnvironment.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelSourceEnvironment.Name = "labelSourceEnvironment";
            this.labelSourceEnvironment.Size = new System.Drawing.Size(59, 25);
            this.labelSourceEnvironment.TabIndex = 29;
            this.labelSourceEnvironment.Text = "None";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 109);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 25);
            this.label2.TabIndex = 28;
            this.label2.Text = "Target Environment : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 25);
            this.label1.TabIndex = 27;
            this.label1.Text = "Source Environment : ";
            // 
            // groupBoxSolutionComparing
            // 
            this.groupBoxSolutionComparing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSolutionComparing.Controls.Add(this.checkBoxCompareByGuid);
            this.groupBoxSolutionComparing.Controls.Add(this.checkBoxCompareByName);
            this.groupBoxSolutionComparing.Controls.Add(this.radioButtonCompareOrg);
            this.groupBoxSolutionComparing.Controls.Add(this.radioButtonCompareAssembly);
            this.groupBoxSolutionComparing.Controls.Add(this.radioButtonCompareSolution);
            this.groupBoxSolutionComparing.Controls.Add(this.label3);
            this.groupBoxSolutionComparing.Controls.Add(this.buttonLoadSolutionsAssemblies);
            this.groupBoxSolutionComparing.Controls.Add(this.buttonCompare);
            this.groupBoxSolutionComparing.Controls.Add(this.labelComparing);
            this.groupBoxSolutionComparing.Controls.Add(this.comboBoxSolutionsAssembliesList);
            this.groupBoxSolutionComparing.Location = new System.Drawing.Point(697, 6);
            this.groupBoxSolutionComparing.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxSolutionComparing.Name = "groupBoxSolutionComparing";
            this.groupBoxSolutionComparing.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxSolutionComparing.Size = new System.Drawing.Size(1176, 200);
            this.groupBoxSolutionComparing.TabIndex = 1;
            this.groupBoxSolutionComparing.TabStop = false;
            this.groupBoxSolutionComparing.Text = "Details selection";
            // 
            // radioButtonCompareOrg
            // 
            this.radioButtonCompareOrg.AutoSize = true;
            this.radioButtonCompareOrg.Checked = true;
            this.radioButtonCompareOrg.Location = new System.Drawing.Point(11, 74);
            this.radioButtonCompareOrg.Margin = new System.Windows.Forms.Padding(6);
            this.radioButtonCompareOrg.Name = "radioButtonCompareOrg";
            this.radioButtonCompareOrg.Size = new System.Drawing.Size(125, 29);
            this.radioButtonCompareOrg.TabIndex = 33;
            this.radioButtonCompareOrg.Text = "Entire Org";
            this.radioButtonCompareOrg.UseVisualStyleBackColor = true;
            this.radioButtonCompareOrg.CheckedChanged += new System.EventHandler(this.RadioButtonCompareOrg_CheckedChanged);
            // 
            // radioButtonCompareAssembly
            // 
            this.radioButtonCompareAssembly.AutoSize = true;
            this.radioButtonCompareAssembly.Location = new System.Drawing.Point(11, 159);
            this.radioButtonCompareAssembly.Margin = new System.Windows.Forms.Padding(6);
            this.radioButtonCompareAssembly.Name = "radioButtonCompareAssembly";
            this.radioButtonCompareAssembly.Size = new System.Drawing.Size(123, 29);
            this.radioButtonCompareAssembly.TabIndex = 32;
            this.radioButtonCompareAssembly.Text = "Assembly";
            this.radioButtonCompareAssembly.UseVisualStyleBackColor = true;
            this.radioButtonCompareAssembly.Click += new System.EventHandler(this.radioButtonCompareAssembly_Click);
            // 
            // radioButtonCompareSolution
            // 
            this.radioButtonCompareSolution.AutoSize = true;
            this.radioButtonCompareSolution.Location = new System.Drawing.Point(11, 116);
            this.radioButtonCompareSolution.Margin = new System.Windows.Forms.Padding(6);
            this.radioButtonCompareSolution.Name = "radioButtonCompareSolution";
            this.radioButtonCompareSolution.Size = new System.Drawing.Size(108, 29);
            this.radioButtonCompareSolution.TabIndex = 31;
            this.radioButtonCompareSolution.Text = "Solution";
            this.radioButtonCompareSolution.UseVisualStyleBackColor = true;
            this.radioButtonCompareSolution.Click += new System.EventHandler(this.radioButtonCompareSolution_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 25);
            this.label3.TabIndex = 30;
            this.label3.Text = "Compare from : ";
            // 
            // buttonLoadSolutionsAssemblies
            // 
            this.buttonLoadSolutionsAssemblies.Location = new System.Drawing.Point(937, 48);
            this.buttonLoadSolutionsAssemblies.Margin = new System.Windows.Forms.Padding(6);
            this.buttonLoadSolutionsAssemblies.Name = "buttonLoadSolutionsAssemblies";
            this.buttonLoadSolutionsAssemblies.Size = new System.Drawing.Size(229, 42);
            this.buttonLoadSolutionsAssemblies.TabIndex = 29;
            this.buttonLoadSolutionsAssemblies.Text = "Load Solutions";
            this.toolTipInfo.SetToolTip(this.buttonLoadSolutionsAssemblies, "Load the solutions/assemblies from the source environment.");
            this.buttonLoadSolutionsAssemblies.UseVisualStyleBackColor = true;
            this.buttonLoadSolutionsAssemblies.Click += new System.EventHandler(this.buttonLoadSolutionsAssemblies_Click);
            // 
            // buttonCompare
            // 
            this.buttonCompare.Location = new System.Drawing.Point(204, 105);
            this.buttonCompare.Margin = new System.Windows.Forms.Padding(6);
            this.buttonCompare.Name = "buttonCompare";
            this.buttonCompare.Size = new System.Drawing.Size(667, 40);
            this.buttonCompare.TabIndex = 28;
            this.buttonCompare.Text = "Compare the two environments";
            this.toolTipInfo.SetToolTip(this.buttonCompare, "Comparing the solution/assembly in order to find out the differences.");
            this.buttonCompare.UseVisualStyleBackColor = true;
            this.buttonCompare.Click += new System.EventHandler(this.buttonCompare_Click);
            // 
            // labelComparing
            // 
            this.labelComparing.AutoSize = true;
            this.labelComparing.Location = new System.Drawing.Point(198, 59);
            this.labelComparing.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelComparing.Name = "labelComparing";
            this.labelComparing.Size = new System.Drawing.Size(284, 25);
            this.labelComparing.TabIndex = 27;
            this.labelComparing.Text = "Select the solution to compare :";
            // 
            // comboBoxSolutionsAssembliesList
            // 
            this.comboBoxSolutionsAssembliesList.FormattingEnabled = true;
            this.comboBoxSolutionsAssembliesList.ItemHeight = 24;
            this.comboBoxSolutionsAssembliesList.Location = new System.Drawing.Point(507, 50);
            this.comboBoxSolutionsAssembliesList.Margin = new System.Windows.Forms.Padding(6);
            this.comboBoxSolutionsAssembliesList.Name = "comboBoxSolutionsAssembliesList";
            this.comboBoxSolutionsAssembliesList.Size = new System.Drawing.Size(425, 32);
            this.comboBoxSolutionsAssembliesList.TabIndex = 26;
            this.comboBoxSolutionsAssembliesList.SelectedIndexChanged += new System.EventHandler(this.comboBoxSolutionsList_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox4, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 292);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1879, 820);
            this.tableLayoutPanel2.TabIndex = 30;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnStepDetailsSourceToTarget);
            this.groupBox1.Controls.Add(this.labelSourceTargetMatch);
            this.groupBox1.Controls.Add(this.listViewSourceTarget);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(863, 808);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Steps which are on Source but not on Target environment";
            // 
            // btnStepDetailsSourceToTarget
            // 
            this.btnStepDetailsSourceToTarget.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStepDetailsSourceToTarget.Enabled = false;
            this.btnStepDetailsSourceToTarget.Image = global::Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Properties.Resources.details;
            this.btnStepDetailsSourceToTarget.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnStepDetailsSourceToTarget.Location = new System.Drawing.Point(17, 27);
            this.btnStepDetailsSourceToTarget.Margin = new System.Windows.Forms.Padding(6);
            this.btnStepDetailsSourceToTarget.Name = "btnStepDetailsSourceToTarget";
            this.btnStepDetailsSourceToTarget.Size = new System.Drawing.Size(195, 47);
            this.btnStepDetailsSourceToTarget.TabIndex = 3;
            this.btnStepDetailsSourceToTarget.Text = "Step Details";
            this.btnStepDetailsSourceToTarget.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTipWarning.SetToolTip(this.btnStepDetailsSourceToTarget, "Delete the selected step(s), it\'s permanent !");
            this.btnStepDetailsSourceToTarget.UseVisualStyleBackColor = true;
            this.btnStepDetailsSourceToTarget.Visible = false;
            this.btnStepDetailsSourceToTarget.Click += new System.EventHandler(this.BtnStepDetailsSourceToTarget_Click);
            // 
            // labelSourceTargetMatch
            // 
            this.labelSourceTargetMatch.AutoSize = true;
            this.labelSourceTargetMatch.BackColor = System.Drawing.Color.White;
            this.labelSourceTargetMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceTargetMatch.ForeColor = System.Drawing.Color.Green;
            this.labelSourceTargetMatch.Location = new System.Drawing.Point(143, 377);
            this.labelSourceTargetMatch.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelSourceTargetMatch.Name = "labelSourceTargetMatch";
            this.labelSourceTargetMatch.Size = new System.Drawing.Size(517, 54);
            this.labelSourceTargetMatch.TabIndex = 27;
            this.labelSourceTargetMatch.Text = "This is a perfect match !";
            this.labelSourceTargetMatch.Visible = false;
            // 
            // listViewSourceTarget
            // 
            this.listViewSourceTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSourceTarget.CheckBoxes = true;
            this.listViewSourceTarget.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSTStepName,
            this.columnHeaderSTEntity,
            this.columnHeaderSTMessage,
            this.columnHeaderSTModifiedOn,
            this.columnHeaderSTCreatedOn});
            this.listViewSourceTarget.HideSelection = false;
            this.listViewSourceTarget.Location = new System.Drawing.Point(17, 27);
            this.listViewSourceTarget.Margin = new System.Windows.Forms.Padding(6);
            this.listViewSourceTarget.Name = "listViewSourceTarget";
            this.listViewSourceTarget.Size = new System.Drawing.Size(832, 766);
            this.listViewSourceTarget.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewSourceTarget.TabIndex = 26;
            this.listViewSourceTarget.UseCompatibleStateImageBehavior = false;
            this.listViewSourceTarget.View = System.Windows.Forms.View.Details;
            this.listViewSourceTarget.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewSourceTarget_ColumnClick);
            this.listViewSourceTarget.SelectedIndexChanged += new System.EventHandler(this.ListViewSourceTarget_SelectedIndexChanged);
            this.listViewSourceTarget.DoubleClick += new System.EventHandler(this.ListViewSourceTarget_DoubleClick);
            // 
            // columnHeaderSTStepName
            // 
            this.columnHeaderSTStepName.Text = "Step Name";
            this.columnHeaderSTStepName.Width = 86;
            // 
            // columnHeaderSTEntity
            // 
            this.columnHeaderSTEntity.Text = "Entity";
            // 
            // columnHeaderSTMessage
            // 
            this.columnHeaderSTMessage.Text = "Message";
            // 
            // columnHeaderSTModifiedOn
            // 
            this.columnHeaderSTModifiedOn.Text = "Modified On";
            this.columnHeaderSTModifiedOn.Width = 81;
            // 
            // columnHeaderSTCreatedOn
            // 
            this.columnHeaderSTCreatedOn.Text = "Created On";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnStepDetailsTargetToSource);
            this.groupBox2.Controls.Add(this.labelTargetSourceMatch);
            this.groupBox2.Controls.Add(this.listViewTargetSource);
            this.groupBox2.Location = new System.Drawing.Point(1009, 6);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox2.Size = new System.Drawing.Size(864, 808);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Steps which are on Target but not on Source environment";
            // 
            // btnStepDetailsTargetToSource
            // 
            this.btnStepDetailsTargetToSource.Enabled = false;
            this.btnStepDetailsTargetToSource.Image = global::Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Properties.Resources.details;
            this.btnStepDetailsTargetToSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStepDetailsTargetToSource.Location = new System.Drawing.Point(11, 27);
            this.btnStepDetailsTargetToSource.Margin = new System.Windows.Forms.Padding(6);
            this.btnStepDetailsTargetToSource.Name = "btnStepDetailsTargetToSource";
            this.btnStepDetailsTargetToSource.Size = new System.Drawing.Size(195, 47);
            this.btnStepDetailsTargetToSource.TabIndex = 28;
            this.btnStepDetailsTargetToSource.Text = "Step Details";
            this.btnStepDetailsTargetToSource.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTipWarning.SetToolTip(this.btnStepDetailsTargetToSource, "Delete the selected step(s), it\'s permanent !");
            this.btnStepDetailsTargetToSource.UseVisualStyleBackColor = true;
            this.btnStepDetailsTargetToSource.Visible = false;
            this.btnStepDetailsTargetToSource.Click += new System.EventHandler(this.BtnStepDetailsTargetToSource_Click);
            // 
            // labelTargetSourceMatch
            // 
            this.labelTargetSourceMatch.AutoSize = true;
            this.labelTargetSourceMatch.BackColor = System.Drawing.Color.White;
            this.labelTargetSourceMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTargetSourceMatch.ForeColor = System.Drawing.Color.Green;
            this.labelTargetSourceMatch.Location = new System.Drawing.Point(156, 377);
            this.labelTargetSourceMatch.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelTargetSourceMatch.Name = "labelTargetSourceMatch";
            this.labelTargetSourceMatch.Size = new System.Drawing.Size(517, 54);
            this.labelTargetSourceMatch.TabIndex = 28;
            this.labelTargetSourceMatch.Text = "This is a perfect match !";
            this.labelTargetSourceMatch.Visible = false;
            // 
            // listViewTargetSource
            // 
            this.listViewTargetSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTargetSource.CheckBoxes = true;
            this.listViewTargetSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTSStepName,
            this.columnHeaderTSEntity,
            this.columnHeaderTSMessage,
            this.columnHeaderTSModifiedOn,
            this.columnHeaderTSCreatedOn});
            this.listViewTargetSource.HideSelection = false;
            this.listViewTargetSource.Location = new System.Drawing.Point(11, 27);
            this.listViewTargetSource.Margin = new System.Windows.Forms.Padding(6);
            this.listViewTargetSource.Name = "listViewTargetSource";
            this.listViewTargetSource.Size = new System.Drawing.Size(839, 766);
            this.listViewTargetSource.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewTargetSource.TabIndex = 27;
            this.listViewTargetSource.UseCompatibleStateImageBehavior = false;
            this.listViewTargetSource.View = System.Windows.Forms.View.Details;
            this.listViewTargetSource.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewTargetSource_ColumnClick);
            this.listViewTargetSource.SelectedIndexChanged += new System.EventHandler(this.ListViewTargetSource_SelectedIndexChanged);
            // 
            // columnHeaderTSStepName
            // 
            this.columnHeaderTSStepName.Text = "Step Name";
            this.columnHeaderTSStepName.Width = 82;
            // 
            // columnHeaderTSEntity
            // 
            this.columnHeaderTSEntity.Text = "Entity";
            // 
            // columnHeaderTSMessage
            // 
            this.columnHeaderTSMessage.Text = "Message";
            // 
            // columnHeaderTSModifiedOn
            // 
            this.columnHeaderTSModifiedOn.DisplayIndex = 4;
            this.columnHeaderTSModifiedOn.Text = "Modified On";
            this.columnHeaderTSModifiedOn.Width = 77;
            // 
            // columnHeaderTSCreatedOn
            // 
            this.columnHeaderTSCreatedOn.DisplayIndex = 3;
            this.columnHeaderTSCreatedOn.Text = "Created On";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.buttonDeleteStep);
            this.groupBox4.Controls.Add(this.buttonCopySourceToTarget);
            this.groupBox4.Controls.Add(this.buttonCopyTargetToSource);
            this.groupBox4.Location = new System.Drawing.Point(881, 6);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox4.Size = new System.Drawing.Size(116, 808);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Actions";
            // 
            // buttonDeleteStep
            // 
            this.buttonDeleteStep.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteStep.Image")));
            this.buttonDeleteStep.Location = new System.Drawing.Point(22, 198);
            this.buttonDeleteStep.Margin = new System.Windows.Forms.Padding(6);
            this.buttonDeleteStep.Name = "buttonDeleteStep";
            this.buttonDeleteStep.Size = new System.Drawing.Size(77, 66);
            this.buttonDeleteStep.TabIndex = 2;
            this.toolTipWarning.SetToolTip(this.buttonDeleteStep, "Delete the selected step(s), it\'s permanent !");
            this.buttonDeleteStep.UseVisualStyleBackColor = true;
            this.buttonDeleteStep.Click += new System.EventHandler(this.buttonDeleteStep_Click);
            // 
            // buttonCopySourceToTarget
            // 
            this.buttonCopySourceToTarget.Image = global::Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Properties.Resources.move_right;
            this.buttonCopySourceToTarget.Location = new System.Drawing.Point(20, 118);
            this.buttonCopySourceToTarget.Margin = new System.Windows.Forms.Padding(6);
            this.buttonCopySourceToTarget.Name = "buttonCopySourceToTarget";
            this.buttonCopySourceToTarget.Size = new System.Drawing.Size(79, 68);
            this.buttonCopySourceToTarget.TabIndex = 1;
            this.toolTipInfo.SetToolTip(this.buttonCopySourceToTarget, "Copy your step from Source to Target environment.");
            this.buttonCopySourceToTarget.UseVisualStyleBackColor = true;
            this.buttonCopySourceToTarget.Click += new System.EventHandler(this.buttonCopySourceToTarget_Click);
            // 
            // buttonCopyTargetToSource
            // 
            this.buttonCopyTargetToSource.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopyTargetToSource.Image")));
            this.buttonCopyTargetToSource.Location = new System.Drawing.Point(22, 39);
            this.buttonCopyTargetToSource.Margin = new System.Windows.Forms.Padding(6);
            this.buttonCopyTargetToSource.Name = "buttonCopyTargetToSource";
            this.buttonCopyTargetToSource.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonCopyTargetToSource.Size = new System.Drawing.Size(79, 68);
            this.buttonCopyTargetToSource.TabIndex = 0;
            this.toolTipInfo.SetToolTip(this.buttonCopyTargetToSource, "Copy your step from Target to Source environment.");
            this.buttonCopyTargetToSource.UseVisualStyleBackColor = true;
            this.buttonCopyTargetToSource.Click += new System.EventHandler(this.buttonCopyTargetToSource_Click);
            // 
            // toolTipInfo
            // 
            this.toolTipInfo.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipInfo.ToolTipTitle = "Information";
            // 
            // toolTipWarning
            // 
            this.toolTipWarning.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTipWarning.ToolTipTitle = "Warning";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "csv";
            this.saveFileDialog1.Filter = "\"Comma Seperated|*.csv\"";
            this.saveFileDialog1.Title = "Save As";
            // 
            // checkBoxCompareByName
            // 
            this.checkBoxCompareByName.AutoSize = true;
            this.checkBoxCompareByName.Location = new System.Drawing.Point(899, 134);
            this.checkBoxCompareByName.Name = "checkBoxCompareByName";
            this.checkBoxCompareByName.Size = new System.Drawing.Size(162, 29);
            this.checkBoxCompareByName.TabIndex = 34;
            this.checkBoxCompareByName.Text = "by Step Name";
            this.checkBoxCompareByName.UseVisualStyleBackColor = true;
            this.checkBoxCompareByName.Visible = false;
            this.checkBoxCompareByName.CheckedChanged += new System.EventHandler(this.checkBoxCompareByName_CheckedChanged);
            // 
            // checkBoxCompareByGuid
            // 
            this.checkBoxCompareByGuid.AutoSize = true;
            this.checkBoxCompareByGuid.Checked = true;
            this.checkBoxCompareByGuid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCompareByGuid.Location = new System.Drawing.Point(899, 100);
            this.checkBoxCompareByGuid.Name = "checkBoxCompareByGuid";
            this.checkBoxCompareByGuid.Size = new System.Drawing.Size(151, 29);
            this.checkBoxCompareByGuid.TabIndex = 35;
            this.checkBoxCompareByGuid.Text = "by Step Guid";
            this.checkBoxCompareByGuid.UseVisualStyleBackColor = true;
            this.checkBoxCompareByGuid.Visible = false;
            this.checkBoxCompareByGuid.CheckedChanged += new System.EventHandler(this.checkBoxCompareByGuid_CheckedChanged);
            // 
            // DeltaStepsBetweenEnvironments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "DeltaStepsBetweenEnvironments";
            this.Size = new System.Drawing.Size(1903, 1132);
            this.Load += new System.EventHandler(this.DeltaStepsBetweenEnvironments_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxSolutionComparing.ResumeLayout(false);
            this.groupBoxSolutionComparing.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButtonClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonChangeSource;
        private System.Windows.Forms.Button btnChangeTargetEnvironment;
        private System.Windows.Forms.Label labelTargetEnvironment;
        private System.Windows.Forms.Label labelSourceEnvironment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxSolutionComparing;
        private System.Windows.Forms.Button buttonLoadSolutionsAssemblies;
        private System.Windows.Forms.Button buttonCompare;
        private System.Windows.Forms.Label labelComparing;
        private System.Windows.Forms.ComboBox comboBoxSolutionsAssembliesList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonCopySourceToTarget;
        private System.Windows.Forms.Button buttonCopyTargetToSource;
        private System.Windows.Forms.ToolStripButton toolStripButtonOptions;
        private System.Windows.Forms.RadioButton radioButtonCompareAssembly;
        private System.Windows.Forms.RadioButton radioButtonCompareSolution;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listViewSourceTarget;
        private System.Windows.Forms.ColumnHeader columnHeaderSTStepName;
        private System.Windows.Forms.ColumnHeader columnHeaderSTEntity;
        private System.Windows.Forms.ColumnHeader columnHeaderSTMessage;
        private System.Windows.Forms.ColumnHeader columnHeaderSTModifiedOn;
        private System.Windows.Forms.ColumnHeader columnHeaderSTCreatedOn;
        private System.Windows.Forms.ListView listViewTargetSource;
        private System.Windows.Forms.ColumnHeader columnHeaderTSStepName;
        private System.Windows.Forms.ColumnHeader columnHeaderTSEntity;
        private System.Windows.Forms.ColumnHeader columnHeaderTSMessage;
        private System.Windows.Forms.ColumnHeader columnHeaderTSModifiedOn;
        private System.Windows.Forms.ColumnHeader columnHeaderTSCreatedOn;
        private System.Windows.Forms.Label labelSourceTargetMatch;
        private System.Windows.Forms.Label labelTargetSourceMatch;
        private System.Windows.Forms.Button buttonDeleteStep;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.ToolTip toolTipWarning;
        private System.Windows.Forms.RadioButton radioButtonCompareOrg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnStepDetailsSourceToTarget;
        private System.Windows.Forms.Button btnStepDetailsTargetToSource;
        private System.Windows.Forms.CheckBox checkBoxCompareByGuid;
        private System.Windows.Forms.CheckBox checkBoxCompareByName;
    }
}
