using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Media;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace ColorSchemeExtension
{
	[ComVisible ( true )]
	public class ColorSchemeOptionsPage : DialogPage
	{
		// This is the only way I found to force saving settings from code (withotu opening the options dialog)
		public void SaveSettingsToStorage ( WritableSettingsStore Store )
		{
			PropertyDescriptorCollection oProperties = TypeDescriptor.GetProperties ( AutomationObject, new Attribute[ ]
			{
				DesignerSerializationVisibilityAttribute.Visible
			} );

			if ( !Store.CollectionExists ( SettingsRegistryPath ) )
			{
				Store.CreateCollection ( SettingsRegistryPath );
			}

			foreach ( PropertyDescriptor oProperty in oProperties )
			{
				TypeConverter oConverter = oProperty.Converter;

				if ( oConverter.CanConvertTo( typeof( string ) ) && oConverter.CanConvertFrom( typeof( string ) ) )
				{
					Store.SetString ( SettingsRegistryPath, oProperty.Name, oConverter.ConvertToInvariantString ( oProperty.GetValue ( AutomationObject ) ) );
				}
			}
		}

		// Basic mappings
		private Color2CodeList _Color2CodeList = new Color2CodeList ( )
		{
			new Color2CodeMap( ) { Extensions = ".js;.ts;.html;.aspx", Template = "'%X'" },
			new Color2CodeMap( ) { Extensions = ".css;.less", Template = "%X" },
			new Color2CodeMap( ) { Extensions = ".xaml", Template = "<Color R=\"%R\" G=\"%G\" B=\"%B\" />" },
			new Color2CodeMap( ) { Extensions = ".cs;.vb;.fs", Template = "Color.FromRgb(%R, %G, %B)" },
			new Color2CodeMap( ) { Extensions = ".c;.cpp", Template = "Color::FromRgb(%R, %G, %B)" }
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

		private SavedColorList _SavedColorList = new SavedColorList ( );

		public SavedColorList SavedColorList
		{
			get
			{
				return ( _SavedColorList );
			}
			set
			{
				_SavedColorList = value;
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
		[DisplayName ( "Extension list" )]
		public string Extensions
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
	}

	[TypeConverter ( typeof ( Color2CodeListTypeConverter ) )]
	public class Color2CodeList : List<Color2CodeMap>
	{
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
			return ( JsonConvert.DeserializeObject<Color2CodeList> ( Convert.ToString ( value ) ) );
		}

		public override object ConvertTo ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType )
		{
			return ( JsonConvert.SerializeObject ( value ) );
		}

		public override bool IsValid ( ITypeDescriptorContext context, object value )
		{
			return ( true );
		}
	}

	public class SavedColor
	{
		public string Name
		{
			get;
			set;
		}

		public Color Value
		{
			get;
			set;
		}
	}

	[TypeConverter ( typeof ( SavedColorListTypeConverter ) )]
	public class SavedColorList : List<SavedColor>
	{
	}

	public class SavedColorListTypeConverter : TypeConverter
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
			return ( JsonConvert.DeserializeObject<SavedColorList> ( Convert.ToString ( value ) ) );
		}

		public override object ConvertTo ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType )
		{
			return ( JsonConvert.SerializeObject ( value ) );
		}

		public override bool IsValid ( ITypeDescriptorContext context, object value )
		{
			return ( true );
		}
	}
}
