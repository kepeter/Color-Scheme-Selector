using System;
using System.Windows.Forms;

namespace ColorSchemeExtension
{
	public partial class ColorSchemeOptionsControl : UserControl
	{
		public ColorSchemeOptionsControl ( ColorSchemeOptionsPage Parent )
		{
			InitializeComponent( );

			ColorMapGrid.DataSource = Parent.Color2CodeList;
		}

		private void button1_Click ( object sender, EventArgs e )
		{
			//ColorMapGrid.Rows.Add( );
		}
	}
}
