using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorSchemeExtension
{
	public partial class ColorSchemeOptionsControl : UserControl
	{
		public ColorSchemeOptionsControl ( ColorSchemeOptionsPage Parent )
		{
			InitializeComponent( );

			ColorMapGrid.DataSource = Parent.Color2CodeMap;
		}

		private void button1_Click ( object sender, EventArgs e )
		{
			//ColorMapGrid.Rows.Add( );
		}
	}
}
