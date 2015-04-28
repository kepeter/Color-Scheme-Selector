using System.Windows.Controls;
using System.Windows.Media;

namespace ColorSchemeExtension
{
    /// <summary>
    /// Interaction logic for ColorSchemeControl.xaml
    /// </summary>
    public partial class ColorSchemeControl : UserControl
    {
        public ColorSchemeControl ( )
        {
            InitializeComponent ( );

            SelectedColor.Background = new SolidColorBrush ( Color.FromRgb ( GBPart.R, GBPart.G, GBPart.B ) );

            RPart.PropertyChanged += RColor_PropertyChanged;
            GBPart.PropertyChanged += GBColor_PropertyChanged;
        }

        void GBColor_PropertyChanged ( object sender, System.ComponentModel.PropertyChangedEventArgs e )
        {
            SelectedColor.Background = new SolidColorBrush ( Color.FromRgb ( GBPart.R, GBPart.G, GBPart.B ) );
        }

        void RColor_PropertyChanged ( object sender, System.ComponentModel.PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == "R" )
            {
                GBPart.R = ( ( ColorWheelR )sender ).R;
            }
        }
    }
}
