﻿using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Settings;
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

        public WritableSettingsStore WritableSettingsStore
        {
            get;
            set;
        }

		internal object GetVsService ( Type service )
		{
			return GetService( service );
		}
	}
}
