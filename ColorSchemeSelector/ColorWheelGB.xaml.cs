using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorSchemeExtension
{
	public partial class ColorWheelGB : UserControl, INotifyPropertyChanged
	{
		#region constant values

		private int _Width = 256;
		private int _Height = 256;
		private int _Stride = 768;
		private Pen _Pen = new Pen( Brushes.Cyan, 1 );
		private Rect _Rect = new Rect( 0, 0, 256, 256 );
		private BitmapSource _Image;

		#endregion

		#region Properties

		#region Color

		private ColorEx _BaseColor = new ColorEx( );

		public ColorEx BaseColor
		{
			get
			{
				return _BaseColor;
			}
			set
			{
				_BaseColor = value;
			}
		}

		#endregion

		#region R

		public byte R
		{
			get
			{
				return ( _BaseColor.R );
			}
			set
			{
				_BaseColor.R = value;

				CreateImage( );
				InvalidateVisual( );

				NotifyPropertyChanged( "R" );
			}
		}

		#endregion

		#region G

		public byte G
		{
			get
			{
				return ( _BaseColor.G );
			}
			set
			{
				_BaseColor.G = value;

				UpdateSelector( _BaseColor.G, _BaseColor.B, true );
			}
		}

		#endregion

		#region B

		public byte B
		{
			get
			{
				return ( _BaseColor.B );
			}
			set
			{
				_BaseColor.B = value;

				UpdateSelector( _BaseColor.G, _BaseColor.B, true );
			}
		}

		#endregion

		#region H

		public short H
		{
			get
			{
				return ( _BaseColor.H );
			}
			set
			{
				_BaseColor.H = value;

				NotifyPropertyChanged( "H" );
			}
		}

		#endregion

		#region S

		public byte S
		{
			get
			{
				return ( _BaseColor.S );
			}
			set
			{
				_BaseColor.S = value;

				NotifyPropertyChanged( "S" );
			}
		}

		#endregion

		#region V

		public byte V
		{
			get
			{
				return ( _BaseColor.V );
			}
			set
			{
				_BaseColor.V = value;

				NotifyPropertyChanged( "V" );
			}
		}

		#endregion

		#endregion

		#region ctor / dtor

		public ColorWheelGB ( )
		{
			InitializeComponent( );

			CreateImage( );
		}

		#endregion

		#region Event handlers

		protected override void OnRender ( DrawingContext drawingContext )
		{
			base.OnRender( drawingContext );

			drawingContext.DrawImage( _Image, _Rect );

			drawingContext.DrawLine( _Pen, new Point( _BaseColor.G, 0 ), new Point( _BaseColor.G, _Height ) );
			drawingContext.DrawLine( _Pen, new Point( 0, _BaseColor.B ), new Point( _Width, _BaseColor.B ) );
		}

		protected override void OnMouseLeftButtonUp ( MouseButtonEventArgs e )
		{
			base.OnMouseLeftButtonUp( e );

			Point oPoint = e.GetPosition( this );

			UpdateSelector( oPoint.X, oPoint.Y );
		}

		protected override void OnMouseMove ( MouseEventArgs e )
		{
			base.OnMouseMove( e );

			if ( e.LeftButton == MouseButtonState.Pressed )
			{
				Point oPoint = e.GetPosition( this );

				UpdateSelector( oPoint.X, oPoint.Y );
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged ( [CallerMemberName] string PropertyName = "" )
		{
			if ( PropertyChanged != null )
			{
				PropertyChanged( this, new PropertyChangedEventArgs( PropertyName ) );
			}
		}

		#endregion

		#region Helpers

		private void UpdateSelector ( double G, double B, bool FromPropery = false )
		{
			_BaseColor.G = ( byte )G;
			if ( !FromPropery )
			{
				NotifyPropertyChanged( "G" );
			}

			_BaseColor.B = ( byte )B;
			if ( !FromPropery )
			{
				NotifyPropertyChanged( "B" );
			}

			InvalidateVisual( );
		}

		private void CreateImage ( )
		{
			Width = _Width;
			Height = _Height;

			byte[ ] bPixels = new byte[ Math.Abs( _Stride ) * _Height ];

			for ( int i = 0; i < bPixels.Length; i += 3 )
			{
				bPixels[ i + 2 ] = ( byte )( i / _Stride );
				bPixels[ i + 1 ] = ( byte )( ( i % _Stride ) / 3 );
				bPixels[ i ] = _BaseColor.R;
			}

			_Image = BitmapSource.Create(
				_Width,
				_Height,
				0,
				0,
				PixelFormats.Rgb24,
				null,
				bPixels,
				_Stride );
		}

		#endregion
	}
}
