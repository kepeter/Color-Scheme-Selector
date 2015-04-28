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

        public const byte MaxRed = 255;
        public const byte MaxGreen = 255;
        public const byte MaxBlue = 255;

        public const short MaxHue = 360;
        public const byte MaxSaturation = 100;
        public const byte MaxValue = 100;

        private int _Width = 256;
        private int _Height = 256;
        private int _Stride = 768;
        private Pen _Pen = new Pen ( Brushes.Cyan, 1 );
        private Rect _Rect = new Rect ( 0, 0, 256, 256 );
        private BitmapSource _Image;

        #endregion

        #region Properties

        #region R

        private byte _R = 128;

        public byte R
        {
            get
            {
                return _R;
            }
            set
            {
                _R = value;

                CreateImage ( );
                InvalidateVisual ( );

                NotifyPropertyChanged ( "R" );
                RGB2HSV ( );
            }
        }

        #endregion

        #region G

        private byte _G = 128;

        public byte G
        {
            get
            {
                return _G;
            }
            set
            {
                _G = value;

                UpdateSelector ( _G, _B, true );
            }
        }

        #endregion

        #region B

        private byte _B = 128;

        public byte B
        {
            get
            {
                return _B;
            }
            set
            {
                _B = value;

                UpdateSelector ( _G, _B, true );
            }
        }

        #endregion

        #region H

        private short _H = 0;

        public short H
        {
            get
            {
                return _H;
            }
            set
            {
                _H = value;

                NotifyPropertyChanged ( "H" );
                HSV2RGB ( );
            }
        }

        #endregion

        #region S

        private byte _S = 0;

        public byte S
        {
            get
            {
                return _S;
            }
            set
            {
                _S = value;

                NotifyPropertyChanged ( "S" );
                HSV2RGB ( );
            }
        }

        #endregion

        #region V

        private byte _V = 50;

        public byte V
        {
            get
            {
                return _V;
            }
            set
            {
                _V = value;

                NotifyPropertyChanged ( "V" );
                HSV2RGB ( );
            }
        }

        #endregion

        #endregion

        #region ctor / dtor

        public ColorWheelGB ( )
        {
            InitializeComponent ( );

            CreateImage ( );
        }

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

            RGB2HSV ( );

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

        private void RGB2HSV ( )
        {
            double nR = _R / ( double )MaxRed;
            double nG = _G / ( double )MaxGreen;
            double nB = _B / ( double )MaxBlue;
            double nCmax = Math.Max ( nR, Math.Max ( nG, nB ) );
            double nCmin = Math.Min ( nR, Math.Min ( nG, nB ) );
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

            _H = Convert.ToInt16 ( nH );
            _S = Convert.ToByte ( nS );
            _V = Convert.ToByte ( nV );

            NotifyPropertyChanged ( "H" );
            NotifyPropertyChanged ( "S" );
            NotifyPropertyChanged ( "V" );
        }

        private void HSV2RGB ( )
        {
            double nS = _S / ( double )MaxSaturation;
            double nV = _V / ( double )MaxValue;
            double nDelta = nV * nS;
            double nH = _H / 60.0;
            double nX = nDelta * ( 1 - Math.Abs ( ( nH % 2 ) - 1 ) );
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

            _R = Convert.ToByte ( nR );
            _G = Convert.ToByte ( nG );
            _B = Convert.ToByte ( nB );

            NotifyPropertyChanged ( "R" );
            NotifyPropertyChanged ( "G" );
            NotifyPropertyChanged ( "B" );
        }

        #endregion
    }
}
