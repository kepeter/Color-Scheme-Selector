using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;

namespace ColorSchemeExtension
{
	public class Color2CodeTypeConverter : TypeConverter
	{
		public override bool CanConvertFrom ( ITypeDescriptorContext context, System.Type sourceType )
		{
			return base.CanConvertFrom( context, sourceType );
		}

		public override bool CanConvertTo ( ITypeDescriptorContext context, System.Type destinationType )
		{
			return base.CanConvertTo( context, destinationType );
		}

		public override object ConvertFrom ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value )
		{
			return base.ConvertFrom( context, culture, value );
		}

		public override object ConvertTo ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType )
		{
			return base.ConvertTo( context, culture, value, destinationType );
		}

		public override bool IsValid ( ITypeDescriptorContext context, object value )
		{
			return base.IsValid( context, value );
		}
	}

	[TypeConverter( typeof( Color2CodeTypeConverter ) )]
	public class Color2Code
	{
		[DisplayName( "Language" )]
		public string Language
		{
			get;
			set;
		}

		[DisplayName( "Code template" )]
		public string Template
		{
			get;
			set;
		}
	}

	[ComVisible( true )]
	public class ColorSchemeOptionsPage : DialogPage
	{
		private Color2Code[ ] _Color2CodeMap = new Color2Code[ ]
		{
			new Color2Code(){Language="JavaScript",Template="JavaScript"},
			new Color2Code(){Language="CSharp",Template="CSharp"},
			new Color2Code(){Language="CSS",Template="CSS"},
			new Color2Code(){Language="XAML",Template="XAML"},
			new Color2Code(){Language="Basic",Template="Basic"},
			new Color2Code(){Language="HTMLX",Template="HTMLX"},
			new Color2Code(){Language="LESS",Template="LESS"}
		};

		public Color2Code[ ] Color2CodeMap
		{
			get
			{
				return ( _Color2CodeMap );
			}
			set
			{
				_Color2CodeMap = value;
			}
		}

		protected override IWin32Window Window
		{
			get
			{
				return ( new ColorSchemeOptionsControl( this ) );
			}
		}

		public override void SaveSettingsToStorage ( )
		{
			base.SaveSettingsToStorage( );
		}
	}
}
