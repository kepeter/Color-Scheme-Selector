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

			FillColorButtons( GBPart.BaseColor.Color );
		}

		private void GBColor_PropertyChanged ( object sender, System.ComponentModel.PropertyChangedEventArgs e )
		{
			FillColorButtons( GBPart.BaseColor.Color );
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
			SelectedColor.Background = new SolidColorBrush( Color );

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

/*
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColorSchemeSelector
{
	public class ColorEx
	{
		#region private

		private enum ColorSource
		{
			RGB,
			HSV,
			CMYK
		}

		private ColorSource _LastColorSource;
		private bool _RGBIsDirty = false;
		private bool _HSVIsDirty = false;
		private bool _CMYKIsDirty = false;

		#endregion

		#region constant values

		public const byte MaxRed = 255;
		public const byte MaxGreen = 255;
		public const byte MaxBlue = 255;

		public const short MaxHue = 360;
		public const byte MaxSaturation = 100;
		public const byte MaxValue = 100;

		public const byte MaxCyan = 100;
		public const byte MaxMagenta = 100;
		public const byte MaxYellow = 100;
		public const byte MaxKey = 100;

		public const byte MaxAlpha = 100;

		#endregion

		#region ctor

		public static ColorEx Clone ( ColorEx Color )
		{
			return ( new ColorEx( )
			{
				R = Color.R,
				G = Color.G,
				B = Color.B,
				A = Color.A
			} );
		}

		public static ColorEx RGB ( byte R, byte G, byte B )
		{
			return ( new ColorEx( )
			{
				R = R,
				G = G,
				B = B,
				A = MaxAlpha
			} );
		}

		public static ColorEx RGBA ( byte R, byte G, byte B, byte A )
		{
			return ( new ColorEx( )
			{
				R = R,
				G = G,
				B = B,
				A = A
			} );
		}

		public static ColorEx HSV ( short H, byte S, byte V )
		{
			return ( new ColorEx( )
			{
				H = H,
				S = S,
				V = V,
				A = MaxAlpha
			} );
		}

		public static ColorEx HSVA ( short H, byte S, byte V, byte A )
		{
			return ( new ColorEx( )
			{
				H = H,
				S = S,
				V = V,
				A = A
			} );
		}

		public static ColorEx CMYK ( byte C, byte M, byte Y, byte K )
		{
			return ( new ColorEx( )
			{
				C = C,
				M = M,
				Y = Y,
				K = K,
				A = MaxAlpha
			} );
		}

		public static ColorEx CMYKA ( byte C, byte M, byte Y, byte K, byte A )
		{
			return ( new ColorEx( )
			{
				C = C,
				M = M,
				Y = Y,
				K = K,
				A = A
			} );
		}

		public static ColorEx Color ( Color Color )
		{
			return ( new ColorEx( )
			{
				R = Color.R,
				G = Color.G,
				B = Color.B,
				A = Convert.ToByte( Color.A / 2.55 )
			} );
		}

		#endregion

		#region properties

		#region alpha

		private byte _A = MaxAlpha;

		public byte A
		{
			get
			{
				return ( _A );
			}
			set
			{
				if ( value > MaxAlpha )
				{
					_A = MaxAlpha;
				}
				else
				{
					_A = value;
				}
			}
		}

		#endregion

		#region red

		private byte _R = 0;

		public byte R
		{
			get
			{
				UpdateValues( ColorSource.RGB );

				return ( _R );
			}
			set
			{
				_R = value;

				SetState( ColorSource.RGB );
			}
		}

		#endregion

		#region green

		private byte _G = 0;

		public byte G
		{
			get
			{
				UpdateValues( ColorSource.RGB );

				return ( _G );
			}
			set
			{
				_G = value;

				SetState( ColorSource.RGB );
			}
		}

		#endregion

		#region blue

		private byte _B = 0;

		public byte B
		{
			get
			{
				UpdateValues( ColorSource.RGB );

				return ( _B );
			}
			set
			{
				_B = value;

				SetState( ColorSource.RGB );
			}
		}

		#endregion

		#region hue

		private short _H = 0;

		public short H
		{
			get
			{
				UpdateValues( ColorSource.HSV );

				return ( _H );
			}
			set
			{
				while ( value > MaxHue )
				{
					value -= MaxHue;
				}

				while ( value < 0 )
				{
					value += MaxHue;
				}

				_H = value;

				SetState( ColorSource.HSV );
			}
		}

		#endregion

		#region saturation

		private byte _S = 0;

		public byte S
		{
			get
			{
				UpdateValues( ColorSource.HSV );

				return ( _S );
			}
			set
			{
				if ( value > MaxSaturation )
				{
					_S = MaxSaturation;
				}
				else
				{
					_S = value;
				}

				SetState( ColorSource.HSV );
			}
		}

		#endregion

		#region value

		private byte _V = 0;

		public byte V
		{
			get
			{
				UpdateValues( ColorSource.HSV );

				return ( _V );
			}
			set
			{
				if ( value > MaxValue )
				{
					_V = MaxValue;
				}
				else
				{
					_V = value;
				}

				SetState( ColorSource.HSV );
			}
		}

		#endregion

		#region cyan

		private byte _C = 0;

		public byte C
		{
			get
			{
				UpdateValues( ColorSource.CMYK );

				return ( _C );
			}
			set
			{
				if ( value > MaxCyan )
				{
					_C = MaxCyan;
				}
				else
				{
					_C = value;
				}

				SetState( ColorSource.CMYK );
			}
		}

		#endregion

		#region magenta

		private byte _M = 0;

		public byte M
		{
			get
			{
				UpdateValues( ColorSource.CMYK );

				return ( _M );
			}
			set
			{
				if ( value > MaxMagenta )
				{
					_M = MaxMagenta;
				}
				else
				{
					_M = value;
				}

				SetState( ColorSource.CMYK );
			}
		}

		#endregion

		#region yellow

		private byte _Y = 0;

		public byte Y
		{
			get
			{
				UpdateValues( ColorSource.CMYK );

				return ( _Y );
			}
			set
			{
				if ( value > MaxYellow )
				{
					_Y = MaxYellow;
				}
				else
				{
					_Y = value;
				}

				SetState( ColorSource.CMYK );
			}
		}

		#endregion

		#region key

		private byte _K = 0;

		public byte K
		{
			get
			{
				UpdateValues( ColorSource.CMYK );

				return ( _K );
			}
			set
			{
				if ( value > MaxKey )
				{
					_K = MaxKey;
				}
				else
				{
					_K = value;
				}

				SetState( ColorSource.CMYK );
			}
		}

		#endregion

		#endregion

		#region helpers

		private void RGB2HSV ( )
		{
			double nR = _R / ( double )MaxRed;
			double nG = _G / ( double )MaxGreen;
			double nB = _B / ( double )MaxBlue;
			double nCmax = Math.Max( nR, Math.Max( nG, nB ) );
			double nCmin = Math.Min( nR, Math.Min( nG, nB ) );
			double nDelta = nCmax - nCmin;
			double nH = 0;
			double nS = 0;
			double nV = nCmax;

			if ( nDelta != 0 )
			{
				if ( nCmax == nR )
				{
					nH = ( ( nG - nB ) / nDelta ) % 6.0;
				}
				else if ( nCmax == nG )
				{
					nH = ( ( nB - nR ) / nDelta ) + 2.0;
				}
				else if ( nCmax == nB )
				{
					nH = ( ( nR - nG ) / nDelta ) + 4.0;
				}
			}

			nH *= 60.0;

			if ( nH < 0 )
			{
				nH += MaxHue;
			}

			if ( nDelta != 0 )
			{
				nS = nDelta / nCmax;
			}

			nS *= ( double )MaxSaturation;
			nV *= ( double )MaxValue;

			_H = Convert.ToInt16( nH );
			_S = Convert.ToByte( nS );
			_V = Convert.ToByte( nV );
		}

		private void HSV2RGB ( )
		{
			double nS = _S / ( double )MaxSaturation;
			double nV = _V / ( double )MaxValue;
			double nDelta = nV * nS;
			double nH = _H / 60.0;
			double nX = nDelta * ( 1 - Math.Abs( ( nH % 2 ) - 1 ) );
			double nM = nV - nDelta;
			double nR = 0;
			double nG = 0;
			double nB = 0;

			if ( nH >= 0 && nH < 1 )
			{
				nR = nDelta;
				nG = nX;
			}
			else if ( nH >= 1 && nH < 2 )
			{
				nR = nX;
				nG = nDelta;
			}
			else if ( nH >= 2 && nH < 3 )
			{
				nG = nDelta;
				nB = nX;
			}
			else if ( nH >= 3 && nH < 4 )
			{
				nG = nX;
				nB = nDelta;
			}
			else if ( nH >= 4 && nH < 5 )
			{
				nR = nX;
				nB = nDelta;
			}
			else
			{
				nR = nDelta;
				nB = nX;
			}

			nR += nM;
			nG += nM;
			nB += nM;

			nR *= MaxRed;
			nG *= MaxGreen;
			nB *= MaxBlue;

			_R = Convert.ToByte( nR );
			_G = Convert.ToByte( nG );
			_B = Convert.ToByte( nB );
		}

		private void RGB2CMYK ( )
		{
			double nR = _R / ( double )MaxRed;
			double nG = _G / ( double )MaxGreen;
			double nB = _B / ( double )MaxBlue;
			double nMax = Math.Max( nR, Math.Max( nG, nB ) );
			double nK = 1 - nMax;
			double nC;
			double nM;
			double nY;

			if ( nK == 1 )
			{
				nC = nM = nY = 0;
			}
			else
			{
				nC = ( 1 - nR - nK ) / ( 1 - nK );
				nM = ( 1 - nG - nK ) / ( 1 - nK );
				nY = ( 1 - nB - nK ) / ( 1 - nK );
			}

			_C = Convert.ToByte( nC * MaxCyan );
			_M = Convert.ToByte( nM * MaxMagenta );
			_Y = Convert.ToByte( nY * MaxYellow );
			_K = Convert.ToByte( nK * MaxKey );
		}

		private void CMYK2RGB ( )
		{
			double nC = _C / ( double )MaxCyan;
			double nM = _M / ( double )MaxMagenta;
			double nY = _Y / ( double )MaxYellow;
			double nK = _K / ( double )MaxKey;
			double nR = ( 1 - nC ) * ( 1 - nK ) * ( double )MaxRed;
			double nG = ( 1 - nM ) * ( 1 - nK ) * ( double )MaxGreen;
			double nB = ( 1 - nY ) * ( 1 - nK ) * ( double )MaxBlue;

			_R = Convert.ToByte( nR );
			_G = Convert.ToByte( nG );
			_B = Convert.ToByte( nB );
		}

		private void SetState ( ColorSource LastColorSource )
		{
			_LastColorSource = LastColorSource;

			_RGBIsDirty = !( LastColorSource == ColorSource.RGB );
			_HSVIsDirty = !( LastColorSource == ColorSource.HSV );
			_CMYKIsDirty = !( LastColorSource == ColorSource.CMYK );
		}

		private void UpdateValues ( ColorSource RequestedColorSource )
		{
			if ( RequestedColorSource == _LastColorSource )
			{
				return;
			}

			if ( ( RequestedColorSource == ColorSource.RGB ) && ( _RGBIsDirty ) )
			{
				if ( _LastColorSource == ColorSource.HSV )
				{
					HSV2RGB( );
				}
				else
				{
					CMYK2RGB( );
				}

				_RGBIsDirty = false;
			}

			if ( ( RequestedColorSource == ColorSource.HSV ) && ( _HSVIsDirty ) )
			{
				if ( _LastColorSource == ColorSource.CMYK )
				{
					CMYK2RGB( );
				}

				RGB2HSV( );

				_HSVIsDirty = false;
			}

			if ( ( RequestedColorSource == ColorSource.CMYK ) && ( _CMYKIsDirty ) )
			{
				if ( _LastColorSource == ColorSource.HSV )
				{
					HSV2RGB( );
				}

				RGB2CMYK( );

				_CMYKIsDirty = false;
			}
		}

		#endregion
	}

	public class ColorGroup
	{
		public ColorGroup ( ColorEx Base )
		{
			_Base = Base;

			Init( );
		}

		private ColorEx _Base;

		public ColorEx Base
		{
			get
			{
				return ( _Base );
			}
			set
			{
				_Base = value;

				Init( );
			}
		}

		private List<ColorEx> _Colors = new List<ColorEx>( );

		public List<ColorEx> Colors
		{
			get
			{
				return ( _Colors );
			}
		}

		protected virtual void Init ( )
		{
		}
	}

	public class Complements : ColorGroup
	{
		public Complements ( ColorEx Base )
			: base( Base )
		{
		}

		protected override void Init ( )
		{
			ColorEx oComplement = ColorEx.Clone( Base );

			oComplement.H += ( ColorEx.MaxHue / 2 );

			Colors.Clear( );

			Colors.Add( Base );
			Colors.Add( oComplement );
		}
	}

	public class SplitComplements : ColorGroup
	{
		public SplitComplements ( ColorEx Base )
			: base( Base )
		{
		}

		protected override void Init ( )
		{
			ColorEx oComplement1 = ColorEx.Clone( Base );
			ColorEx oComplement2 = ColorEx.Clone( Base );

			oComplement1.H += ( ( ColorEx.MaxHue / 2 ) - 30 );
			oComplement2.H += ( ( ColorEx.MaxHue / 2 ) + 30 );

			Colors.Clear( );

			Colors.Add( Base );
			Colors.Add( oComplement1 );
			Colors.Add( oComplement2 );
		}
	}

	public class Triads : ColorGroup
	{
		public Triads ( ColorEx Base )
			: base( Base )
		{
		}

		protected override void Init ( )
		{
			ColorEx oTriad1 = ColorEx.Clone( Base );
			ColorEx oTriad2 = ColorEx.Clone( Base );

			oTriad1.H += 120;
			oTriad2.H -= 120;

			Colors.Clear( );

			Colors.Add( Base );
			Colors.Add( oTriad1 );
			Colors.Add( oTriad2 );
		}
	}

	public class Tetrads : ColorGroup
	{
		public Tetrads ( ColorEx Base )
			: base( Base )
		{
		}

		protected override void Init ( )
		{
			ColorEx oTetrad1 = ColorEx.Clone( Base );
			ColorEx oTetrad2 = ColorEx.Clone( Base );
			ColorEx oTetrad3 = ColorEx.Clone( Base );

			oTetrad1.H += 90;
			oTetrad2.H += ( ColorEx.MaxHue / 2 );
			oTetrad3.H += ( ( ColorEx.MaxHue / 2 ) + 90 );

			Colors.Clear( );

			Colors.Add( Base );
			Colors.Add( oTetrad1 );
			Colors.Add( oTetrad2 );
			Colors.Add( oTetrad3 );
		}
	}

	public class Quintads : ColorGroup
	{
		public Quintads ( ColorEx Base )
			: base( Base )
		{
		}

		protected override void Init ( )
		{
			ColorEx oQuintad1 = ColorEx.Clone( Base );
			ColorEx oQuintad2 = ColorEx.Clone( Base );
			ColorEx oQuintad3 = ColorEx.Clone( Base );
			ColorEx oQuintad4 = ColorEx.Clone( Base );

			oQuintad1.H += 72;
			oQuintad2.H += 2 * 72;
			oQuintad3.H += 3 * 72;
			oQuintad4.H += 4 * 72;

			Colors.Clear( );

			Colors.Add( Base );
			Colors.Add( oQuintad1 );
			Colors.Add( oQuintad2 );
			Colors.Add( oQuintad3 );
			Colors.Add( oQuintad4 );
		}
	}

	public class Analogous : ColorGroup
	{
		public Analogous ( ColorEx Base )
			: base( Base )
		{
		}

		protected override void Init ( )
		{
			ColorEx oAnalogous1 = ColorEx.Clone( Base );
			ColorEx oAnalogous2 = ColorEx.Clone( Base );

			oAnalogous1.H += 30;
			oAnalogous2.H -= 30;

			Colors.Clear( );

			Colors.Add( Base );
			Colors.Add( oAnalogous1 );
			Colors.Add( oAnalogous2 );
		}
	}

	public class Monochromatics : ColorGroup
	{
		public Monochromatics ( ColorEx Base )
			: base( Base )
		{
		}

		protected override void Init ( )
		{
			byte nS = Base.S;
			ColorEx oMonochromatic1;
			ColorEx oMonochromatic2;
			ColorEx oMonochromatic3;
			ColorEx oMonochromatic4;

			while ( nS <= ColorEx.MaxSaturation )
			{
				nS += 15;
			}

			nS -= 15;

			Base.S = nS;
			oMonochromatic1 = ColorEx.HSV( Base.H, ( byte )( nS - 15 ), Base.V );
			oMonochromatic2 = ColorEx.HSV( Base.H, ( byte )( nS - ( 2 * 15 ) ), Base.V );
			oMonochromatic3 = ColorEx.HSV( Base.H, ( byte )( nS - ( 3 * 15 ) ), Base.V );
			oMonochromatic4 = ColorEx.HSV( Base.H, ( byte )( nS - ( 4 * 15 ) ), Base.V );

			Colors.Clear( );

			Colors.Add( Base );
			Colors.Add( oMonochromatic1 );
			Colors.Add( oMonochromatic2 );
			Colors.Add( oMonochromatic3 );
			Colors.Add( oMonochromatic4 );
		}
	}

	public class Transparents : ColorGroup
	{
		public Transparents ( ColorEx Base )
			: base( Base )
		{
		}

		protected override void Init ( )
		{
			ColorEx oTransparent1 = ColorEx.Clone( Base );
			ColorEx oTransparent2 = ColorEx.Clone( Base );
			ColorEx oTransparent3 = ColorEx.Clone( Base );
			ColorEx oTransparent4 = ColorEx.Clone( Base );

			Base.A = 90;
			oTransparent1.A = 70;
			oTransparent2.A = 50;
			oTransparent3.A = 30;
			oTransparent4.A = 10;

			Colors.Clear( );

			Colors.Add( Base );
			Colors.Add( oTransparent1 );
			Colors.Add( oTransparent2 );
			Colors.Add( oTransparent3 );
			Colors.Add( oTransparent4 );
		}
	}

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

	public class ColorScheme
	{
		private ColorEx _Base;

		public ColorEx Base
		{
			get
			{
				return ( _Base );
			}
			set
			{
				_Base = value;

				Init( );
			}
		}

		private Complements _Complements;

		public Complements Complements
		{
			get
			{
				return ( _Complements );
			}
		}

		private SplitComplements _SplitComplements;

		public SplitComplements SplitComplements
		{
			get
			{
				return ( _SplitComplements );
			}
		}

		private Triads _Triads;

		public Triads Triads
		{
			get
			{
				return ( _Triads );
			}
		}

		private Tetrads _Tetrads;

		public Tetrads Tetrads
		{
			get
			{
				return ( _Tetrads );
			}
		}

		private Quintads _Quintads;

		public Quintads Quintads
		{
			get
			{
				return ( _Quintads );
			}
		}

		private Analogous _Analogous;

		public Analogous Analogous
		{
			get
			{
				return ( _Analogous );
			}
		}

		private Monochromatics _Monochromatics;

		public Monochromatics Monochromatics
		{
			get
			{
				return ( _Monochromatics );
			}
		}

		private Transparents _Transparents;

		public Transparents Transparents
		{
			get
			{
				return ( _Transparents );
			}
		}

		private Combinations _Combinations;

		public Combinations Combinations
		{
			get
			{
				return _Combinations;
			}
		}

		public ColorScheme ( ColorEx Base )
		{
			_Base = Base;

			Init( );
		}

		private void Init ( )
		{
			if ( _Complements == null )
			{
				_Complements = new Complements( _Base );
			}
			else
			{
				_Complements.Base = _Base;
			}

			if ( _SplitComplements == null )
			{
				_SplitComplements = new SplitComplements( _Base );
			}
			else
			{
				_SplitComplements.Base = _Base;
			}

			if ( _Triads == null )
			{
				_Triads = new Triads( _Base );
			}
			else
			{
				_Triads.Base = _Base;
			}

			if ( _Tetrads == null )
			{
				_Tetrads = new Tetrads( _Base );
			}
			else
			{
				_Tetrads.Base = _Base;
			}

			if ( _Quintads == null )
			{
				_Quintads = new Quintads( _Base );
			}
			else
			{
				_Quintads.Base = _Base;
			}

			if ( _Analogous == null )
			{
				_Analogous = new Analogous( _Base );
			}
			else
			{
				_Analogous.Base = _Base;
			}

			if ( _Monochromatics == null )
			{
				_Monochromatics = new Monochromatics( _Base );
			}
			else
			{
				_Monochromatics.Base = _Base;
			}

			if ( _Transparents == null )
			{
				_Transparents = new Transparents( _Base );
			}
			else
			{
				_Transparents.Base = _Base;
			}

			if ( _Combinations == null )
			{
				_Combinations = new Combinations( _Base );
			}
			else
			{
				_Combinations.Base = _Base;
			}
		}
	}

	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load ( object sender, EventArgs e )
		{
			ColorEx oBase = ColorEx.RGB( 4, 44, 255 );
			ColorScheme oColorScheme = new ColorScheme( oBase );

			c1.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Complements.Colors[ 0 ].R, oColorScheme.Complements.Colors[ 0 ].G, oColorScheme.Complements.Colors[ 0 ].B ) );
			c2.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Complements.Colors[ 1 ].R, oColorScheme.Complements.Colors[ 1 ].G, oColorScheme.Complements.Colors[ 1 ].B ) );

			s1.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.SplitComplements.Colors[ 0 ].R, oColorScheme.SplitComplements.Colors[ 0 ].G, oColorScheme.SplitComplements.Colors[ 0 ].B ) );
			s2.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.SplitComplements.Colors[ 1 ].R, oColorScheme.SplitComplements.Colors[ 1 ].G, oColorScheme.SplitComplements.Colors[ 1 ].B ) );
			s3.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.SplitComplements.Colors[ 2 ].R, oColorScheme.SplitComplements.Colors[ 2 ].G, oColorScheme.SplitComplements.Colors[ 2 ].B ) );

			t1.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Tetrads.Colors[ 0 ].R, oColorScheme.Tetrads.Colors[ 0 ].G, oColorScheme.Tetrads.Colors[ 0 ].B ) );
			t2.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Tetrads.Colors[ 1 ].R, oColorScheme.Tetrads.Colors[ 1 ].G, oColorScheme.Tetrads.Colors[ 1 ].B ) );
			t3.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Tetrads.Colors[ 2 ].R, oColorScheme.Tetrads.Colors[ 2 ].G, oColorScheme.Tetrads.Colors[ 2 ].B ) );
			t4.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Tetrads.Colors[ 3 ].R, oColorScheme.Tetrads.Colors[ 3 ].G, oColorScheme.Tetrads.Colors[ 3 ].B ) );

			a1.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Analogous.Colors[ 0 ].R, oColorScheme.Analogous.Colors[ 0 ].G, oColorScheme.Analogous.Colors[ 0 ].B ) );
			a2.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Analogous.Colors[ 1 ].R, oColorScheme.Analogous.Colors[ 1 ].G, oColorScheme.Analogous.Colors[ 1 ].B ) );
			a3.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Analogous.Colors[ 2 ].R, oColorScheme.Analogous.Colors[ 2 ].G, oColorScheme.Analogous.Colors[ 2 ].B ) );

			m1.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Monochromatics.Colors[ 0 ].R, oColorScheme.Monochromatics.Colors[ 0 ].G, oColorScheme.Monochromatics.Colors[ 0 ].B ) );
			m2.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Monochromatics.Colors[ 1 ].R, oColorScheme.Monochromatics.Colors[ 1 ].G, oColorScheme.Monochromatics.Colors[ 1 ].B ) );
			m3.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Monochromatics.Colors[ 2 ].R, oColorScheme.Monochromatics.Colors[ 2 ].G, oColorScheme.Monochromatics.Colors[ 2 ].B ) );
			m4.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Monochromatics.Colors[ 3 ].R, oColorScheme.Monochromatics.Colors[ 3 ].G, oColorScheme.Monochromatics.Colors[ 3 ].B ) );
			m5.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Monochromatics.Colors[ 4 ].R, oColorScheme.Monochromatics.Colors[ 4 ].G, oColorScheme.Monochromatics.Colors[ 4 ].B ) );

			tr1.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Triads.Colors[ 0 ].R, oColorScheme.Triads.Colors[ 0 ].G, oColorScheme.Triads.Colors[ 0 ].B ) );
			tr2.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Triads.Colors[ 1 ].R, oColorScheme.Triads.Colors[ 1 ].G, oColorScheme.Triads.Colors[ 1 ].B ) );
			tr3.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Triads.Colors[ 2 ].R, oColorScheme.Triads.Colors[ 2 ].G, oColorScheme.Triads.Colors[ 2 ].B ) );

			q1.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Quintads.Colors[ 0 ].R, oColorScheme.Quintads.Colors[ 0 ].G, oColorScheme.Quintads.Colors[ 0 ].B ) );
			q2.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Quintads.Colors[ 1 ].R, oColorScheme.Quintads.Colors[ 1 ].G, oColorScheme.Quintads.Colors[ 1 ].B ) );
			q3.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Quintads.Colors[ 2 ].R, oColorScheme.Quintads.Colors[ 2 ].G, oColorScheme.Quintads.Colors[ 2 ].B ) );
			q4.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Quintads.Colors[ 3 ].R, oColorScheme.Quintads.Colors[ 3 ].G, oColorScheme.Quintads.Colors[ 3 ].B ) );
			q5.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Quintads.Colors[ 4 ].R, oColorScheme.Quintads.Colors[ 4 ].G, oColorScheme.Quintads.Colors[ 4 ].B ) );

			o1.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgba({0}, {1}, {2}, {3})", oColorScheme.Transparents.Colors[ 0 ].R, oColorScheme.Transparents.Colors[ 0 ].G, oColorScheme.Transparents.Colors[ 0 ].B, oColorScheme.Transparents.Colors[ 0 ].A / 100.0 ) );
			o2.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgba({0}, {1}, {2}, {3})", oColorScheme.Transparents.Colors[ 1 ].R, oColorScheme.Transparents.Colors[ 1 ].G, oColorScheme.Transparents.Colors[ 1 ].B, oColorScheme.Transparents.Colors[ 1 ].A / 100.0 ) );
			o3.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgba({0}, {1}, {2}, {3})", oColorScheme.Transparents.Colors[ 2 ].R, oColorScheme.Transparents.Colors[ 2 ].G, oColorScheme.Transparents.Colors[ 2 ].B, oColorScheme.Transparents.Colors[ 2 ].A / 100.0 ) );
			o4.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgba({0}, {1}, {2}, {3})", oColorScheme.Transparents.Colors[ 3 ].R, oColorScheme.Transparents.Colors[ 3 ].G, oColorScheme.Transparents.Colors[ 3 ].B, oColorScheme.Transparents.Colors[ 3 ].A / 100.0 ) );
			o5.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgba({0}, {1}, {2}, {3})", oColorScheme.Transparents.Colors[ 4 ].R, oColorScheme.Transparents.Colors[ 4 ].G, oColorScheme.Transparents.Colors[ 4 ].B, oColorScheme.Transparents.Colors[ 4 ].A / 100.0 ) );

			cb1.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 0 ].R, oColorScheme.Combinations.Colors[ 0 ].G, oColorScheme.Combinations.Colors[ 0 ].B ) );
			cb2.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 1 ].R, oColorScheme.Combinations.Colors[ 1 ].G, oColorScheme.Combinations.Colors[ 1 ].B ) );
			cb3.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 2 ].R, oColorScheme.Combinations.Colors[ 2 ].G, oColorScheme.Combinations.Colors[ 2 ].B ) );
			cb4.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 3 ].R, oColorScheme.Combinations.Colors[ 3 ].G, oColorScheme.Combinations.Colors[ 3 ].B ) );
			cb5.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 4 ].R, oColorScheme.Combinations.Colors[ 4 ].G, oColorScheme.Combinations.Colors[ 4 ].B ) );
			cb6.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 5 ].R, oColorScheme.Combinations.Colors[ 5 ].G, oColorScheme.Combinations.Colors[ 5 ].B ) );
			cb7.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 6 ].R, oColorScheme.Combinations.Colors[ 6 ].G, oColorScheme.Combinations.Colors[ 6 ].B ) );
			cb8.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 7 ].R, oColorScheme.Combinations.Colors[ 7 ].G, oColorScheme.Combinations.Colors[ 7 ].B ) );
			cb9.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 8 ].R, oColorScheme.Combinations.Colors[ 8 ].G, oColorScheme.Combinations.Colors[ 8 ].B ) );
			cb10.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 9 ].R, oColorScheme.Combinations.Colors[ 9 ].G, oColorScheme.Combinations.Colors[ 9 ].B ) );
			cb11.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 10 ].R, oColorScheme.Combinations.Colors[ 10 ].G, oColorScheme.Combinations.Colors[ 10 ].B ) );
			cb12.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 11 ].R, oColorScheme.Combinations.Colors[ 11 ].G, oColorScheme.Combinations.Colors[ 11 ].B ) );
			cb13.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 12 ].R, oColorScheme.Combinations.Colors[ 12 ].G, oColorScheme.Combinations.Colors[ 12 ].B ) );
			cb14.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 13 ].R, oColorScheme.Combinations.Colors[ 13 ].G, oColorScheme.Combinations.Colors[ 13 ].B ) );
			cb15.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 14 ].R, oColorScheme.Combinations.Colors[ 14 ].G, oColorScheme.Combinations.Colors[ 14 ].B ) );
			cb16.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 15 ].R, oColorScheme.Combinations.Colors[ 15 ].G, oColorScheme.Combinations.Colors[ 15 ].B ) );
			cb17.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 16 ].R, oColorScheme.Combinations.Colors[ 16 ].G, oColorScheme.Combinations.Colors[ 16 ].B ) );
			cb18.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 17 ].R, oColorScheme.Combinations.Colors[ 17 ].G, oColorScheme.Combinations.Colors[ 17 ].B ) );
			cb19.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 18 ].R, oColorScheme.Combinations.Colors[ 18 ].G, oColorScheme.Combinations.Colors[ 18 ].B ) );
			cb20.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 19 ].R, oColorScheme.Combinations.Colors[ 19 ].G, oColorScheme.Combinations.Colors[ 19 ].B ) );
			cb21.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 20 ].R, oColorScheme.Combinations.Colors[ 20 ].G, oColorScheme.Combinations.Colors[ 20 ].B ) );
			cb22.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 21 ].R, oColorScheme.Combinations.Colors[ 21 ].G, oColorScheme.Combinations.Colors[ 21 ].B ) );
			cb23.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 22 ].R, oColorScheme.Combinations.Colors[ 22 ].G, oColorScheme.Combinations.Colors[ 22 ].B ) );
			cb24.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 23 ].R, oColorScheme.Combinations.Colors[ 23 ].G, oColorScheme.Combinations.Colors[ 23 ].B ) );
			cb25.Style.Add( HtmlTextWriterStyle.BackgroundColor, string.Format( "rgb({0}, {1}, {2})", oColorScheme.Combinations.Colors[ 24 ].R, oColorScheme.Combinations.Colors[ 24 ].G, oColorScheme.Combinations.Colors[ 24 ].B ) );
		}
	}
}
*/
