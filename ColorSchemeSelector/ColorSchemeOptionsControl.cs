using System;
using System.Windows.Forms;

namespace ColorSchemeExtension
{
	public partial class ColorSchemeOptionsControl : UserControl
	{
		ColorSchemeOptionsPage _Parent = null;

		public ColorSchemeOptionsControl ( ColorSchemeOptionsPage Parent )
		{
			InitializeComponent( );

			_Parent = Parent;

			Bind( );

			Format( );
		}

		private void btnAdd_Click ( object sender, EventArgs e )
		{
			_Parent.Color2CodeList.Add( new Color2CodeMap( )
			{
				Language = "[new language]",
				Template = "[code template]"
			} );
			
			Bind( );
		}

		private void btnDelete_Click ( object sender, EventArgs e )
		{
			if ( ColorMapGrid.CurrentRow == null )
			{
				MessageBox.Show( "You must select a row to delete it!", "Color Scheme Selector", MessageBoxButtons.OK );
			}
			else
			{
				if ( MessageBox.Show( string.Format( "You are going to delete the language '{0}'!{1}Are you sure?", ColorMapGrid.CurrentRow.Cells[ 0 ].Value, Environment.NewLine ), "Color Scheme Selector", MessageBoxButtons.YesNo ) == DialogResult.Yes )
				{
					_Parent.Color2CodeList.RemoveAt( ColorMapGrid.CurrentRow.Index );

					Bind( );
				}
			}
		}

		private void Bind ( )
		{
			ColorMapGrid.DataSource = null;
			ColorMapGrid.DataSource = _Parent.Color2CodeList;
			ColorMapGrid.CurrentCell = ColorMapGrid.Rows[ ColorMapGrid.Rows.Count - 1 ].Cells[ 0 ];
			ColorMapGrid.Rows[ ColorMapGrid.Rows.Count - 1 ].Selected = true;
		}

		private void Format ( )
		{
			foreach ( DataGridViewColumn oCol in ColorMapGrid.Columns )
			{
				oCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			}
		}
	}
}
