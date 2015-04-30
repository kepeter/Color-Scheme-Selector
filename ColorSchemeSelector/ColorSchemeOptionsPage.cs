using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;

namespace ColorSchemeExtension
{
    [ComVisible ( true )]
    public class ColorSchemeOptionsPage : DialogPage
    {
        private Color2CodeList _Color2CodeList = new Color2CodeList ( )
		{
			new Color2CodeMap( ) { Language = "JavaScript", Template = "JavaScript" },
			new Color2CodeMap( ) { Language = "CSharp", Template = "CSharp" },
			new Color2CodeMap( ) { Language = "CSS", Template = "CSS" },
			new Color2CodeMap( ) { Language = "XAML", Template = "XAML" },
			new Color2CodeMap( ) { Language = "Basic", Template = "Basic" },
			new Color2CodeMap( ) { Language = "HTMLX", Template = "HTMLX" },
			new Color2CodeMap( ) { Language = "LESS", Template = "LESS" }
		};

        public Color2CodeList Color2CodeList
        {
            get
            {
                return ( _Color2CodeList );
            }
            set
            {
                _Color2CodeList = value;
            }
        }

        protected override IWin32Window Window
        {
            get
            {
                return ( new ColorSchemeOptionsControl ( this ) );
            }
        }
    }

    public class Color2CodeMap
    {
        [DisplayName ( "Language" )]
        public string Language
        {
            get;
            set;
        }

        [DisplayName ( "Code template" )]
        public string Template
        {
            get;
            set;
        }

        public override string ToString ( )
        {
            return ( string.Format ( "{0}={1}", Language, Template ) );
        }
    }

    [TypeConverter ( typeof ( Color2CodeListTypeConverter ) )]
    public class Color2CodeList : List<Color2CodeMap>
    {
        public override string ToString ( )
        {
            string szRV = string.Empty;

            foreach ( Color2CodeMap oItem in ToArray ( ) )
            {
                szRV = string.Format ( "{0}{1};", szRV, oItem.ToString ( ) );
            }

            return ( szRV );
        }
    }

    public class Color2CodeListTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom ( ITypeDescriptorContext context, System.Type sourceType )
        {
            return ( sourceType == typeof ( string ) );
        }

        public override bool CanConvertTo ( ITypeDescriptorContext context, System.Type destinationType )
        {
            return ( destinationType == typeof ( string ) );
        }

        public override object ConvertFrom ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value )
        {
            JavaScriptSerializer oSerializer = new JavaScriptSerializer ( );

            return ( oSerializer.Deserialize ( ( string )value, typeof ( Color2CodeList ) ) );
        }

        public override object ConvertTo ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType )
        {
            JavaScriptSerializer oSerializer = new JavaScriptSerializer ( );

            return ( oSerializer.Serialize ( value ) );
        }

        public override bool IsValid ( ITypeDescriptorContext context, object value )
        {
            return ( true );
        }
    }
}
