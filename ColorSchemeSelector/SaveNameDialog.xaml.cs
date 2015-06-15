using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ColorSchemeExtension
{
	/// <summary>
	/// Interaction logic for SaveNameDialog.xaml
	/// </summary>
	public partial class SaveNameDialog : Window
	{
		public SaveNameDialog ( Color Color )
		{
			InitializeComponent( );

			SaveName.Text = Color.ToString( );
			SaveName.SelectAll( );
			SaveName.Focus( );

			SelectedColor.Background = new SolidColorBrush( Color );
		}

		private void Button_Click ( object sender, RoutedEventArgs e )
		{
			this.DialogResult = !string.IsNullOrEmpty( SaveName.Text );
		}
	}
}
