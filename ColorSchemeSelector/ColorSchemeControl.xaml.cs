using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;
using EnvDTE;

namespace ColorSchemeExtension
{
	public partial class ColorSchemeControl : UserControl, IDisposable
	{
		private enum ColorSets
		{
			Complements = 0,
			SplitComplements = 1,
			Triads = 2,
			Tetrads = 3,
			Quintads = 4,
			Analogous = 5,
			Monochromatics = 6,
			Combinations = 7
		}

		private ColorSchemeToolWindow _Parent = null;
		private Array _Buttons = null;

		public ColorSchemeControl ( ColorSchemeToolWindow Parent )
		{
			_Parent = Parent;

			InitializeComponent( );

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

			RGBWheel.PropertyChanged += Color_PropertyChanged;

			FillColorButtons( );
		}

		~ColorSchemeControl ( )
		{
			Dispose( false );
		}

		private ColorSchemeOptionsPage _OptionPage = null;

		public ColorSchemeOptionsPage OptionPage
		{
			get
			{
				if ( _OptionPage == null )
				{
					_OptionPage = new ColorSchemeOptionsPage( );
					PropertyInfo[ ] oPropertyInfoArray = _OptionPage.GetType( ).GetProperties( );
					EnvDTE80.DTE2 oApplication = ( EnvDTE80.DTE2 )GetService( typeof( EnvDTE.DTE ) );
					Properties oProperties = oApplication.get_Properties( "Custom Tools", "Color Scheme Selector" );

					foreach ( PropertyInfo oPropertyInfo in oPropertyInfoArray )
					{
						if ( oPropertyInfo.ReflectedType == oPropertyInfo.DeclaringType )
						{
							oPropertyInfo.SetValue( _OptionPage, oProperties.Item( oPropertyInfo.Name ).Value, null );
						}
					}
				}

				return ( _OptionPage );
			}
		}

		private void Color_PropertyChanged ( object sender, System.ComponentModel.PropertyChangedEventArgs e )
		{
			FillColorButtons( );
		}

		private void FillSavedList ( )
		{
			SavedColorList.ItemsSource = null;

			SavedColorList.Items.Clear( );

			SavedColorList.ItemsSource = OptionPage.SavedColorList;
		}

		private void FillColorButtons ( )
		{
			SelectedColor.Background = new SolidColorBrush( RGBWheel.BaseColor.Color );

			FillComplements( ( Button[ ] )_Buttons.GetValue( ( int )ColorSets.Complements ), RGBWheel.BaseColor );
			FillSplitComplements( ( Button[ ] )_Buttons.GetValue( ( int )ColorSets.SplitComplements ), RGBWheel.BaseColor );
			FillTriads( ( Button[ ] )_Buttons.GetValue( ( int )ColorSets.Triads ), RGBWheel.BaseColor );
			FillTetrads( ( Button[ ] )_Buttons.GetValue( ( int )ColorSets.Tetrads ), RGBWheel.BaseColor );
			FillQuintads( ( Button[ ] )_Buttons.GetValue( ( int )ColorSets.Quintads ), RGBWheel.BaseColor );
			FillAnalogous( ( Button[ ] )_Buttons.GetValue( ( int )ColorSets.Analogous ), RGBWheel.BaseColor );
			FillMonochromatics( ( Button[ ] )_Buttons.GetValue( ( int )ColorSets.Monochromatics ), RGBWheel.BaseColor );
			FillCombinations( ( Array[ ] )_Buttons.GetValue( ( int )ColorSets.Combinations ), RGBWheel.BaseColor );
		}

		private void FillComplements ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oComplement = BaseColor.Clone( );

			oComplement.H += ( short )( ColorEx.MaxHue / 2 );

			Buttons[ 0 ].Background = new SolidColorBrush( BaseColor.Color );
			Buttons[ 1 ].Background = new SolidColorBrush( oComplement.Color );
		}

		private void FillSplitComplements ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oComplement1 = BaseColor.Clone( );
			ColorEx oComplement2 = BaseColor.Clone( );

			oComplement1.H += ( short )( ( ColorEx.MaxHue / 2 ) - 30 );
			oComplement2.H += ( short )( ( ColorEx.MaxHue / 2 ) + 30 );

