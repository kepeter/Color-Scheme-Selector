using System.Windows;
using System.Windows.Media;

namespace ColorSchemeExtension
{
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
