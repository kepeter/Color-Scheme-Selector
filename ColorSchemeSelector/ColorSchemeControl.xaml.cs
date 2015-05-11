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

			var oButtons = new Array[ 8 ] {	
				new Button[ 2 ] { Complements_0_0, Complements_0_1 },
				new Button[ 3 ] { Split_0_0, Split_0_1, Split_0_2 },
				new Button[ 3 ] { Triads_0_0, Triads_0_1, Triads_0_2 },
				new Button[ 4 ] { Tetrads_0_0, Tetrads_0_1, Tetrads_0_2, Tetrads_0_3 },
				new Button[ 5 ] { Quintads_0_0, Quintads_0_1, Quintads_0_2, Quintads_0_3, Quintads_0_4 },
				new Button[ 3 ] { Analogous_0_0, Analogous_0_1, Analogous_0_2 },
				new Button[ 5 ] { Monochromatics_0_0, Monochromatics_0_1, Monochromatics_0_2, Monochromatics_0_3, Monochromatics_0_4 },
				new Array[ 5 ] {
					new Button[ 5 ] { Combinations_0_0, Combinations_0_1, Combinations_0_2, Combinations_0_3, Combinations_0_4 },
					new Button[ 5 ] { Combinations_1_0, Combinations_1_1, Combinations_1_2, Combinations_1_3, Combinations_1_4 },
					new Button[ 5 ] { Combinations_2_0, Combinations_2_1, Combinations_2_2, Combinations_2_3, Combinations_2_4 },
					new Button[ 5 ] { Combinations_3_0, Combinations_3_1, Combinations_3_2, Combinations_3_3, Combinations_3_4 },
					new Button[ 5 ] { Combinations_4_0, Combinations_4_1, Combinations_4_2, Combinations_4_3, Combinations_4_4 }
				}
			};
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
