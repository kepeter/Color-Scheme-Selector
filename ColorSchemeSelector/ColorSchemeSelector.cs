using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell.Settings;

namespace ColorSchemeExtension
{
	[PackageRegistration( UseManagedResourcesOnly = true )]
	[InstalledProductRegistration( "#110", "#112", "1.0", IconResourceID = 400 )]
	[ProvideMenuResource( "Menus.ctmenu", 1 )]
	[ProvideToolWindow( typeof( ColorSchemeToolWindow ), Style = Microsoft.VisualStudio.Shell.VsDockStyle.Tabbed, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057" )]
	[ProvideOptionPage( typeof( ColorSchemeOptionsPage ), "Custom Tools", "Color Scheme Selector", 0, 0, true )]
	[Guid( Guids.Package )]
	public sealed class ColorSchemeSelector : Package
	{
        private void InitStore()
        {
            ToolWindowPane oWindow = FindToolWindow ( typeof ( ColorSchemeToolWindow ), 0, true );

            if ( ( oWindow != null ) && ( oWindow.Frame != null ) )
            {
                SettingsManager oSettingsManager = new ShellSettingsManager ( this );
                WritableSettingsStore oWritableSettingsStore = oSettingsManager.GetWritableSettingsStore ( SettingsScope.UserSettings );

                ( ( ColorSchemeToolWindow )oWindow ).WritableSettingsStore = oWritableSettingsStore;
            }
        }

		private void ShowToolWindow ( object sender, EventArgs e )
		{
            ToolWindowPane oWindow = FindToolWindow ( typeof ( ColorSchemeToolWindow ), 0, true );

			if ( ( oWindow == null ) || ( oWindow.Frame == null ) )
			{
				throw new NotSupportedException( "Can not create tool window." );
			}

            InitStore ( );

			IVsWindowFrame oWindowFrame = ( IVsWindowFrame )oWindow.Frame;
			
			Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure( oWindowFrame.Show( ) );
		}

		#region Package Members

		protected override void Initialize ( )
		{
			base.Initialize( );

            InitStore ( );

			OleMenuCommandService oOleMenuCommandService = ( OleMenuCommandService )GetService( typeof( IMenuCommandService ) );

			if ( oOleMenuCommandService != null )
			{
				CommandID oCommandID = new CommandID( new Guid( Guids.CommandSet ), ( int )CommandIDs.ColorSchemeSelector );
				MenuCommand oMenuCommand = new MenuCommand( ShowToolWindow, oCommandID );

				oOleMenuCommandService.AddCommand( oMenuCommand );
			}
		}

		#endregion
	}
}
