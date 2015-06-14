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
        private Pen _Pen = new Pen ( Brushes.Cyan, 1 );
        private Rect _Rect = new Rect ( 0, 0, 256, 256 );
        private BitmapSource _Image;

        #endregion

        #region ctor / dtor

        public ColorWheelGB ( )
        {
            InitializeComponent ( );

            CreateImage ( );
        }

        #endregion

        #region Properties

        #region R

        private byte _R;

        public byte R
        {
            set
            {
                _R = value;

                CreateImage ( );
                InvalidateVisual ( );
            }
        }

        #endregion

        #region G

        private byte _G;

        public byte G
        {
            get
            {
                return ( _G );
            }
            set
            {
                _G = value;

                UpdateSelector ( _G, _B, true );
            }
        }

        #endregion

        #region B

        private byte _B;

        public byte B
        {
            get
            {
                return ( _B );
            }
            set
            {
                _B = value;

                UpdateSelector ( _G, _B, true );
            }
        }

        #endregion

        #endregion

        #region Event handlers

        protected override void OnRender ( DrawingContext drawingContext )
        {
            base.OnRender ( drawingContext );

            drawingContext.DrawImage ( _Image, _Rect );

            drawingContext.DrawLine ( _Pen, new Point ( _G, 0 ), new Point ( _G, _Height ) );
            drawingContext.DrawLine ( _Pen, new Point ( 0, _B ), new Point ( _Width, _B ) );
        }

        protected override void OnMouseLeftButtonUp ( MouseButtonEventArgs e )
        {
            base.OnMouseLeftButtonUp ( e );

            Point oPoint = e.GetPosition ( this );

            UpdateSelector ( oPoint.X, oPoint.Y );
        }

        protected override void OnMouseMove ( MouseEventArgs e )
        {
            base.OnMouseMove ( e );

            if ( e.LeftButton == MouseButtonState.Pressed )
            {
                Point oPoint = e.GetPosition ( this );

                UpdateSelector ( oPoint.X, oPoint.Y );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged ( [CallerMemberName] string PropertyName = "" )
        {
            if ( PropertyChanged != null )
            {
                PropertyChanged ( this, new PropertyChangedEventArgs ( PropertyName ) );
            }
        }

        #endregion

        #region Helpers

        private void UpdateSelector ( double G, double B, bool FromPropery = false )
        {
            _G = ( byte )G;
            if ( !FromPropery )
            {
                NotifyPropertyChanged ( "G" );
            }

            _B = ( byte )B;
            if ( !FromPropery )
            {
                NotifyPropertyChanged ( "B" );
            }

            InvalidateVisual ( );
        }

        private void CreateImage ( )
        {
            Width = _Width;
            Height = _Height;

            byte[ ] bPixels = new byte[ Math.Abs ( _Stride ) * _Height ];

            for ( int i = 0; i < bPixels.Length; i += 3 )
            {
                bPixels[ i + 2 ] = ( byte )( i / _Stride );
                bPixels[ i + 1 ] = ( byte )( ( i % _Stride ) / 3 );
                bPixels[ i ] = _R;
            }

            _Image = BitmapSource.Create (
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
