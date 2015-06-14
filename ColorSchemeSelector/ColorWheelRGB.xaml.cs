using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorSchemeExtension
{
    /// <summary>
    /// Interaction logic for ColorWheelRGB.xaml
    /// </summary>
    public partial class ColorWheelRGB : UserControl, INotifyPropertyChanged
    {
        #region ctor / dtor

        public ColorWheelRGB ( )
        {
            InitializeComponent ( );

            R = 128;
            G = 128;
            B = 128;

            RPart.PropertyChanged += Color_PropertyChanged;
            GBPart.PropertyChanged += Color_PropertyChanged;
        }

        #endregion

        #region Properties

        #region Color

        private ColorEx _BaseColor = new ColorEx ( );

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

                SyncColor ( true, false, false, false );

                NotifyPropertyChanged ( );
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

                SyncColor ( false, true, false, false );

                NotifyPropertyChanged ( );
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

                SyncColor ( false, false, true, false );

                NotifyPropertyChanged ( );
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

                SyncColor ( false, false, false, true );

                NotifyPropertyChanged ( );
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

                SyncColor ( false, false, false, true );

                NotifyPropertyChanged ( );
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

                SyncColor ( false, false, false, true );

                NotifyPropertyChanged ( );
            }
        }

        #endregion

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged ( [CallerMemberName] string PropertyName = "" )
        {
            if ( PropertyChanged != null )
            {
                PropertyChanged ( this, new PropertyChangedEventArgs ( PropertyName ) );
            }
        }

        #endregion

        #region Event Handlers

        private void Color_PropertyChanged ( object sender, System.ComponentModel.PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == "R" )
            {
                R = ( ( ColorWheelR )sender ).R;
            }

            if ( e.PropertyName == "G" )
            {
                G = ( ( ColorWheelGB )sender ).G;
            }

            if ( e.PropertyName == "B" )
            {
                B = ( ( ColorWheelGB )sender ).B;
            }

            NotifyPropertyChanged ( "H" );
            NotifyPropertyChanged ( "S" );
            NotifyPropertyChanged ( "V" );
        }

        #endregion

        #region Helpers

        private void SyncColor ( bool R = false, bool G = false, bool B = false, bool HSV = false )
        {
            if ( HSV )
            {
                RPart.R = _BaseColor.R;
                GBPart.R = _BaseColor.R;
                GBPart.G = _BaseColor.G;
                GBPart.B = _BaseColor.B;
            }
            else
            {
                if ( R )
                {
                    RPart.R = _BaseColor.R;
                    GBPart.R = _BaseColor.R;
                }

                if ( G )
                {
                    GBPart.G = _BaseColor.G;
                }

                if ( B )
                {
                    GBPart.B = _BaseColor.B;
                }
            }
        }

        #endregion
    }
}
