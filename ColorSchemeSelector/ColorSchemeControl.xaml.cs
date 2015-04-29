using System;
using System.Windows.Controls;
using System.Windows.Media;
using EnvDTE;

namespace ColorSchemeExtension
{
	/// <summary>
	/// Interaction logic for ColorSchemeControl.xaml
	/// </summary>
	public partial class ColorSchemeControl : UserControl
	{
		private ColorSchemeToolWindow _Parent = null;

		public ColorSchemeControl ( ColorSchemeToolWindow Parent )
		{
			InitializeComponent( );

			_Parent = Parent;

			SelectedColor.Background = new SolidColorBrush( Color.FromRgb( GBPart.R, GBPart.G, GBPart.B ) );

			RPart.PropertyChanged += RColor_PropertyChanged;
			GBPart.PropertyChanged += GBColor_PropertyChanged;
		}

		void GBColor_PropertyChanged ( object sender, System.ComponentModel.PropertyChangedEventArgs e )
		{
			SelectedColor.Background = new SolidColorBrush( Color.FromRgb( GBPart.R, GBPart.G, GBPart.B ) );
		}

		void RColor_PropertyChanged ( object sender, System.ComponentModel.PropertyChangedEventArgs e )
		{
			if ( e.PropertyName == "R" )
			{
				GBPart.R = ( ( ColorWheelR )sender ).R;
			}
		}

		protected object GetService ( Type service )
		{
			if ( _Parent != null )
			{
				return ( _Parent.GetVsService( service ) );
			}

			return ( null );
		}

		private void Button_Click ( object sender, System.Windows.RoutedEventArgs e )
		{
			EnvDTE80.DTE2 oApplication = ( EnvDTE80.DTE2 )GetService( typeof( EnvDTE.DTE ) );
			EnvDTE.Document oActive = oApplication.ActiveDocument;

			if ( oActive != null )
			{
				string szLanguage = oActive.Language;
				TextSelection oSelection = ( TextSelection )oActive.Selection;

				/*	Languages
				 * 
					JavaScript
					CSharp
					CSS
					XAML
					Basic
					HTMLX
					LESS
				*/

				oSelection.Text = "color value";
			}
		}
	}
}