			Buttons[ 0 ].Background = new SolidColorBrush( BaseColor.Color );
			Buttons[ 1 ].Background = new SolidColorBrush( oComplement1.Color );
			Buttons[ 2 ].Background = new SolidColorBrush( oComplement2.Color );
		}

		private void FillTriads ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oTriad1 = BaseColor.Clone( );
			ColorEx oTriad2 = BaseColor.Clone( );

			oTriad1.H += 120;
			oTriad2.H -= 120;

			Buttons[ 0 ].Background = new SolidColorBrush( BaseColor.Color );
			Buttons[ 1 ].Background = new SolidColorBrush( oTriad1.Color );
			Buttons[ 2 ].Background = new SolidColorBrush( oTriad2.Color );
		}

		private void FillTetrads ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oTetrad1 = BaseColor.Clone( );
			ColorEx oTetrad2 = BaseColor.Clone( );
			ColorEx oTetrad3 = BaseColor.Clone( );

			oTetrad1.H += 90;
			oTetrad2.H += ( short )( ColorEx.MaxHue / 2 );
			oTetrad3.H += ( short )( ( ColorEx.MaxHue / 2 ) + 90 );

			Buttons[ 0 ].Background = new SolidColorBrush( BaseColor.Color );
			Buttons[ 1 ].Background = new SolidColorBrush( oTetrad1.Color );
			Buttons[ 2 ].Background = new SolidColorBrush( oTetrad2.Color );
			Buttons[ 3 ].Background = new SolidColorBrush( oTetrad3.Color );
		}

		private void FillQuintads ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx[ ] oQuintad = FillQuintadsInt( BaseColor );

			Buttons[ 0 ].Background = new SolidColorBrush( oQuintad[ 0 ].Color );
			Buttons[ 1 ].Background = new SolidColorBrush( oQuintad[ 1 ].Color );
			Buttons[ 2 ].Background = new SolidColorBrush( oQuintad[ 2 ].Color );
			Buttons[ 3 ].Background = new SolidColorBrush( oQuintad[ 3 ].Color );
			Buttons[ 4 ].Background = new SolidColorBrush( oQuintad[ 4 ].Color );
		}

		private ColorEx[ ] FillQuintadsInt ( ColorEx BaseColor )
		{
			ColorEx[ ] oQuintad = new ColorEx[ 5 ];

			oQuintad[ 0 ] = BaseColor.Clone( );
			oQuintad[ 1 ] = BaseColor.Clone( );
			oQuintad[ 2 ] = BaseColor.Clone( );
			oQuintad[ 3 ] = BaseColor.Clone( );
			oQuintad[ 4 ] = BaseColor.Clone( );

			oQuintad[ 1 ].H += 72;
			oQuintad[ 2 ].H += 2 * 72;
			oQuintad[ 3 ].H += 3 * 72;
			oQuintad[ 4 ].H += 4 * 72;

			return ( oQuintad );
		}

		private void FillAnalogous ( Button[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx oAnalogous1 = BaseColor.Clone( );
			ColorEx oAnalogous2 = BaseColor.Clone( );

			oAnalogous1.H += 30;
			oAnalogous2.H -= 30;

			Buttons[ 0 ].Background = new SolidColorBrush( BaseColor.Color );
			Buttons[ 1 ].Background = new SolidColorBrush( oAnalogous1.Color );
			Buttons[ 2 ].Background = new SolidColorBrush( oAnalogous2.Color );
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

			oMonochromatic0 = new ColorEx( )
			{
				H = BaseColor.H,
				S = nS,
				V = BaseColor.V
			};
			oMonochromatic1 = new ColorEx( )
			{
				H = BaseColor.H,
				S = ( byte )( nS - nGap ),
				V = BaseColor.V
			};
			oMonochromatic2 = new ColorEx( )
			{
				H = BaseColor.H,
				S = ( byte )( nS - ( 2 * nGap ) ),
				V = BaseColor.V
			};
			oMonochromatic3 = new ColorEx( )
			{
				H = BaseColor.H,
				S = ( byte )( nS - ( 3 * nGap ) ),
				V = BaseColor.V
			};
			oMonochromatic4 = new ColorEx( )
			{
				H = BaseColor.H,
				S = ( byte )( nS - ( 4 * nGap ) ),
				V = BaseColor.V
			};

			Buttons[ 0 ].Background = new SolidColorBrush( oMonochromatic0.Color );
			Buttons[ 1 ].Background = new SolidColorBrush( oMonochromatic1.Color );
			Buttons[ 2 ].Background = new SolidColorBrush( oMonochromatic2.Color );
			Buttons[ 3 ].Background = new SolidColorBrush( oMonochromatic3.Color );
			Buttons[ 4 ].Background = new SolidColorBrush( oMonochromatic4.Color );
		}

		private void FillCombinations ( Array[ ] Buttons, ColorEx BaseColor )
		{
			ColorEx[ ] oQuintad = FillQuintadsInt( BaseColor );

			FillMonochromatics( ( Button[ ] )Buttons.GetValue( 0 ), oQuintad[ 0 ] );
			FillMonochromatics( ( Button[ ] )Buttons.GetValue( 1 ), oQuintad[ 1 ] );
			FillMonochromatics( ( Button[ ] )Buttons.GetValue( 2 ), oQuintad[ 2 ] );
			FillMonochromatics( ( Button[ ] )Buttons.GetValue( 3 ), oQuintad[ 3 ] );
			FillMonochromatics( ( Button[ ] )Buttons.GetValue( 4 ), oQuintad[ 4 ] );
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

			if ( oActive != null )
			{
				string szExt = Path.GetExtension( oActive.FullName );
				TextSelection oSelection = ( TextSelection )oActive.Selection;

				Color2CodeMap oMap = OptionPage.Color2CodeList.Find( oItem => oItem.Extensions.ToLower( ).Split( ';' ).Any( szString => szString.Equals( szExt.ToLower( ) ) ) );

				if ( oMap != null )
				{
					oSelection.Text = EvaluateTemplate( oMap.Template, ( ( SolidColorBrush )( ( Button )sender ).Background ).Color );
				}

				oActive.Activate( );
			}
		}

		private void Save_Click ( object sender, System.Windows.RoutedEventArgs e )
		{
			Color oColor = ( ( SolidColorBrush )SelectedColor.Background ).Color;
			SaveNameDialog oDlg = new SaveNameDialog( oColor );

			if ( oDlg.ShowDialog( ) == true )
			{
				SavedColor oSavedColor = OptionPage.SavedColorList.Find( oItem => oItem.Name == oDlg.SaveName.Text );
				bool bSave = ( oSavedColor == null );

				if ( oSavedColor != null )
				{
					bSave = ( System.Windows.Forms.MessageBox.Show( "The name you entered already exist in the list of saved colors...\nDo you want to replace the existing color?", "Duplicate name", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Stop ) == System.Windows.Forms.DialogResult.Yes );
				}

				if ( bSave )
				{
					if ( oSavedColor != null )
					{
						oSavedColor.Value = oColor;
					}
					else
					{
						OptionPage.SavedColorList.Add( new SavedColor( )
						{
							Name = oDlg.SaveName.Text,
							Value = oColor
						} );
					}

					OptionPage.SaveSettingsToStorage( ( EnvDTE80.DTE2 )GetService( typeof( EnvDTE.DTE ) ) );

					FillSavedList( );
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

		private void SavedColorList_Loaded ( object sender, System.Windows.RoutedEventArgs e )
		{
			FillSavedList( );
		}

		private void SavedColor_Click ( object sender, System.Windows.RoutedEventArgs e )
		{
			Color oColor = ( ( SolidColorBrush )( ( Button )sender ).Background ).Color;

			RGBWheel.R = oColor.R;
			RGBWheel.G = oColor.G;
			RGBWheel.B = oColor.B;
		}

		private void MenuItem_Click ( object sender, System.Windows.RoutedEventArgs e )
		{
			if ( SavedColorList.SelectedIndex > -1 )
			{
				OptionPage.SavedColorList.Remove( ( ( SavedColor )SavedColorList.SelectedValue ) );

				OptionPage.SaveSettingsToStorage( ( EnvDTE80.DTE2 )GetService( typeof( EnvDTE.DTE ) ) );

				FillSavedList( );
			}
		}

		#region Implement IDisposable because of OptionPage (a win form, that IDisposable)

		private bool _Disposed = false;

		protected virtual void Dispose ( bool Dispose )
		{
			if ( !_Disposed )
			{
				if ( Dispose )
				{
					_OptionPage.Dispose( );
				}

				_Disposed = true;
			}
		}

		public void Dispose ( )
		{
			Dispose( true );

			GC.SuppressFinalize( this );
		}

		#endregion
	}
}
