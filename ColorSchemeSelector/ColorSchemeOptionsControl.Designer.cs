namespace ColorSchemeExtension
{
	partial class ColorSchemeOptionsControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose ( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose( );
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ( )
		{
			this.ColorMapGrid = new System.Windows.Forms.DataGridView();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.ColorMapGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// ColorMapGrid
			// 
			this.ColorMapGrid.AllowUserToResizeRows = false;
			this.ColorMapGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ColorMapGrid.Location = new System.Drawing.Point(12, 12);
			this.ColorMapGrid.MultiSelect = false;
			this.ColorMapGrid.Name = "ColorMapGrid";
			this.ColorMapGrid.Size = new System.Drawing.Size(360, 240);
			this.ColorMapGrid.TabIndex = 0;
			this.ColorMapGrid.VirtualMode = true;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(297, 258);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 23);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.Add_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(216, 258);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 2;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.Delete_Click);
			// 
			// ColorSchemeOptionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.ColorMapGrid);
			this.Name = "ColorSchemeOptionsControl";
			this.Size = new System.Drawing.Size(675, 450);
			((System.ComponentModel.ISupportInitialize)(this.ColorMapGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView ColorMapGrid;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
	}
}
