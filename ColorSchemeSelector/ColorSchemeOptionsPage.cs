﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace ColorSchemeExtension
{
	[ComVisible( true )]
	public class ColorSchemeOptionsPage : DialogPage
	{
		private Color2CodeList _Color2CodeList = new Color2CodeList( )
		{
			new Color2CodeMap( ) { Language = "JavaScript", Template = "'%X'" },
			new Color2CodeMap( ) { Language = "TypeScript", Template = "'%X'" },
			new Color2CodeMap( ) { Language = "CSS", Template = "%X" },
			new Color2CodeMap( ) { Language = "LESS", Template = "%X" },
			new Color2CodeMap( ) { Language = "XAML", Template = "<Color R=\"%R\" G=\"%G\" B=\"%B\" />" },
			new Color2CodeMap( ) { Language = "CSharp", Template = "Color.FromRgb(%R, %G, %B)" },
			new Color2CodeMap( ) { Language = "F#", Template = "Color.FromRgb(%R, %G, %B)", Explaination="Including F# script files" },
			new Color2CodeMap( ) { Language = "Basic", Template = "Color.FromRgb(%R, %G, %B)" },
			new Color2CodeMap( ) { Language = "HTMLX", Template = "'%X'", Explaination="Plain HTML file" },
			new Color2CodeMap( ) { Language = "HTML", Template = "'%X'", Explaination="Including ASPX files, master pages and user controls"},
			new Color2CodeMap( ) { Language = "C/C++", Template = "Color::FromRgb(%R, %G, %B)", Explaination="Including C/C++ header files"}
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
				return ( new ColorSchemeOptionsControl( this ) );
			}
		}
	}

	public class Color2CodeMap
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

		[DisplayName( "Explaination" )]
		public string Explaination
		{
			get;
			set;
		}
	}

	[TypeConverter( typeof( Color2CodeListTypeConverter ) )]
	public class Color2CodeList : List<Color2CodeMap>
	{
	}

	public class Color2CodeListTypeConverter : TypeConverter
	{
		public override bool CanConvertFrom ( ITypeDescriptorContext context, System.Type sourceType )
		{
			return ( sourceType == typeof( string ) );
		}

		public override bool CanConvertTo ( ITypeDescriptorContext context, System.Type destinationType )
		{
			return ( destinationType == typeof( string ) );
		}

		public override object ConvertFrom ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value )
		{
			return ( JsonConvert.DeserializeObject<Color2CodeList>( Convert.ToString( value ) ) );
		}

		public override object ConvertTo ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType )
		{
			return ( JsonConvert.SerializeObject( value ) );
		}

		public override bool IsValid ( ITypeDescriptorContext context, object value )
		{
			return ( true );
		}
	}
}
