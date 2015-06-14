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
    public partial class ColorWheelR : UserControl, INotifyPropertyChanged
    {
        #region constant values

        private int _Width = 16;
        private int _Height = 256;
        private int _Stride = 48;
        private Pen _Pen = new Pen ( Brushes.Cyan, 1 );
        private Rect _Rect = new Rect ( 0, 0, 16, 256 );
        private BitmapSource _Image;

        #endregion

        #region ctor / dtor

        public ColorWheelR ( )
        {
            InitializeComponent ( );

            CreateImage ( );
        }

        #endregion

        #region Properties

        private byte _R;

        public byte R
        {
            get
            {
                return _R;
            }
            set
            {
                _R = value;

                InvalidateVisual ( );
            }
        }

        #endregion

        #region Event handlers

        protected override void OnRender ( DrawingContext drawingContext )
        {
            base.OnRender ( drawingContext );

            drawingContext.DrawImage ( _Image, _Rect );

            drawingContext.DrawLine ( _Pen, new Point ( 0, _R ), new Point ( _Width, _R ) );
        }

        protected override void OnMouseLeftButtonUp ( MouseButtonEventArgs e )
        {
            base.OnMouseLeftButtonUp ( e );

            Point oPoint = e.GetPosition ( this );

            UpdateSelector ( oPoint.Y );
        }

        protected override void OnMouseMove ( MouseEventArgs e )
        {
            base.OnMouseMove ( e );

            if ( e.LeftButton == MouseButtonState.Pressed )
            {
                Point oPoint = e.GetPosition ( this );

                UpdateSelector ( oPoint.Y );
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

        private void UpdateSelector ( double R, bool FromPropery = false )
        {
            _R = ( byte )R;

            NotifyPropertyChanged ( "R" );

            InvalidateVisual ( );
        }

        private void CreateImage ( )
        {
            Width = _Width;
            Height = _Height;

            byte[ ] bPixels = new byte[ Math.Abs ( _Stride ) * _Height ];

            for ( int i = 0; i < bPixels.Length; i += 3 )
            {
                bPixels[ i + 2 ] = 0;
                bPixels[ i + 1 ] = 0;
                bPixels[ i ] = ( byte )( i / _Stride );
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
