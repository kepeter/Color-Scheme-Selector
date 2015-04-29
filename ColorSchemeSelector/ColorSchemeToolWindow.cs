using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;

namespace ColorSchemeExtension
{
	[Guid( "2b119b11-7f31-4e10-adf1-6b43a180dbac" )]
	public class ColorSchemeToolWindow : ToolWindowPane
	{
		public ColorSchemeToolWindow ( ) :
			base( null )
		{
			this.Caption = "Color Scheme Selector";
			this.BitmapResourceID = 301;
			this.BitmapIndex = 1;

			base.Content = new ColorSchemeControl( this );
		}

		internal object GetVsService ( Type service )
		{
			return GetService( service );
		}
	}
}
