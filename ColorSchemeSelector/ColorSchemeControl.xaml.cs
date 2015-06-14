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
		private static int Complements = 0;
		private static int SplitComplements = 1;
		private static int Triads = 2;
		private static int Tetrads = 3;
		private static int Quintads = 4;
		private static int Analogous = 5;
		private static int Monochromatics = 6;
		private static int Combinations = 7;

		private ColorSchemeToolWindow _Parent = null;
		private ColorSchemeOptionsPage _OptionPage = null;
		private Array _Buttons = null;

		public ColorSchemeControl ( ColorSchemeToolWindow Parent )
		{
			InitializeComponent ( );

			_Parent = Parent;

			RGBWheel.PropertyChanged += Color_PropertyChanged;

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

			FillColorButtons ( );
		}

		private void Color_PropertyChanged ( object sender, System.ComponentModel.PropertyChangedEventArgs e )
		{
			FillColorButtons ( );
		}

		private void FillColorButtons ( )
		{
			SelectedColor.Background = new SolidColorBrush ( RGBWheel.BaseColor.Color );

			FillComplements ( ( Button[ ] )_Buttons.GetValue ( Complements ), RGBWheel.BaseColor );
			FillSplitComplements ( ( Button[ ] )_Buttons.GetValue ( SplitComplements ), RGBWheel.BaseColor );
			FillTriads ( ( Button[ ] )_Buttons.GetValue ( Triads ), RGBWheel.BaseColor );
			FillTetrads ( ( Button[ ] )_Buttons.GetValue ( Tetrads ), RGBWheel.BaseColor );
			FillQuintads ( ( Button[ ] )_Buttons.GetValue ( Quintads ), RGBWheel.BaseColor );
			FillAnalogous ( ( Button[ ] )_Buttons.GetValue ( Analogous ), RGBWheel.BaseColor );
			FillMonochromatics ( ( Button[ ] )_Buttons.GetValue ( Monochromatics ), RGBWheel.BaseColor );
			FillCombinations ( ( Array[ ] )_Buttons.GetValue ( Combinations ), RGBWheel.BaseColor );
		}

		private void FillComplements ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oComplement = BaseColor.Clone ( );

			oComplement.H += ( short )( ColorEx.MaxHue / 2 );

			Buttons[ 0 ].Background = new SolidColorBrush ( BaseColor.Color );
			Buttons[ 1 ].Background = new SolidColorBrush ( oComplement.Color );
		}

		private void FillSplitComplements ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oComplement1 = BaseColor.Clone ( );
			ColorEx oComplement2 = BaseColor.Clone ( );

			oComplement1.H += ( short )( ( ColorEx.MaxHue / 2 ) - 30 );
			oComplement2.H += ( short )( ( ColorEx.MaxHue / 2 ) + 30 );

			Buttons[ 0 ].Background = new SolidColorBrush ( BaseColor.Color );
			Buttons[ 1 ].Background = new SolidColorBrush ( oComplement1.Color );
			Buttons[ 2 ].Background = new SolidColorBrush ( oComplement2.Color );
		}

		private void FillTriads ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oTriad1 = BaseColor.Clone ( );
			ColorEx oTriad2 = BaseColor.Clone ( );

			oTriad1.H += 120;
			oTriad2.H -= 120;

			Buttons[ 0 ].Background = new SolidColorBrush ( BaseColor.Color );
			Buttons[ 1 ].Background = new SolidColorBrush ( oTriad1.Color );
			Buttons[ 2 ].Background = new SolidColorBrush ( oTriad2.Color );
		}

		private void FillTetrads ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oTetrad1 = BaseColor.Clone ( );
			ColorEx oTetrad2 = BaseColor.Clone ( );
			ColorEx oTetrad3 = BaseColor.Clone ( );

			oTetrad1.H += 90;
			oTetrad2.H += ( short )( ColorEx.MaxHue / 2 );
			oTetrad3.H += ( short )( ( ColorEx.MaxHue / 2 ) + 90 );

			Buttons[ 0 ].Background = new SolidColorBrush ( BaseColor.Color );
			Buttons[ 1 ].Background = new SolidColorBrush ( oTetrad1.Color );
			Buttons[ 2 ].Background = new SolidColorBrush ( oTetrad2.Color );
			Buttons[ 3 ].Background = new SolidColorBrush ( oTetrad3.Color );
		}

		private void FillQuintads ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oQuintad1 = BaseColor.Clone ( );
			ColorEx oQuintad2 = BaseColor.Clone ( );
			ColorEx oQuintad3 = BaseColor.Clone ( );
			ColorEx oQuintad4 = BaseColor.Clone ( );

			oQuintad1.H += 72;
			oQuintad2.H += 2 * 72;
			oQuintad3.H += 3 * 72;
			oQuintad4.H += 4 * 72;

			Buttons[ 0 ].Background = new SolidColorBrush ( BaseColor.Color );
			Buttons[ 1 ].Background = new SolidColorBrush ( oQuintad1.Color );
			Buttons[ 2 ].Background = new SolidColorBrush ( oQuintad2.Color );
			Buttons[ 3 ].Background = new SolidColorBrush ( oQuintad3.Color );
			Buttons[ 4 ].Background = new SolidColorBrush ( oQuintad4.Color );
		}

		private void FillAnalogous ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oAnalogous1 = BaseColor.Clone ( );
			ColorEx oAnalogous2 = BaseColor.Clone ( );

			oAnalogous1.H += 30;
			oAnalogous2.H -= 30;

			Buttons[ 0 ].Background = new SolidColorBrush ( BaseColor.Color );
			Buttons[ 1 ].Background = new SolidColorBrush ( oAnalogous1.Color );
			Buttons[ 2 ].Background = new SolidColorBrush ( oAnalogous2.Color );
		}

		private void FillMonochromatics ( Button[ ] Buttons, ColorEx BaseColor )
		{
			byte nGap = 20;
			byte nS = BaseColor.S;
			ColorEx oMonochromatic0;
			ColorEx oMonochromatic1;
			ColorEx oMonochromatic2;
			ColorEx oMonochromatic3;
			ColorEx oMonochromatic4;

			while ( nS <= ColorEx.MaxSaturation )
			{
				nS += nGap;
			}

			nS -= nGap;

			oMonochromatic0 = new ColorEx ( )
			{
				H = BaseColor.H,
				S = nS,
				V = BaseColor.V
			};
			oMonochromatic1 = new ColorEx ( )
			{
				H = BaseColor.H,
				S = ( byte )( nS - nGap ),
				V = BaseColor.V
			};
			oMonochromatic2 = new ColorEx ( )
			{
				H = BaseColor.H,
				S = ( byte )( nS - ( 2 * nGap ) ),
				V = BaseColor.V
			};
			oMonochromatic3 = new ColorEx ( )
			{
				H = BaseColor.H,
				S = ( byte )( nS - ( 3 * nGap ) ),
				V = BaseColor.V
			};
			oMonochromatic4 = new ColorEx ( )
			{
				H = BaseColor.H,
				S = ( byte )( nS - ( 4 * nGap ) ),
				V = BaseColor.V
			};

			Buttons[ 0 ].Background = new SolidColorBrush ( oMonochromatic0.Color );
			Buttons[ 1 ].Background = new SolidColorBrush ( oMonochromatic1.Color );
			Buttons[ 2 ].Background = new SolidColorBrush ( oMonochromatic2.Color );
			Buttons[ 3 ].Background = new SolidColorBrush ( oMonochromatic3.Color );
			Buttons[ 4 ].Background = new SolidColorBrush ( oMonochromatic4.Color );
		}

		private void FillCombinations ( Array[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oQuintad1 = BaseColor.Clone ( );
			ColorEx oQuintad2 = BaseColor.Clone ( );
			ColorEx oQuintad3 = BaseColor.Clone ( );
			ColorEx oQuintad4 = BaseColor.Clone ( );

			oQuintad1.H += 72;
			oQuintad2.H += 2 * 72;
			oQuintad3.H += 3 * 72;
			oQuintad4.H += 4 * 72;

			FillMonochromatics ( ( Button[ ] )Buttons.GetValue ( 0 ), BaseColor );
			FillMonochromatics ( ( Button[ ] )Buttons.GetValue ( 1 ), oQuintad1 );
			FillMonochromatics ( ( Button[ ] )Buttons.GetValue ( 2 ), oQuintad2 );
			FillMonochromatics ( ( Button[ ] )Buttons.GetValue ( 3 ), oQuintad3 );
			FillMonochromatics ( ( Button[ ] )Buttons.GetValue ( 4 ), oQuintad4 );
		}

		private object GetService ( Type service )
		{
			if ( _Parent != null )
			{
				return ( _Parent.GetVsService ( service ) );
			}

			return ( null );
		}

		private void Button_Click ( object sender, System.Windows.RoutedEventArgs e )
		{
			EnvDTE80.DTE2 oApplication = ( EnvDTE80.DTE2 )GetService ( typeof ( EnvDTE.DTE ) );
			EnvDTE.Document oActive = oApplication.ActiveDocument;

			if ( _OptionPage == null )
			{
				_OptionPage = new ColorSchemeOptionsPage ( );
				PropertyInfo[ ] oPropertyInfoArray = _OptionPage.GetType ( ).GetProperties ( );
				Properties oProperties = oApplication.get_Properties ( "Custom Tools", "Color Scheme Selector" );

				foreach ( PropertyInfo oPropertyInfo in oPropertyInfoArray )
				{
					if ( oPropertyInfo.ReflectedType == oPropertyInfo.DeclaringType )
					{
						oPropertyInfo.SetValue ( _OptionPage, oProperties.Item ( oPropertyInfo.Name ).Value, null );
					}
				}
			}

			if ( oActive != null )
			{
				string szLanguage = oActive.Language;
				TextSelection oSelection = ( TextSelection )oActive.Selection;

				Color2CodeMap oMap = _OptionPage.Color2CodeList.Find ( oItem => oItem.Language.ToLower ( ).Equals ( oActive.Language.ToLower ( ) ) );

				if ( oMap != null )
				{
					oSelection.Text = EvaluateTemplate ( oMap.Template, ( ( SolidColorBrush )( ( Button )sender ).Background ).Color );
				}
			}
		}

		private string EvaluateTemplate ( string Template, Color Color )
		{
			IDictionary<string, string> oReplMap = new Dictionary<string, string> ( )
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

			Regex oRegex = new Regex ( String.Join ( "|", oReplMap.Keys ), RegexOptions.IgnoreCase );
			string szWork = oRegex.Replace ( Template, oItem => oReplMap[ oItem.Value ] );

			return ( szWork );
		}

		private string R ( Color Color )
		{
			return ( string.Format ( "{0}", Color.R ) );
		}

		private string G ( Color Color )
		{
			return ( string.Format ( "{0}", Color.G ) );
		}

		private string B ( Color Color )
		{
			return ( string.Format ( "{0}", Color.B ) );
		}

		private string Rx ( Color Color )
		{
			return ( string.Format ( "#{0:X2}", Color.R ) );
		}

		private string Gx ( Color Color )
		{
			return ( string.Format ( "#{0:X2}", Color.G ) );
		}

		private string Bx ( Color Color )
		{
			return ( string.Format ( "#{0:X2}", Color.B ) );
		}

		private string X ( Color Color )
		{
			return ( string.Format ( "#{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B ) );
		}

		private string C ( Color Color )
		{
			return ( string.Format ( "{0}", Convert.ToUInt16 ( string.Format ( "{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B ), 16 ) ) );
		}
	}
}

/*
	public class Combinations : ColorGroup
	{
		public Combinations ( ColorEx Base )
			: base( Base )
		{
		}

		protected override void Init ( )
		{
			Quintads oQuintads = new Quintads( Base );

			Colors.Clear( );

			foreach ( ColorEx oColor in oQuintads.Colors )
			{
				Monochromatics oMonochromatics = new Monochromatics( oColor );

				foreach(ColorEx oMono in oMonochromatics.Colors)
				{
					Colors.Add( oMono );
				}
			}
		}
	}
}
*/
