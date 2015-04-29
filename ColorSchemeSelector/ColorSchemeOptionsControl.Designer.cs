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
			this.button1 = new System.Windows.Forms.Button();
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
			this.ColorMapGrid.Size = new System.Drawing.Size(328, 204);
			this.ColorMapGrid.TabIndex = 0;
			this.ColorMapGrid.VirtualMode = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(265, 222);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// ColorSchemeOptionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.ColorMapGrid);
			this.Name = "ColorSchemeOptionsControl";
			this.Size = new System.Drawing.Size(675, 450);
			((System.ComponentModel.ISupportInitialize)(this.ColorMapGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView ColorMapGrid;
		private System.Windows.Forms.Button button1;
	}
}
