using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
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
		private ColorSchemeOptionsPage _OptionPage = null;
		private object _Buttons = null;

		public ColorSchemeControl ( ColorSchemeToolWindow Parent )
		{
			InitializeComponent( );

			_Parent = Parent;

			SelectedColor.Background = new SolidColorBrush( Color.FromRgb( GBPart.R, GBPart.G, GBPart.B ) );

			RPart.PropertyChanged += RColor_PropertyChanged;
			GBPart.PropertyChanged += GBColor_PropertyChanged;

			_Buttons = new Array[ 8 ] {	
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

			FillColorButtons( Color.FromRgb( GBPart.R, GBPart.G, GBPart.B ) );
		}

		private void GBColor_PropertyChanged ( object sender, System.ComponentModel.PropertyChangedEventArgs e )
		{
			FillColorButtons( Color.FromRgb( GBPart.R, GBPart.G, GBPart.B ) );
		}

		private void RColor_PropertyChanged ( object sender, System.ComponentModel.PropertyChangedEventArgs e )
		{
			if ( e.PropertyName == "R" )
			{
				GBPart.R = ( ( ColorWheelR )sender ).R;
			}
		}

		private void FillColorButtons ( Color Color )
		{
			Complements_0_0.Background = new SolidColorBrush( Color );
			Complements_0_1.Background = new SolidColorBrush( Color );

			Split_0_0.Background = new SolidColorBrush( Color );
			Split_0_1.Background = new SolidColorBrush( Color );
			Split_0_2.Background = new SolidColorBrush( Color );

			Triads_0_0.Background = new SolidColorBrush( Color );
			Triads_0_1.Background = new SolidColorBrush( Color );
			Triads_0_2.Background = new SolidColorBrush( Color );

			Tetrads_0_0.Background = new SolidColorBrush( Color );
			Tetrads_0_1.Background = new SolidColorBrush( Color );
			Tetrads_0_2.Background = new SolidColorBrush( Color );
			Tetrads_0_3.Background = new SolidColorBrush( Color );

			Quintads_0_0.Background = new SolidColorBrush( Color );
			Quintads_0_1.Background = new SolidColorBrush( Color );
			Quintads_0_2.Background = new SolidColorBrush( Color );
			Quintads_0_3.Background = new SolidColorBrush( Color );
			Quintads_0_4.Background = new SolidColorBrush( Color );

			Analogous_0_0.Background = new SolidColorBrush( Color );
			Analogous_0_1.Background = new SolidColorBrush( Color );
			Analogous_0_2.Background = new SolidColorBrush( Color );

			Monochromatics_0_0.Background = new SolidColorBrush( Color );
			Monochromatics_0_1.Background = new SolidColorBrush( Color );
			Monochromatics_0_2.Background = new SolidColorBrush( Color );
			Monochromatics_0_3.Background = new SolidColorBrush( Color );
			Monochromatics_0_4.Background = new SolidColorBrush( Color );

			Combinations_0_0.Background = new SolidColorBrush( Color );
			Combinations_0_1.Background = new SolidColorBrush( Color );
			Combinations_0_2.Background = new SolidColorBrush( Color );
			Combinations_0_3.Background = new SolidColorBrush( Color );
			Combinations_0_4.Background = new SolidColorBrush( Color );

			Combinations_1_0.Background = new SolidColorBrush( Color );
			Combinations_1_1.Background = new SolidColorBrush( Color );
			Combinations_1_2.Background = new SolidColorBrush( Color );
			Combinations_1_3.Background = new SolidColorBrush( Color );
			Combinations_1_4.Background = new SolidColorBrush( Color );

			Combinations_2_0.Background = new SolidColorBrush( Color );
			Combinations_2_1.Background = new SolidColorBrush( Color );
			Combinations_2_2.Background = new SolidColorBrush( Color );
			Combinations_2_3.Background = new SolidColorBrush( Color );
			Combinations_2_4.Background = new SolidColorBrush( Color );

			Combinations_3_0.Background = new SolidColorBrush( Color );
			Combinations_3_1.Background = new SolidColorBrush( Color );
			Combinations_3_2.Background = new SolidColorBrush( Color );
			Combinations_3_3.Background = new SolidColorBrush( Color );
			Combinations_3_4.Background = new SolidColorBrush( Color );

			Combinations_4_0.Background = new SolidColorBrush( Color );
			Combinations_4_1.Background = new SolidColorBrush( Color );
			Combinations_4_2.Background = new SolidColorBrush( Color );
			Combinations_4_3.Background = new SolidColorBrush( Color );
			Combinations_4_4.Background = new SolidColorBrush( Color );
		}

		private object GetService ( Type service )
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

			if ( _OptionPage == null )
			{
				_OptionPage = new ColorSchemeOptionsPage( );
				PropertyInfo[ ] oPropertyInfoArray = _OptionPage.GetType( ).GetProperties( );
				Properties oProperties = oApplication.get_Properties( "Custom Tools", "Color Scheme Selector" );

				foreach ( PropertyInfo oPropertyInfo in oPropertyInfoArray )
				{
					if ( oPropertyInfo.ReflectedType == oPropertyInfo.DeclaringType )
					{
						oPropertyInfo.SetValue( _OptionPage, oProperties.Item( oPropertyInfo.Name ).Value, null );
					}
				}
			}

			if ( oActive != null )
			{
				string szLanguage = oActive.Language;
				TextSelection oSelection = ( TextSelection )oActive.Selection;

				Color2CodeMap oMap = _OptionPage.Color2CodeList.Find( oItem => oItem.Language.ToLower( ).Equals( oActive.Language.ToLower( ) ) );

				if ( oMap != null )
				{
					oSelection.Text = EvaluateTemplate( oMap.Template, ( ( SolidColorBrush )( ( Button )sender ).Background ).Color );
				}
			}
		}

		private string EvaluateTemplate ( string Template, Color Color )
		{
			IDictionary<string, string> oReplMap = new Dictionary<string, string>( )
															{
																{"%X",X(Color)},
																{"%R",R(Color)},
																{"%G",G(Color)},
																{"%B",B(Color)},
																{"%Rx",Rx(Color)},
																{"%Gx",Gx(Color)},
																{"%Bx",Bx(Color)},
																{"%C",C(Color)}
															};

			Regex oRegex = new Regex( String.Join( "|", oReplMap.Keys ), RegexOptions.IgnoreCase );
			string szWork = oRegex.Replace( Template, oItem => oReplMap[ oItem.Value ] );

			return ( szWork );
		}

		private string R ( Color Color )
		{
			return ( string.Format( "{0}", Color.R ) );
		}

		private string G ( Color Color )
		{
			return ( string.Format( "{0}", Color.G ) );
		}

		private string B ( Color Color )
		{
			return ( string.Format( "{0}", Color.B ) );
		}

		private string Rx ( Color Color )
		{
			return ( string.Format( "#{0:X2}", Color.R ) );
		}

		private string Gx ( Color Color )
		{
			return ( string.Format( "#{0:X2}", Color.G ) );
		}

		private string Bx ( Color Color )
		{
			return ( string.Format( "#{0:X2}", Color.B ) );
		}

		private string X ( Color Color )
		{
			return ( string.Format( "#{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B ) );
		}

		private string C ( Color Color )
		{
			return ( string.Format( "{0}", Convert.ToUInt32( string.Format( "{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B ), 16 ) ) );
		}
	}
}
